using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LX_FSM;
using LX_MachineMonitor;
using Test_FSM_WPF.Data;
using Test_FSM_WPF.Station;
using Test_FSM_WPF.Utility;


namespace Test_FSM_WPF.Model
{
    public class MainManager
    {
        public FiniteStateMachineController FsmController { get; private set; }
        public MachineMonitorController MonitorController { get; private set; }
        public BillBoardData BillBoard { get; private set; }
        public MachineEvent MachineEvent { get; private set; }
        public IErrorActionHandler ErrorWindowHandler { get; private set; }

        public MainManager()
        {
            CreateObjects();
        }

     //===============================================//
        private void CreateObjects()
        {
            FsmController = new FiniteStateMachineController();
            MonitorController = new MachineMonitorController();
            BillBoard = new BillBoardData();
            MachineEvent = new MachineEvent();
            ErrorWindowHandler = new CustomWindow();

            InitializeErrorMap();
        }
     //===============================================//
       void InitializeErrorMap()
        {
            StationCustomErrorCode.BuildErrorMap();
        }
    }
}
