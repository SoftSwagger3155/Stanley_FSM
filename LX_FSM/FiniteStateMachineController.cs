using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LX_MachineMonitor;

namespace LX_FSM
{
    public class FiniteStateMachineController
    {
        public  MachineMonitorController MonitorController { get; private set; }
        public Dictionary<StateMachineType, List<FiniteStateMachine>> AllStateMachineDict { get; private set; }
        public List<FiniteStateMachine> InitializeFSM { get; private set; }
        public List<FiniteStateMachine> AutoFSM { get; private set; }
        public List<FiniteStateMachine> ResetFSM { get; private set; }


        public FiniteStateMachineController()
        {
            this.MonitorController = new MachineMonitorController();
            AllStateMachineDict = new Dictionary<StateMachineType, List<FiniteStateMachine>>();
            InitializeFSM = new List<FiniteStateMachine>();
            AutoFSM = new List<FiniteStateMachine>();
            ResetFSM = new List<FiniteStateMachine>();

            AllStateMachineDict[StateMachineType.Initilize] = InitializeFSM;
            AllStateMachineDict[StateMachineType.Auto] = AutoFSM;
            AllStateMachineDict[StateMachineType.Reset] = ResetFSM;
        }

     //==========================================================//
     //跑Cycle
        public void RunFSMChain(StateMachineType type, StateMachineStatus status)
        {
            try
            {
                if (AllStateMachineDict.Count == 0) return;

                this.MonitorController.SetMachineStatus(MachineStatus.Busy);
                Parallel.ForEach(AllStateMachineDict[type], FSM => { FSM.RunStateChain(status); });

            }
            catch
            {
                this.MonitorController.SetMachineStatus(MachineStatus.Idle);
            }
            finally
            {
                this.MonitorController.SetMachineStatus(MachineStatus.Idle);
            }
        }
        //==========================================================//

        //==========================================================//
        public void Reset(StateMachineType type)
        {
            List<FiniteStateMachine> temp = AllStateMachineDict[type];
            foreach (var item in temp)
            {
                item.SetCurrentState(item.Machine.FirstState);
            }
        }

         
        //==========================================================//
    }
}
