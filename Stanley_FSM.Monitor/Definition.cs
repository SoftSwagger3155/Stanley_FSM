using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanley_FSM.Monitor
{
    public enum StateMachineRunStatus
    {
        Idle,
        Running,
        Stop,
        Done,
        Error
    }

    public enum FSMRunMode
    {
        SingleState,
        Auto,
        Reset,
        OneCycle
    }
}
