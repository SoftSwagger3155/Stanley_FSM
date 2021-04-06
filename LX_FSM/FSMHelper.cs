using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LX_FSM
{
   public static class FSMHelper
    {      
        public static void SetStateChain(IState currentState, IState nextState)
        {
            currentState.SetNextState(nextState);
        }
    }
}
