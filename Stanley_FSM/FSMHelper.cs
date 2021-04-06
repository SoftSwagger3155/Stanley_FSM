using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanley_FSM
{
    public static class FSMHelper
    {
        public static void SetStateChain(this IState currentState, IState nextState)
        {
            //currentState.SetNextState(nextState);
            //nextState.SetPreviousState(currentState);

            currentState.NextState = nextState;
            nextState.PreviousState = currentState;
        }

    }
}
