using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LX_FSM
{
    using Flag = LX_MachineMonitor.SystemFlag;

    public delegate int EnterActionHandler();
    public delegate int ExecuteActionHandler();
    public delegate int ExitActionHandler();


    public class BasicState :  IState
    {
        EnterActionHandler onEnter;
        ExecuteActionHandler onExecute;
        ExitActionHandler onExit;
        

        public BasicState(int id, string name, ExecuteActionHandler onExecute, EnterActionHandler onEnter = null, ExitActionHandler onExit = null )
        {
            this.Id = id;
            this.Name = name;
            this.onExecute = onExecute != null ? onExecute : null;
            this.onEnter = onEnter != null ? onEnter : null;
            this.onExit = onExit != null ? onExit : null;         
        }

        public override int Enter()
        {
            //======================================//

            if (this.onEnter == null) return FSMInnerErrorCode.NoError;
            return this.onEnter.Invoke();

            //======================================//
        }

        public override int Execute()
        {
            int errorCode = FSMInnerErrorCode.NoError;
            this.status = FSMStatus.None;
            do
            {
              //===================================//
                this.status = FSMStatus.Enter;
                errorCode = this.Enter();
                if (errorCode != FSMInnerErrorCode.NoError || Flag.IsStop()) break;
                this.status = FSMStatus.EnterDone;
              //===================================//

              //===================================//
                this.status = FSMStatus.Enter;
                if (this.onExecute == null)
                    errorCode = FSMInnerErrorCode.NotExecute;
                else
                    errorCode = this.onExecute.Invoke();

                if (errorCode != FSMInnerErrorCode.NoError || Flag.IsStop()) break;
                this.status = FSMStatus.EnterDone;
              //===================================//

              //===================================//
                this.status = FSMStatus.Exit;
                errorCode = this.Exit();
                if (errorCode != FSMInnerErrorCode.NoError || Flag.IsStop()) break;
                this.status = FSMStatus.EnterDone;
              //===================================//

            } while (false);

            return errorCode;            
        }

        public override int Exit()
        {
           //=====================================//
            if (this.onExit == null) return FSMInnerErrorCode.NoError;
            return this.onExit.Invoke();
          //=====================================//
        }
    }
}
