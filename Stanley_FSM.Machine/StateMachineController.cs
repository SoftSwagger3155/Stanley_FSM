using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Stanley_FSM.Monitor;
using System.Threading;

namespace Stanley_FSM.Machine
{
    public class StateMachineController: INotifyPropertyChanged
    {
        private string errorMs;
        private IMachineMonitor monitor = null;
        private List<IFiniteStateMachine> stateMachines = new List<IFiniteStateMachine>();
        private void OnPropertyChanged(string info)
        { if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(info)); }
        private Thread singleCycleThread = null;
        private Thread autoCycleThread = null;
        private bool isReqStop = false;
        public string ErrorMsg { get { return errorMs;} }
        public List<IFiniteStateMachine> StateMachines
        {
            get { return stateMachines; }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public StateMachineController(IMachineMonitor monitor)
        {
            this.monitor = monitor;

            this.monitor.PropertyChanged -= Monitor_PropertyChanged;
            this.monitor.PropertyChanged += Monitor_PropertyChanged;
        }

        private void Monitor_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
           if(e.PropertyName == "RunStatus")
            {
                if(isReqStop == false)
                {
                    if ((sender as IMachineMonitor).RunStatus == StateMachineRunStatus.Error)
                    {
                        if (monitor.RunMode == FSMRunMode.OneCycle)
                            StopSingleCycle();
                        else if (monitor.RunMode == FSMRunMode.Auto)
                            StopAutoCycle();
                    }
                }
            }
        }

        public void Add(params IFiniteStateMachine[]  machines)
        {
            for (int i = 0; i < machines.Length; i++)
            {
                stateMachines.Add(machines[i]);
            }
        }
   
        public void Stop()
        {
            if (stateMachines.Count == 0) return;
            
            foreach (var item in stateMachines)
            {
                item.Stop();
            }
            monitor.RunStatus = StateMachineRunStatus.Stop;
            isReqStop = true;
        }

        public void Reset()
        {
            if (stateMachines.Count == 0) return;

            foreach (var item in stateMachines)
            {
                item.Reset();
            }
            isReqStop = false;
        }

        public void RunSingleCycle()
        {
            singleCycleThread = new Thread(() => RunStateChain(FSMRunMode.OneCycle));
            singleCycleThread.IsBackground = true;
            singleCycleThread.Start();
        }

        public void StopSingleCycle()
        {
            if (singleCycleThread == null) return;
            if (singleCycleThread.IsAlive == false) return;
            this.Stop();
            singleCycleThread.Join();
           
        }

        public void RunAutoCycle()
        {
            autoCycleThread = new Thread(() => RunStateChain(FSMRunMode.Auto));
            autoCycleThread.IsBackground = true;
            autoCycleThread.Start();
        }

        public void StopAutoCycle()
        {
            if (autoCycleThread == null) return;
            if (autoCycleThread.IsAlive == false) return;

            this.Stop();
            autoCycleThread.Join();
            
        }

        public void RunStateChain(FSMRunMode runMode)
        {         
            try
            {
                if(runMode != monitor.RunMode)
                {
                    stateMachines.ForEach(x => { x.ResetCurrentStateToFirsetState(); });
                }

                monitor.RunStatus = StateMachineRunStatus.Running;
                monitor.RunMode = runMode;

                if (stateMachines.Count == 0) return;

                Reset();
                Parallel.ForEach(stateMachines, FSM => { FSM.RunStateChain(runMode); });

            }
            catch (Exception ex)
            {
                errorMs = ex.Message;

            }
        }

        public int RunSingleStateAction(string name)
        {
            int errorCode = FSMInnerErrorCode.NoError;
            monitor.RunMode = FSMRunMode.SingleState;
            IState foundState = null;
            do
            {
                for (int i = 0; i < StateMachines.Count; i++)
                {
                    foundState = stateMachines[i].FSMStates.ToList().Find(x => x.Name == name);
                    if (foundState != null) break;
                }

                if (foundState == null)
                    throw new Exception("Can't Find Execute State");

                foundState.Execute();


            } while (false);

            return errorCode;
        }
    }
}
