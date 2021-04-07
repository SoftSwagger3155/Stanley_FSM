using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Stanley_MCPNet.IO
{
    [Serializable]
    public class IpObj : IOElement
    {
        public IpObj()
        {
            this.iOBase = null;
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

        public IpObj(int cardNo, int portNo, int bit, int logic, IOBase iOBase, string name, bool enable = true, bool simulate = false, string groupName = "")
        {
            this.cardNo = cardNo;
            this.portNo = portNo;
            this.bit = bit;
            this.logic = logic;
            this.name = name;
            this.enable = enable;
            this.iOBase = iOBase;
            this.caption = name;
            this.simulate = simulate;
            base.State = (simulate ? IOState.Off : IOState.Unknown);
            this.groupName = groupName;
        }

        public void SetInBuffer(int[] portBuf, int bufNo)
        {
            this.PortBuf = portBuf;
            this.bufNo = bufNo;
        }

        public void SimulateOff()
        {
            this.Set(false);
        }

        public void SimulateOn()
        {
            this.Set(true);
        }

        public void Set(bool bSet)
        {
            if (this.iOBase != null)
            {
                if (this.simulate)
                {
                    base.State = (bSet ? IOState.On : IOState.Off);
                }
            }
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
                bool flag = (this.logic == 1) ? this.iOBase.inp_chkbit(this.cardNo, this.portNo, this.bit) : (!this.iOBase.inp_chkbit(this.cardNo, this.portNo, this.bit));
                base.State = (flag ? IOState.On : IOState.Off);
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
                bool flag = (this.logic == 1) ? (!this.iOBase.inp_chkbit(this.cardNo, this.portNo, this.bit)) : this.iOBase.inp_chkbit(this.cardNo, this.portNo, this.bit);
                base.State = ((!flag) ? IOState.On : IOState.Off);
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
                    bool flag2;
                    if (this.PortBuf == null || this.bufNo >= this.PortBuf.Length)
                    {
                        flag2 = ((this.logic == 1) ? this.iOBase.inp_chkbit(this.cardNo, this.portNo, this.bit) : (!this.iOBase.inp_chkbit(this.cardNo, this.portNo, this.bit)));
                    }
                    else
                    {
                        flag2 = ((this.logic == 1) ? this.iOBase.inp_chkbit(this.cardNo, this.portNo, this.bit, ref this.PortBuf[this.bufNo]) : (!this.iOBase.inp_chkbit(this.cardNo, this.portNo, this.bit, ref this.PortBuf[this.bufNo])));
                    }
                    base.State = (flag2 ? IOState.On : IOState.Off);
                }
                result = flag;
            }
            return result;
        }

        public void SetSimulateStat(bool on)
        {
            if (on)
            {
                this.SimulateOn();
            }
            else
            {
                this.SimulateOff();
            }
        }
        
        public bool Wait(bool wait_sts, int wait_delay, int sleep_delay)
        {
            bool result;
            if (!this.enable)
            {
                result = false;
            }
            else if (this.iOBase == null)
            {
                result = false;
            }
            else
            {
                bool flag;
                if (this.simulate)
                {
                    DateTime now = DateTime.Now;
                    TimeSpan timeSpan = DateTime.Now - now;
                    while ((DateTime.Now - now).TotalMilliseconds <= (double)wait_delay)
                    {
                        if (sleep_delay > 0)
                        {
                            Thread.Sleep(sleep_delay);
                        }
                        Thread.Sleep(50);
                        if (this.logic == 1)
                        {
                            flag = (wait_sts ? (base.State == IOState.On) : (base.State == IOState.Off));
                        }
                        else
                        {
                            flag = (wait_sts ? (base.State == IOState.Off) : (base.State == IOState.On));
                        }
                        if (flag)
                        {
                            return true;
                        }
                    }             
                }
                flag = ((this.logic == 1) ? this.iOBase.wait_inpbit(this.cardNo, this.portNo, this.bit, wait_sts, wait_delay, sleep_delay) : this.iOBase.wait_inpbit(this.cardNo, this.portNo, this.bit, !wait_sts, wait_delay, sleep_delay));
                result = flag;
            }
            return result;
        }
    }
}
