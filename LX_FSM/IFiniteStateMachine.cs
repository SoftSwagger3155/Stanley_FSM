using LX_MachineMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LX_FSM
{
    public interface IFiniteStateMachine
    {
        string Name { get; set; }
        void SetStateChain();
        void ResetFlag();
        int RunStateChain(StateMachineStatus status);
    }
}
