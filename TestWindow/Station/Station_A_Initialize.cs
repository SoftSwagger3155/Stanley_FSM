using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using LX_FSM;
using Test_FSM_WPF.Data;
using Test_FSM_WPF.Model;
using Test_FSM_WPF.Utility;
using Test_FSM_WPF.View;
using ErrorCode = Test_FSM_WPF.Utility.StationCustomErrorCode;

namespace Test_FSM_WPF.Station
{
    public class Station_A_Initialize: FiniteStateMachine
    {
        MainManager mmgr = null;
        BillBoardData billBoard = null;
        public BasicState state_1 { get; private set; }
        public BasicState state_2 { get; private set; }
        public BasicState state_3 { get; private set; }


        public Station_A_Initialize(MainManager mmgr)
        {
            this.Name = this.GetType().Name;
            this.mmgr = mmgr;

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
                //errorCode = ErrorCode.ActionNotTaken;
                //if(errorCode != FSMInnerErrorCode.NoError)
                //{
                //    mmgr.ErrorWindowHandler.DoErrorCodeAction(errorCode);
                //    errorCode = mmgr.ErrorWindowHandler.GetResponse() == "Ok" ? FSMInnerErrorCode.NoError : FSMInnerErrorCode.Error;
                //}

                //if (errorCode != FSMInnerErrorCode.NoError) break;

                if (this.StateMachineStatus != StateMachineStatus.SingleState)
                {
                    mmgr.BillBoard.MessageA = "A Started";
                    Thread.Sleep(1000);
                    mmgr.BillBoard.MessageA = "A 1";
                    Thread.Sleep(1000);
                }
                else
                {
                    mmgr.BillBoard.MessageA = "A 1";
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
                mmgr.BillBoard.MessageA = "A 2";
                Thread.Sleep(1000);
                mmgr.BillBoard.MessageA = "A Set B";
                Thread.Sleep(1000);
                mmgr.MachineEvent.SwitchCall_A.Set();

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
                bool ok = mmgr.MachineEvent.SwitchCall_B.WaitOne(10);

                if(ok)
                {
                    mmgr.BillBoard.MessageA = "Receive Call From B";
                    Thread.Sleep(1000);

                    mmgr.BillBoard.MessageA = "A 3";
                    Thread.Sleep(1000);

                    mmgr.BillBoard.MessageA = "A Set C";
                    Thread.Sleep(1000);
                    mmgr.MachineEvent.SwitchCall_C.Set();

                    mmgr.BillBoard.MessageA = "FSM A Finished";

                    errorCode = FSMInnerErrorCode.NoError;
                    break;
                }
                else
                {
                    errorCode = FSMInnerErrorCode.Repeat;
                    Thread.Sleep(10);
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
            mmgr.MachineEvent.ResetEventB();
        }
    }
}
