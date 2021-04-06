using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LX_FSM
{
    public enum SingleStateStatus
    {
        Idle,
        Busy
    }

    public enum FSMStatus
    {
        None,
        Enter,
        EnterDone,
        Execute,
        ExecuteDone,
        Exit,
        ExitDone
    }

    public enum FSMState
    {
        SingleState,
        Auto,
        OneCycle
    }

    public enum StateMachineStatus
    {
        Auto,
        SingleState,
        OneCycle
    }

    public enum StateMachineType
    {
        Initilize,
        Auto,
        Reset
    }
}
