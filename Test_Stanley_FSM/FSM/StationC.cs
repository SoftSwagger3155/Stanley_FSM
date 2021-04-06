using Stanley_FSM;
using Stanley_FSM.Machine;
using Stanley_FSM.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Test_Stanley_FSM.Data;

namespace Test_Stanley_FSM.FSM
{
    class StationC : FiniteStateMachine
    {
        BasicState state1 = null;
        BasicState state2 = null;
        BasicState state3 = null;

        BillBoard board = null;
        IMachineMonitor monitor = null;
        public StationC(string name, IMachineMonitor monitor, BillBoard board) : base(name, monitor)
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
            this.Add("C State1", DoState1, out state1);
            this.Add("C State2", DoState2, out state2);
            this.Add("C State3", DoState3, out state3);
        }

        private int DoState1()
        {
            int errorCode = FSMInnerErrorCode.NoError;

            do
            {
                board.MessageC = "C 1";
                
                if(monitor.RunMode != FSMRunMode.SingleState)
                    Thread.Sleep(TestDelay);

            } while (false);

            return errorCode;
        }

        private int DoState2()
        {
            int errorCode = FSMInnerErrorCode.NoError;

            do
            {
                board.MessageC = "C 2.1";
                errorCode = monitor.RunStatus == StateMachineRunStatus.Stop ? FSMInnerErrorCode.Stop : FSMInnerErrorCode.NoError;
                if (errorCode != FSMInnerErrorCode.NoError) break;
                Thread.Sleep(200);

                board.MessageC = "C 2.2";
                errorCode = monitor.RunStatus == StateMachineRunStatus.Stop ? FSMInnerErrorCode.Stop : FSMInnerErrorCode.NoError;
                if (errorCode != FSMInnerErrorCode.NoError) break;
                Thread.Sleep(200);

                board.MessageC = "C 2.3";
                errorCode = monitor.RunStatus == StateMachineRunStatus.Stop ? FSMInnerErrorCode.Stop : FSMInnerErrorCode.NoError;
                if (errorCode != FSMInnerErrorCode.NoError) break;
                Thread.Sleep(200);
                board.MessageC = "C 2.4";
                errorCode = monitor.RunStatus == StateMachineRunStatus.Stop ? FSMInnerErrorCode.Stop : FSMInnerErrorCode.NoError;
                if (errorCode != FSMInnerErrorCode.NoError) break;
                Thread.Sleep(200);
                board.MessageC = "C 2.5";
                errorCode = monitor.RunStatus == StateMachineRunStatus.Stop ? FSMInnerErrorCode.Stop : FSMInnerErrorCode.NoError;
                if (errorCode != FSMInnerErrorCode.NoError) break;

                if (errorCode != FSMInnerErrorCode.NoError) break;

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
                board.MessageC = "C 3";

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

            this.SetFirstState(state1);
            this.SetFinalState(state3);
        }

        public override void ResetMachineEvent()
        {

        }
    }
}
