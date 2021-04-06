using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanley_FSM.Machine
{
    public static class Helper
    {
        public static List<IState> Sort(this List<IState> states, StateMachine machine)
        {
            List<IState> templist = new List<IState>();
            templist.Add(machine.FinalState);
            IState curState = templist[0];
            while (true)
            {

                templist.Add(curState.NextState);
                curState = curState.NextState;

                if (curState == machine.FinalState)
                    break;

            }

            return templist;
        }
    }
}
