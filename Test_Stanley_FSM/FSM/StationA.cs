using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Stanley_FSM;
using Stanley_FSM.Machine;
using Test_Stanley_FSM.Data;
using Stanley_FSM.Monitor;

namespace Test_Stanley_FSM.FSM
{
    public class StationA: FiniteStateMachine
    {
        BasicState state1 = null;
        BasicState state2 = null;
        BasicState state3 = null;

        BillBoard board = null;
        IMachineMonitor monitor = null;
        public StationA(string name, IMachineMonitor monitor, BillBoard board): base(name, monitor)
        {
            this.TestDelay = 1000;
            AssignData(board);
            CreateFSMStateInstance();
            SetStateChain();
            this.monitor = monitor;
        }

  
        public void AssignData(BillBoard board)
        {
            this.board = board;
        }


        public override void CreateFSMStateInstance()
        {
            this.Add("A State1", DoState1, out state1);
            this.Add("A State2", DoState2, out state2);
            this.Add("A State3", DoState3, out state3);
        }

        private int DoState1()
        {
            int errorCode = FSMInnerErrorCode.NoError;

            do
            {
                board.MessageA = "A 1";

                if (monitor.RunMode != FSMRunMode.SingleState)
                    Thread.Sleep(TestDelay);
            } while (false);

            return errorCode;
        }

        private int DoState2()
        {
            int errorCode = FSMInnerErrorCode.NoError;

            do
            {
                board.MessageA = "A 2";

                if (monitor.RunMode != FSMRunMode.SingleState)
                    Thread.Sleep(TestDelay);
            } while (false);

            return errorCode;
        }

        private int DoState3()
        {
            int errorCode = FSMInnerErrorCode.NoError;

            do
            {
                board.MessageA = "A 3";

                if (monitor.RunMode != FSMRunMode.SingleState)
                    Thread.Sleep(TestDelay);
            } while (false);

            return errorCode;
        }

        public override void SetStateChain()
        {
            state1.SetStateChain(state2);
            state2.SetStateChain(state3);
            state3.SetStateChain(state1);
            //FSMHelper.SetStateChain(state1, state2);
            //FSMHelper.SetStateChain(state2, state3);
            //FSMHelper.SetStateChain(state3, state1);


            this.SetFirstState(state1);
            this.SetFinalState(state3);
        }

        public override void ResetMachineEvent()
        {
            
        }
    }
}
