using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LX_FSM;
using Flag = LX_MachineMonitor.SystemFlag;

namespace Test_FSM_Console
{
    public class Station_2: FiniteStateMachine
    {
        BasicState basicState_1 = null;
        BasicState basicState_2 = null;
        BasicState basicState_3 = null;

        private void CreateBasicStateObject()
        {
            int index = 0;
            basicState_1 = new BasicState(index++, "Station2_BasicState_1", Station_2_BasicState_1);
            basicState_2 = new BasicState(index++, "Station2_BasicState_2", Station_2_BasicState_2);
            basicState_3 = new BasicState(index++, "Station2_BasicState_3", Station_2_BasicState_3);
        }
        /*===============================================*/

        public int Station_2_BasicState_1()
        {
            int errorCode = FSMInnerErrorCode.NoError;

            do
            {
                bool ok = MachineEvent.SwitchStation.WaitOne(10);
                if (!ok)
                {
                    errorCode = FSMInnerErrorCode.Repeat;
                    Thread.Sleep(10);
                    break;
                }
                else
                {
                    Console.WriteLine("Wait For Station1");
                    Console.WriteLine("Station_2_BasicState_1");
                    errorCode = FSMInnerErrorCode.NoError;

                    //Monitor.Stop();
                    break;
                }

            } while (false);

            return errorCode;
        }


        public int Station_2_BasicState_2()
        {
            int errorCode = FSMInnerErrorCode.NoError;

            do
            {
                Console.WriteLine("Station_2 BasicState_2");

            } while (false);

            return errorCode;
        }

        public  int Station_2_BasicState_3()
        {
            int errorCode = FSMInnerErrorCode.NoError;

            do
            {
                Console.WriteLine("Station_2 BasicState_3");

            } while (false);

            return errorCode;
        }


        /*===============================================*/
        public Station_2()
        {
            this.Name = this.GetType().Name;
            this.CreateBasicStateObject();
            this.SetStateChain();
        }


        public override void SetStateChain()
        {
            try
            {
                this.SetFirstState(basicState_1);
                this.SetFinalState(basicState_3);
                FSMHelper.SetStateChain(basicState_1, basicState_2);
                FSMHelper.SetStateChain(basicState_2, basicState_3);
            }
            catch
            {
                Console.WriteLine("Set Chain Error");
            }
        }
    }
}
