using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stanley_FSM;
using Stanley_FSM.Machine;
using Stanley_FSM.Monitor;
using Test_Stanley_FSM.Data;
using Test_Stanley_FSM.FSM;

namespace Test_Stanley_FSM.Model
{
    public class MainManager
    {
        StateMachineController machineController = null;
        public StateMachineController MachineController
        {
            get { return machineController; }
        }

        MachineMonitor monitor = null;
        public MachineMonitor Monitor
        {
            get { return monitor; }
        }

        BillBoard board = null;
        public BillBoard Board
        {
            get { return board; }
        }

        public MainManager()
        {
            CreateInstance();
            BuildStateMachine();
        }
        

        private void CreateInstance()
        {
            board = new BillBoard();
            monitor = new MachineMonitor();
            machineController = new StateMachineController(monitor);
        }

        private void BuildStateMachine()
        {

            machineController.Add(
                                                  new StationA("Station A", monitor, board),
                                                  new StationB("Station B", monitor, board),
                                                  new StationC("Station C", monitor, board)
                                                  );
        }
    }
}
