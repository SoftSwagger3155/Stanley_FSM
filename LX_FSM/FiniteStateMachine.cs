using LX_MachineMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace LX_FSM
{
    using Flag = LX_MachineMonitor.SystemFlag;

    public class FiniteStateMachine : DispatcherObject, IFiniteStateMachine
    {
        public int TestDelay { get; set; }
        public string Name { get; set; }
        public List<IState> StatePool { get; private set; }

        public SingleStateStatus SingleStateStatus { get; private set; } = SingleStateStatus.Idle;
        public StateMachineStatus StateMachineStatus { get; private set; }

        public  FSMStatus Fstatus { get; private set; } =  FSMStatus.None;

        public StateMachine Machine { get; private set; }


        public FiniteStateMachine()
        {
            Machine = new StateMachine();
            StatePool = new List<IState>();
            Flag.ReActive();
        }

        public FiniteStateMachine(StateMachineType type)
        {
            Machine = new StateMachine();
            Flag.ReActive();
        }

        public void SetFirstState(IState state)
        {
            Machine.SetFirstState(state);
        }

        public void SetFinalState(IState state)
        {
            Machine.SetFinalState(state);
        }

        public void SetCurrentState(IState state)
        {
            Machine.SetCurrentState(state);
        }

        public int RunSingleState(IState state)
        {
            int errorCode = FSMInnerErrorCode.NoError;

            try
            {
                do
                {
                     this.StateMachineStatus = StateMachineStatus.SingleState;
                    //==============================================//
                    errorCode = this.SingleStateStatus == SingleStateStatus.Busy ? FSMInnerErrorCode.IsBusy : FSMInnerErrorCode.NoError;
                    if (errorCode != FSMInnerErrorCode.NoError) break;
                 //==============================================//

                    this.SingleStateStatus = SingleStateStatus.Busy;
                    errorCode = state.Execute();
                    if (errorCode != FSMInnerErrorCode.NoError) break;

                } while (false);

            }
            catch 
            {
                this.SingleStateStatus = SingleStateStatus.Idle;
            }
            finally
            {
                this.SingleStateStatus = SingleStateStatus.Idle;
            }
          
            return errorCode;
        }

        object lckMutex = new object();
        public virtual int RunStateChain(StateMachineStatus status)
        {
            int errorCode = FSMInnerErrorCode.NoError;
            Flag.ReActive();
            this.StateMachineStatus = status;
            try
            {
                //==========================================//
                while (true)
                {
                   lock(lckMutex)
                    {
                        //執行指令
                        errorCode = Machine.Execute();
                        if (errorCode != FSMInnerErrorCode.NoError && errorCode != FSMInnerErrorCode.Repeat) break;
                        if (Machine.CurrentState == Machine.FinalState && status == StateMachineStatus.OneCycle && errorCode != FSMInnerErrorCode.Repeat)
                        {
                            break;
                        }
                        if (Machine.CurrentState == Machine.FinalState && status == StateMachineStatus.Auto && errorCode != FSMInnerErrorCode.Repeat)
                        {
                            ResetFlag();
                        }


                        //機器暫停 根據狀態來記錄當前的state，以便Resume會依據記錄的state繼續執行下去
                        if (Flag.IsStop())
                        {
                            if (Machine.CurrentState.Status == FSMStatus.ExitDone)
                            {
                                Machine.ChangeState(Machine.CurrentState.NextState);
                            }
                            else
                            {
                                Machine.SetCurrentState(Machine.CurrentState);
                            }

                            break;
                        }


                        //如果不是重覆ErrorCode，就指定下一個State
                        if (errorCode != FSMInnerErrorCode.Repeat)
                            Machine.ChangeState(Machine.CurrentState.NextState);

                        //等待其他工站發命令的重覆等待，Reset ErrorCode
                        if (errorCode == FSMInnerErrorCode.Repeat)
                        {
                            errorCode = FSMInnerErrorCode.NoError;
                        }


                        //跑單環停在最後一個State，並執行最後一次的Execute


                        //Test 自動化的時間等待
                        Thread.Sleep(TestDelay);
                    }
                }

            }
            catch
            {
                SystemFlag.Stop();
            }

            if (errorCode != FSMInnerErrorCode.NoError)
                SystemFlag.Stop();

            return errorCode;
        }

        public virtual void SetStateChain()
        {
            
        }

        public virtual void ResetFlag()
        {
        }
    }
}
