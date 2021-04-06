using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Stanley_FSM.Monitor;

namespace Stanley_FSM.Machine
{
    public class FiniteStateMachine : IFiniteStateMachine
    {
        private StateMachine machine = null;
        private IMachineMonitor monitor = null;
        private string name;
        private object mutex = new object();
        private int testDelay = 0;
        volatile bool isStop = false;
        private List<IState> fsmStates = new List<IState>();

        public int TestDelay
        { get { return testDelay; } set { testDelay = value; } }
        public List<IState> FSMStates
        { get { return fsmStates; }  set { fsmStates = value; } }
        public string Name
        { get { return name; } set { name = value; } }
        public bool IsStop
        { get { return isStop; } }

        public FiniteStateMachine()
        { this.machine = new StateMachine(); }
        public FiniteStateMachine(string name)
        {
            this.name = name;
            this.machine = new StateMachine();
        }
        public FiniteStateMachine(string name, IMachineMonitor monitor)
        {
            this.name = name;
            this.monitor = monitor;
            this.machine = new StateMachine();
            this.monitor.RunStatus = StateMachineRunStatus.Idle;
        }

        public void SetCurrentState(IState state)
        {
            machine.SetCurrentState(state);
        }

        public void SetFirstState(IState state)
        {
            machine.SetFirstState(state);
        }

        public void SetFinalState(IState state)
        {
            machine.SetFinalState(state);
        }

        public void Stop()
        {
            this.isStop = true;
        }

        public void Reset()
        {
            this.isStop = false;
        }

        public virtual void ResetMachineEvent()
        {
            
        }

        public virtual void SetStateChain()
        {
           
        }

        public virtual void CreateFSMStateInstance()
        {

        }

        public void ResetCurrentStateToFirsetState()
        {
            SetCurrentState(machine.FirstState);
        }

        public void Add(string name, ExecuteActionHandler action, out BasicState state)
        {
            state = new BasicState(name, action);
            this.FSMStates.Add(state);
        }

        public int RunSingleState(IState state)
        {

            if (fsmStates.Count == 0) return FSMInnerErrorCode.NotExecute;
            int errorCode = state.Execute();

            return errorCode;
        }

        public int RunStateChain(FSMRunMode runMode)
        {
            int errorCode = FSMInnerErrorCode.NoError;
            try
            {
               
                while (true)
                {
                    lock (mutex)
                    {
                        //執行指令
                        errorCode = machine.Execute();
                        if (errorCode != FSMInnerErrorCode.NoError && errorCode != FSMInnerErrorCode.Repeat) break;
                        if (machine.CurrentState == machine.FinalState && runMode == FSMRunMode.OneCycle && errorCode != FSMInnerErrorCode.Repeat)
                        {
                            machine.SetCurrentState(machine.FirstState);
                            break;
                        }
                        if (machine.CurrentState == machine.FinalState && runMode == FSMRunMode.Auto && errorCode != FSMInnerErrorCode.Repeat)
                        {
                            ResetMachineEvent();
                        }


                        //機器暫停 根據狀態來記錄當前的state，以便Resume會依據記錄的state繼續執行下去
                        if (this.isStop)
                        {
                            if (machine.CurrentState.CurrentStatus == FSMStateStatus.Done)
                            {
                                machine.ChangeState(machine.CurrentState.NextState);
                            }
                            else
                            {
                                machine.SetCurrentState(machine.CurrentState);
                            }
                            machine.CurrentState.CurrentStatus = FSMStateStatus.Idle;
                            monitor.RunStatus = StateMachineRunStatus.Idle;
                          
                            break;
                        }


                        //如果不是重覆ErrorCode，就指定下一個State
                        if (errorCode != FSMInnerErrorCode.Repeat)
                            machine.ChangeState(machine.CurrentState.NextState);

                        //等待其他工站發命令的重覆等待，Reset ErrorCode
                        if (errorCode == FSMInnerErrorCode.Repeat)
                        {
                            errorCode = FSMInnerErrorCode.NoError;
                        }


                        //跑單環停在最後一個State，並執行最後一次的Execute


                        //Test 自動化的時間等待
                        //Thread.Sleep(TestDelay);
                    }
                }

            }
            catch
            {
               this.isStop = true;
            }
            finally
            {
                if (errorCode != FSMInnerErrorCode.NoError)
                {
                    //isStop = true;
                    monitor.RunStatus = StateMachineRunStatus.Error;
                    machine.CurrentState.CurrentStatus = FSMStateStatus.Error;
                }

            }

            return errorCode;
        }
    }
}
