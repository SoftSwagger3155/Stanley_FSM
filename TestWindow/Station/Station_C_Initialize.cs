using LX_FSM;
using Test_FSM_WPF.Data;
using Test_FSM_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Test_FSM_WPF.Station
{
    public class Station_C_Initialize : FiniteStateMachine
    {
        MainManager mmgr = null;
        BillBoardData billBoard = null;
        public BasicState state_1 { get; private set; }
        public BasicState state_2 { get; private set; }
        public BasicState state_3 { get; private set; }


        public Station_C_Initialize(MainManager mmgr)
        {
            this.Name = this.GetType().Name;
            this.mmgr = mmgr;
            this.billBoard = mmgr.BillBoard;

            CreateStateObject();
            SetStateChain();
        }

        private void CreateStateObject()
        {
            int index = 0;
            state_1 = new BasicState(index++, $"{this.Name}{nameof(state_1)}", State_1_Action); this.StatePool.Add(state_1);
            state_2 = new BasicState(index++, $"{this.Name}{nameof(state_2)}", State_2_Action); this.StatePool.Add(state_2);
            state_3 = new BasicState(index++, $"{this.Name}{nameof(state_3)}", State_3_Action); this.StatePool.Add(state_3);
        }


        //=====================================================//
        private int State_1_Action()
        {
            int errorCode = FSMInnerErrorCode.NoError;

            do
            {

                bool ok = mmgr.MachineEvent.SwitchCall_C.WaitOne();

                if (ok)
                {
                    mmgr.BillBoard.MessageC = "Receive Call From A";
                    Thread.Sleep(1000);
                    mmgr.BillBoard.MessageC = "C 1";
                    errorCode = FSMInnerErrorCode.NoError;
                    Thread.Sleep(1000);
                }
                else
                {
                    errorCode = FSMInnerErrorCode.Repeat;
                    Thread.Sleep(10);
                    break;
                }


            } while (false);

            return errorCode;
        }
        //=====================================================//

        //=====================================================//
        private int State_2_Action()
        {
            int errorCode = FSMInnerErrorCode.NoError;

            do
            {

                mmgr.BillBoard.MessageC = "C 2";
                if (this.StateMachineStatus != StateMachineStatus.SingleState)
                    Thread.Sleep(1000);


            } while (false);

            return errorCode;
        }
        //=====================================================//

        //=====================================================//
        private int State_3_Action()
        {
            int errorCode = FSMInnerErrorCode.NoError;

            do
            {
                if(this.StateMachineStatus == StateMachineStatus.SingleState)
                {
                    mmgr.BillBoard.MessageC = "C 3";
                }
                else
                {
                    mmgr.BillBoard.MessageC = "C 3";
                    Thread.Sleep(1000);
                    mmgr.BillBoard.MessageC = "FSM C Finished";
                }

            } while (false);

            return errorCode;
        }
        //=====================================================//

        //=====================================================//
        public override void SetStateChain()
        {
            this.SetFirstState(state_1);
            this.SetFinalState(state_3);

            FSMHelper.SetStateChain(state_1, state_2);
            FSMHelper.SetStateChain(state_2, state_3);
            FSMHelper.SetStateChain(state_3, state_1);
        }
        //=====================================================//

        public override void ResetFlag()
        {
            mmgr.MachineEvent.ResetEventC();
        }
    }
}
