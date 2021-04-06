using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanley_FSM.Machine
{
    public class StateMachine
    {
        public IState CurrentState { get; private set; }
        public IState FirstState { get; private set; }
        public IState FinalState { get; private set; }
        public IState PreviousState { get; private set; }
        public IState NextState { get; private set; }

        public void SetFirstState(IState firstState)
        {
            this.FirstState = firstState;
            CurrentState = firstState;
        }

        public void SetFinalState(IState firstState)
        {
            this.FinalState = firstState;
        }

        public void SetCurrentState(IState state)
        {
            this.CurrentState = state;
        }

        public void ChangeState(IState nextState)
        {
            this.PreviousState = CurrentState;
            this.NextState = nextState;
            this.CurrentState = nextState;
        }

        public int Execute()
        {
            int errorCode = FSMInnerErrorCode.NoError;

            do
            {
                if (this.CurrentState == null)
                {
                    errorCode = FSMInnerErrorCode.NotExecute;
                    break;
                }

                errorCode = this.CurrentState.Execute();

            } while (false);

            return errorCode;
        }
    }
}

