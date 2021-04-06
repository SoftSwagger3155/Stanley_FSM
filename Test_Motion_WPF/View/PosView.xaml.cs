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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using LX_MCPNet.Motion;

namespace Test_Motion_WPF.View
{
    /// <summary>
    /// PosView.xaml 的交互逻辑
    /// </summary>
    public partial class PosView : UserControl, INotifyPropertyChanged
    {
        public PosView()
        {
            InitializeComponent();
        }

        AxisBase ax = null;
        public string MotorPosStr
        {
            get { return string.Format("{0}", ax.CurrentPhysicalPos.ToString("F3")); }
        }


        public void Setup(AxisBase ax)
        {
            this.ax = ax;
            this.DataContext = this;
            lblTag.Content = ax.MtrTable.Name;
            OnPropertyChanged(nameof(MotorPosStr));
            ax.PropertyChanged -= Ax_PropertyChanged;
            ax.PropertyChanged += Ax_PropertyChanged;
        }

        private void Ax_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentPhysicalPos" || e.PropertyName == "IsSimulation" ||
               e.PropertyName == "IsServoOn" || e.PropertyName == "IsBusy")
            {
                OnPropertyChanged(nameof(MotorPosStr));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }
    }
}
