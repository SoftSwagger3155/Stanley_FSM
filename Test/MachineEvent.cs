using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test_FSM_Console
{
    public class MachineEvent
    {
        public static ManualResetEvent SwitchStation = new ManualResetEvent(false);
    }
}
