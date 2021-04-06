using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LX_FSM;
using LX_MachineMonitor;
using Flag = LX_MachineMonitor.SystemFlag;

namespace Test_FSM_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Station_1 stn_1 = new Station_1() { };
            Station_2 stn_2 = new Station_2(){ };
            Console.WriteLine("================Start================");
            List<FiniteStateMachine> allStations = new List<FiniteStateMachine>();
            allStations.Add(stn_1);
            allStations.Add(stn_2);
            Parallel.ForEach(allStations, FiniteStateMachine => { FiniteStateMachine.RunStateChain(StateMachineStatus.OneCycle); });
            Console.WriteLine("Stop 1st Time");
            Thread.Sleep(300);
            Console.WriteLine("Resume");
            var curStateS1 = stn_1.Machine.CurrentState;
            var curStateS2 = stn_2.Machine.CurrentState;
            Parallel.ForEach(allStations, FiniteStateMachine => { FiniteStateMachine.RunStateChain(StateMachineStatus.OneCycle); });
            //stn_1.RunStateChain(FSMStatus.OneCycle);
            Console.WriteLine("================End==================");
            Console.ReadKey();
        }
    }
}
