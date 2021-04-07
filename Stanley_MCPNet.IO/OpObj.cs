using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Stanley_MCPNet.IO
{
    [Serializable]
    public class OpObj : IOElement
    {
        public OpObj()
        {
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        [XmlIgnore]
        public bool Enable
        {
            get
            {
                return this.enable;
            }
            set
            {
                this.enable = value;
            }
        }

        public OpObj(int cardNo, int portNo, int bit, int[] portBuf, int bufNo, int logic, IOBase iOBase, string name, bool enable = true, bool simulate = false, string groupName = "")
        {
            this.cardNo = cardNo;
            this.portNo = portNo;
            this.bit = bit;
            this.PortBuf = portBuf;
            this.logic = logic;
            this.bufNo = bufNo;
            this.name = name;
            this.enable = enable;
            this.iOBase = iOBase;
            if (logic == 0)
            {
                this.PortBuf[bufNo] |= bit;
            }
            else
            {
                this.PortBuf[bufNo] &= (int)(-1 - ((long)bit));
            }
            this.isOutput = true;
            this.caption = name;
            this.simulate = simulate;
            this.groupName = groupName;
            base.State = (simulate ? IOState.Off : IOState.Unknown);
            base.ToggleOutput = new ToggleOutputCmd(this);
        }

        public void Set(bool bSet)
        {
            if (this.iOBase != null)
            {
                if (this.simulate)
                {
                    base.State = (bSet ? IOState.On : IOState.Off);
                }
                else if (this.enable)
                {
                    this.iOBase.out_bit(this.cardNo, this.portNo, this.bit, ref this.PortBuf[this.bufNo], (this.logic == 1) ? bSet : (!bSet));
                }
            }
        }

        public void Off()
        {
            this.Set(false);
        }

        public void On()
        {
            this.Set(true);
        }

        public bool IsOn()
        {
            bool result;
            if (this.simulate)
            {
                result = (base.State == IOState.On);
            }
            else if (!this.enable)
            {
                base.State = IOState.Unknown;
                result = false;
            }
            else if (this.iOBase == null)
            {
                base.State = IOState.Unknown;
                result = false;
            }
            else
            {
                bool flag = (this.logic == 1) ? this.iOBase.outbit_readback(this.cardNo, this.portNo, this.bit, ref this.PortBuf[this.bufNo]) : (!this.iOBase.outbit_readback(this.cardNo, this.portNo, this.bit, ref this.PortBuf[this.bufNo]));
                base.State = (flag ? IOState.On : IOState.Off);
                result = flag;
            }
            return result;
        }

        public override bool ReadState()
        {
            bool result;
            if (this.simulate)
            {
                result = true;
            }
            else
            {
                bool flag = false;
                if (!this.enable)
                {
                    base.State = IOState.Unknown;
                }
                else if (this.iOBase == null)
                {
                    base.State = IOState.Unknown;
                }
                else
                {
                    base.State = (((this.logic == 1) ? this.iOBase.outbit_readback(this.cardNo, this.portNo, this.bit, ref this.PortBuf[this.bufNo]) : (!this.iOBase.outbit_readback(this.cardNo, this.portNo, this.bit, ref this.PortBuf[this.bufNo]))) ? IOState.On : IOState.Off);
                }
                result = flag;
            }
            return result;
        }

        public bool IsOff()
        {
            bool result;
            if (this.simulate)
            {
                result = (base.State == IOState.Off);
            }
            else if (!this.enable)
            {
                base.State = IOState.Unknown;
                result = false;
            }
            else if (this.iOBase == null)
            {
                base.State = IOState.Unknown;
                result = false;
            }
            else
            {
                bool flag = (this.logic == 1) ? (!this.iOBase.outbit_readback(this.cardNo, this.portNo, this.bit, ref this.PortBuf[this.bufNo])) : this.iOBase.outbit_readback(this.cardNo, this.portNo, this.bit, ref this.PortBuf[this.bufNo]);
                base.State = ((!flag) ? IOState.On : IOState.Off);
                result = flag;
            }
            return result;
        }

        public bool Toggle()
        {
            if (this.IsOn())
            {
                this.Off();
            }
            else
            {
                this.On();
            }
            return this.IsOn();
        }
    }
}
