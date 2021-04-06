using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Test_Stanley_FSM.Model;
using Stanley_FSM.UI;

namespace Test_Stanley_FSM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        MainManager mmgr = null;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mmgr = new MainManager();
            this.DataContext = mmgr.Board;
        }

        private void _btn_Test_A_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mmgr.MachineController.RunSingleStateAction("A State1");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void _btn_Test_B_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mmgr.MachineController.RunSingleStateAction("B State2");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void _btn_Test_C_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mmgr.MachineController.RunSingleStateAction("C State3");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void _btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            if (mmgr.Monitor.RunMode == Stanley_FSM.Monitor.FSMRunMode.OneCycle)
                mmgr.MachineController.StopSingleCycle();
            else if (mmgr.Monitor.RunMode == Stanley_FSM.Monitor.FSMRunMode.Auto)
                mmgr.MachineController.StopAutoCycle();
         
        }

        private void _btn_Auto_Click(object sender, RoutedEventArgs e)
        {
            mmgr.MachineController.RunAutoCycle();
        }

        private void _btn_RunFSM_Click(object sender, RoutedEventArgs e)
        {
            mmgr.MachineController.RunSingleCycle();
        }

        private void RunSingleCycle()
        {
            mmgr.MachineController.RunStateChain(Stanley_FSM.Monitor.FSMRunMode.OneCycle);
        }

        private void _btn_OpenStateWindnow_Click(object sender, RoutedEventArgs e)
        {


            for (int i = 0; i < mmgr.MachineController.StateMachines.Count; i++)
            {
                WinFSMStateList win = new WinFSMStateList();
                List<CtrlFSMState> ctrlStates = new List<CtrlFSMState>();
                for (int j = 0; j < mmgr.MachineController.StateMachines[i].FSMStates.Count; j++)
                {
                    CtrlFSMState ctrlstate = new CtrlFSMState();
                    ctrlstate.Setup(mmgr.MachineController.StateMachines[i].FSMStates[j]);
                    ctrlStates.Add(ctrlstate);
                }
                win.Setup(ctrlStates);
                win.Show();
            }

           
        }
    }
}
