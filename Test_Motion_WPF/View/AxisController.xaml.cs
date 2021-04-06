using LX_MCPNet.Data;
using LX_MCPNet.Motion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Test_Motion_WPF.View
{
    /// <summary>
    /// AxisController.xaml 的交互逻辑
    /// </summary>
    public partial class AxisController : UserControl, INotifyPropertyChanged
    {
        public AxisController()
        {
            InitializeComponent();
        }
        AxisBase ax = null;
        MtrPosSpeed mtrPosSpeed = null;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        ObservableCollection<double> _AxisList = new ObservableCollection<double>()
        {
            0.001,
            0.002,
            0.005,
            0.01,
            0.02,
            0.05,
            0.1,
            0.2,
            0.5,
            1,
            2,
            5,
            10
        };
        ObservableCollection<double> _AxisZList = new ObservableCollection<double>()
        {
           0.006,
           0.012,
           0.018,
           0.030,
           0.06,
           0.12,
           0.18,
           0.30,
           0.6,
           1.2,
           1.8,
           3,
           6,
           12,
           18,
           30
        };
        ObservableCollection<double> _AxisTList = new ObservableCollection<double>()
        {
           1,
           2,
           5,
           10,
           15,
           30,
           60,
           90,
           120,
           180,
           360
        };


        string _AxisName = string.Empty;
        bool canUpdate = false;
        public bool CanUpdate
        {
            get { return canUpdate; }
            set
            {
                canUpdate = value;
                OnPropertyChanged(nameof(CanUpdate));
            }
        }

        double stepValue = 0;
        public double StepValue
        {
            get { return stepValue; }
            set
            {
                stepValue = value;
                AssignPropertyValue(value);
                OnPropertyChanged(nameof(StepValue));
            }
        }

        double absoluteValue = 0;
        public double AbsoluteValue
        {
            get { return absoluteValue; }
            set
            {
                absoluteValue = value;
                AssignAbsoluteValue(value);
                OnPropertyChanged(nameof(AbsoluteValue));
            }
        }

        public void Setup(AxisBase ax, MtrPosSpeed mtrPosSpeed)
        {
            this.ax = ax;
            this.mtrPosSpeed = mtrPosSpeed;

            this.DataContext = this;
            recPlusLimitStatus.DataContext = ax;
            recNegLimitStatus.DataContext = ax;
            this.UI_Lbl_Name_Axis.Content = this.ax.MtrTable.Name;
            this._AxisName = this.ax.MtrTable.Name;

            this.ax.PropertyChanged -= Ax_PropertyChanged;
            this.ax.PropertyChanged += Ax_PropertyChanged;

            SetDataBinding(UI_Lbl_Pos_Axis, mtrPosSpeed);
           
        }

        private void SetDataBinding(TextBox lbl, MtrPosSpeed pos)
        {
            Binding b = new Binding();
            b.Path = new PropertyPath(nameof(pos.Pos));
            b.Source = pos;
            b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            b.Mode = BindingMode.TwoWay;
            lbl.SetBinding(TextBox.TextProperty, b);
        }


        private void Ax_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CanMotorMove")
            {

            }
     
        }

        
      
        public string ConfiguredFeatureName { get; set; }


        string UnitMeasurement()
        {
            return "(mm)";
        }
        private void SetupComboBoxItemSource()
        {
            cmb_Step.ItemsSource = _AxisList;
            cmb_Step.SelectedItem = _AxisList[0];
        }


        private void UI_Btn_Pos_Go_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void AssignPropertyValue(double value)
        {
            
        }

        private void AssignAbsoluteValue(double value)
        {
         
        }

        private void Cmb_Step_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StepValue = (double)cmb_Step.SelectedItem;
            AssignPropertyValue(StepValue);
        }

        private void Btn_Left_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Btn_Right_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Btn_Go_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void UI_Btn_SingleUpdate_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Btn_Home_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
