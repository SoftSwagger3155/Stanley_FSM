using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Windows.Input;

namespace Stanley_MCPNet.IO
{
    [XmlInclude(typeof(OpObj))]
    [XmlInclude(typeof(IpObj))]
    [Serializable]
    public class IOElement : INotifyPropertyChanged
    {
        [XmlIgnore]
        public ICommand ToggleOutput { get; set; }

        [XmlIgnore]
        public bool IsOutput
        {
            get
            {
                return this.isOutput;
            }
        }

        public string ActiveUIColor
        {
            get
            {
                return this.activeUIColor;
            }
            set
            {
                this.activeUIColor = value;
            }
        }

        public string GroupName
        {
            get
            {
                return this.groupName;
            }
            set
            {
                this.groupName = value;
            }
        }

        public string Caption
        {
            get
            {
                return this.caption;
            }
            set
            {
                this.caption = value;
                this.OnPropertyChanged("Caption");
            }
        }

        [XmlIgnore]
        public string Info
        {
            get
            {
                int num = this.bit;
                ushort value = (ushort)this.bit;
                string text = Convert.ToString((int)value, 2);
                if (text != null && text.Length > 0)
                {
                    num = text.Length - 1;
                }
                return string.Format("Port={0} Bit={1} Grp={2}", this.portNo, num, this.groupName);
            }
        }

        [XmlIgnore]
        public IOState State
        {
            get
            {
                return this.state;
            }
            protected set
            {
                if (value != this.state)
                {
                    this.state = value;
                    this.OnPropertyChanged("State");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public void UpdateInfo(IOElement src)
        {
            this.name = src.name;
            this.caption = src.caption;
            this.cardNo = src.cardNo;
            this.portNo = src.portNo;
            this.bit = src.bit;
            this.bufNo = src.bufNo;
            this.logic = src.logic;
            this.groupName = src.groupName;
            this.OnPropertyChanged("Info");
        }

        public void SetSimulationDefault()
        {
            this.State = (this.simulate ? IOState.Off : IOState.Unknown);
        }

        public virtual bool ReadState()
        {
            return true;
        }

        public void UpdateStateFromBuffer()
        {
            if (!this.simulate)
            {
                if (!this.enable)
                {
                    this.State = IOState.Unknown;
                }
                else if (this.iOBase == null)
                {
                    this.State = IOState.Unknown;
                }
                else if (this.bufNo >= 0 && this.PortBuf != null && this.bufNo < this.PortBuf.Length)
                {
                    bool flag = (this.PortBuf[this.bufNo] & this.bit) == this.bit;
                    this.State = (((this.logic == 1) ? flag : (!flag)) ? IOState.On : IOState.Off);
                }
            }
        }

        public void UpdateStateFromBufferByMcc800(int checkState)
        {
            if (!this.simulate)
            {
                if (!this.enable)
                {
                    this.State = IOState.Unknown;
                }
                else if (this.iOBase == null)
                {
                    this.State = IOState.Unknown;
                }
                else if (this.bufNo >= 0 && this.PortBuf != null && this.bufNo < this.PortBuf.Length)
                {
                    bool flag = checkState == 0;
                    this.State = (((this.logic == 1) ? flag : (!flag)) ? IOState.On : IOState.Off);
                }
            }
        }

        public int cardNo;
        public int portNo;
        public int bit;
        public int logic;
        public string name;
        public bool enable;
        public int bufNo;
        protected bool isOutput = false;
        protected string activeUIColor = "Green";
        protected string groupName = "";
        protected string caption = "";
        [XmlIgnore]
        public int[] PortBuf;
        [XmlIgnore]
        public bool simulate;
        [XmlIgnore]
        private IOState state = IOState.Unknown;
        [XmlIgnore]
        public IOBase iOBase;
    }
}
