using LX_FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_FSM_WPF.Model;

namespace Test_FSM_WPF.Station
{
    public class StationController: FiniteStateMachineController
    {
        MainManager mmgr = null;
        public Station_A_Initialize stn_A_Init { get; private set; }
        public Station_B_Initialize stn_B_Init { get; private set; }
        public Station_C_Initialize stn_C_Init { get; private set; }

        public StationController(MainManager mmgr)
        {
            this.mmgr = mmgr;
            stn_A_Init = new Station_A_Initialize(mmgr);
            stn_B_Init = new Station_B_Initialize(mmgr);
            stn_C_Init = new Station_C_Initialize(mmgr);

            AssignInitFSM();
        }

       private void AssignInitFSM()
        {
            this.InitializeFSM.Add(stn_A_Init);
            this.InitializeFSM.Add(stn_B_Init);
            this.InitializeFSM.Add(stn_C_Init);
        }

    }
}
