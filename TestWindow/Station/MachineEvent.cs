using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Test_FSM_WPF.Station
{
    public class MachineEvent
    {
        public List<EventWaitHandle> ResetCenterA { get; private set; }
        public List<EventWaitHandle> ResetCenterB { get; private set; }
        public List<EventWaitHandle> ResetCenterC { get; private set; }
        public ManualResetEvent SwitchCall_A = new ManualResetEvent(false);
        public ManualResetEvent SwitchCall_B = new ManualResetEvent(false);
        public ManualResetEvent SwitchCall_C = new ManualResetEvent(false);

        public MachineEvent()
        {
            ResetCenterA = new List<EventWaitHandle>();
            ResetCenterB = new List<EventWaitHandle>();
            ResetCenterC = new List<EventWaitHandle>();
            BuildResetCenter();
        }

        void BuildResetCenter()
        {
            ResetCenterA.Add(SwitchCall_A);
            ResetCenterB.Add(SwitchCall_B);
            ResetCenterC.Add(SwitchCall_C);
        }

        public void ResetEventA()
        {
            
            if (ResetCenterA.Count != 0)
            {
                foreach (var item in ResetCenterA)
                {
                    item.Reset();
                }
            }
        }

        public void ResetEventB()
        {
            if (ResetCenterB.Count != 0)
            {
                foreach (var item in ResetCenterB)
                {
                    item.Reset();
                }
            }
        }

        public void ResetEventC()
        {
            if (ResetCenterC.Count != 0)
            {
                foreach (var item in ResetCenterC)
                {
                    item.Reset();
                }
            }
        }
    }
}
