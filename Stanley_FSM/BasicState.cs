using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanley_FSM
{
    public delegate int ExecuteActionHandler();
    public class BasicState : IState
    {
        private ExecuteActionHandler executeAction = null;
        public BasicState(string name, ExecuteActionHandler executeAction)
        {
            this.Name = name;
            this.CurrentState = this;
            //this.CurrentState.Name = this;
            this.executeAction = executeAction;
            this.CurrentStatus = FSMStateStatus.Idle;
        }

        public override int Execute()
        {
            int errorCode = FSMInnerErrorCode.NoError;
            this.CurrentStatus = FSMStateStatus.Running;
            errorCode = this.executeAction.Invoke();

            if (errorCode != FSMInnerErrorCode.NoError)
                this.CurrentStatus = FSMStateStatus.Error;
            else
                this.CurrentStatus = FSMStateStatus.Done;


            return errorCode;
        }
    }
}
