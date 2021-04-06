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
using LX_FSM;
using LX_MachineMonitor;
using Test_FSM_WPF.Model;
using Test_FSM_WPF.Station;
using Test_FSM_WPF.View;

namespace Test_FSM_WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        MainManager mmgr = null;
        StationController stnController = null;
       

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mmgr = new MainManager();
            stnController = new StationController(mmgr);
           this.DataContext = mmgr.BillBoard;
        }

        private void _btn_RunFSM_Click(object sender, RoutedEventArgs e)
        {
            mmgr.BillBoard.MessageA = "Hello FSM A";
            mmgr.BillBoard.MessageB = "Hello FSM B";
            mmgr.BillBoard.MessageC = "Hello FSM C";


            Thread T1 = new Thread(RunInitChainTask);
            T1.IsBackground = true;
            T1.Start();
        }

        void RunInitChainTask()
        {
            try
            {
                stnController.InitializeFSM.ForEach(x => x.ResetFlag());
                stnController.Reset(StateMachineType.Initilize);
                stnController.RunFSMChain(StateMachineType.Initilize, StateMachineStatus.OneCycle);
            }
            catch
            {
                MessageBox.Show("Error: Run Init FSM Failed");
            }
        }

        private void _btn_Auto_Click(object sender, RoutedEventArgs e)
        {

            mmgr.BillBoard.MessageA = "Hello FSM A";
            mmgr.BillBoard.MessageB = "Hello FSM B";
            mmgr.BillBoard.MessageC = "Hello FSM C";


            Thread T1 = new Thread(RunAutoChainTask);
            T1.IsBackground = true;
            T1.Start();
        }

        private void RunAutoChainTask()
        {
            try
            {
                stnController.InitializeFSM.ForEach(x => x.ResetFlag());
                stnController.Reset(StateMachineType.Initilize);
                stnController.RunFSMChain(StateMachineType.Initilize, StateMachineStatus.Auto);
            }
            catch
            {
                MessageBox.Show("Error: Run Init FSM Failed");
            }
        }

        private void _btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SystemFlag.Stop();
            }
            catch
            {
            }
        }

        private void _btn_Test_A_Click(object sender, RoutedEventArgs e)
        {
            stnController.stn_A_Init.RunSingleState(stnController.stn_A_Init.state_1);
        }

        private void _btn_Test_B_Click(object sender, RoutedEventArgs e)
        {
            stnController.stn_B_Init.RunSingleState(stnController.stn_B_Init.state_2);
        }

        private void _btn_Test_C_Click(object sender, RoutedEventArgs e)
        {
            stnController.stn_C_Init.RunSingleState(stnController.stn_C_Init.state_3);
        }

        private void _btn_OpenStateWindnow_Click(object sender, RoutedEventArgs e)
        {
            WinFSMStateList win = new WinFSMStateList();
            win.Show();
        }
    }
}
