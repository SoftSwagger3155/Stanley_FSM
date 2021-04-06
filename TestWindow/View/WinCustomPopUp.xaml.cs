using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Test_FSM_WPF.View
{
    /// <summary>
    /// WinCustomPopUp.xaml 的交互逻辑
    /// </summary>
    public partial class WinCustomPopUp : Window
    {
        public enum ActionResult
        {
            Ok,
            No,
            None
        }

        public ActionResult Result { get; private set; } = ActionResult.None;

        public WinCustomPopUp(string msg)
        {
            InitializeComponent();
            this._lbl_Msg.Content = msg;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_OK_Click(object sender, RoutedEventArgs e)
        {
            this.Result = ActionResult.Ok;
            this.Close();
        }

        private void Btn_No_Click(object sender, RoutedEventArgs e)
        {
            this.Result = ActionResult.No;
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }
    }
}
