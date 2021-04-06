using Stanley_FSM.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanley_FSM.Machine
{
    public interface IFiniteStateMachine
    {
        string Name { get; set; }
        void SetStateChain();
        void ResetMachineEvent();
        void Stop();
        void Reset();
        void CreateFSMStateInstance();
        void SetFirstState(IState state);
        void SetFinalState(IState state);
        void SetCurrentState(IState state);
        void ResetCurrentStateToFirsetState();
        int RunStateChain(FSMRunMode runMode);
        List<IState> FSMStates { get; set; }
    }
}
