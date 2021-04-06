using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Test_FSM_WPF.View;
using LX_MachineMonitor;

namespace Test_FSM_WPF.Utility
{
    public class CustomWindow: IErrorActionHandler
    {    
        public override void DoAction()
        {
            ShowCustomWindow();
        }

        public void  ShowCustomWindow()
        {
           
            App.Current.Dispatcher.Invoke((Action) (()=>
            {
                WinCustomPopUp popUp = null;
                try
                {
                    popUp = new WinCustomPopUp(this.message);
                    popUp.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + Environment.NewLine + "Handling Active Align Action Failed");
                }
                finally
                {
                    this.response = popUp.Result.ToString();
                }
            }));
        }
    }
}
