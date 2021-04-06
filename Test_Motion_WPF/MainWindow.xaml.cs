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
using LX_MCPNet.Data;
using LX_Utility;
using Test_Motion_WPF.Model;

namespace Test_Motion_WPF
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


        Motor mtr = null;
        bool simulate = true;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                mtr = new Motor(false);
                SystemPath.CreateDefaultDirectory();
                CheckMotorConfigFile();
                CheckMotorTableFile();
                CheckMotorSpeedFile();
                CheckMotorMiscFile();

                LoadMotorConfig();
                mtr.Init("10.0.0.100");
               
                SetupPanel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void SetupPanel()
        {
            _axisController_01.Setup(mtr.Axis_01, new MtrPosSpeed());
            _posView_01.Setup(mtr.Axis_01);
        }

        public string LoadMotorConfig()
        {
            string serr = "";
            try
            {
                mtr.LoadedMics.Clear();
                mtr.LoadedSpeed.Clear();
                mtr.LoadedTable.Clear();
                mtr.LoadedConfig.Clear();

                mtr.LoadedConfig = FileHandler.LoadAxesConfig();
                mtr.LoadedTable = FileHandler.LoadAxesTable();
                mtr.LoadedSpeed = FileHandler.LoadAxesSpeed();
                mtr.LoadedMics = FileHandler.LoadAxesMisc();
                //FileHandler.LoadAxesConfig(Motor.TotalAxes, mtr.LoadedConfig);
                //FileHandler.LoadAxesMisc(Motor.TotalAxes, mtr.LoadedMics);
                //FileHandler.LoadAxesSpeed(Motor.TotalAxes, mtr.LoadedSpeed);
                //FileHandler.LoadAxesTable(Motor.TotalAxes, mtr.LoadedTable);
            }
            catch (Exception ex)
            {
                serr = ex.Message;
            }
            return serr;
        }       

        void CheckMotorConfigFile()
        {
            try
            {
                var tempConfig = FileHandler.LoadAxesConfig();

            }
            catch
            {
                    CreateDefaultMotorConfig();
            }
        }
        void CheckMotorTableFile()
        {
            try
            {
                var tempConfig = FileHandler.LoadAxesTable();

            }
            catch
            {
                CreateDefaultMotorTable();
            }
        }
        void CheckMotorSpeedFile()
        {
            try
            {
                var tempConfig = FileHandler.LoadAxesSpeed();

            }
            catch
            {
                CreateDefaultMotorSpeed();
            }
        }
        void CheckMotorMiscFile()
        {
            try
            {
                var tempConfig = FileHandler.LoadAxesMisc();

            }
            catch
            {
                CreateDefaultMotorMisc();
            }
        }
        void CreateDefaultMotorConfig()
        {
            List<MtrConfig> tempList = new List<MtrConfig>();
            for (int i = 0; i < 3; i++)
            {
                tempList.Add(new MtrConfig { home_mode = (short)(i+1)});
            }

            FileHandler.SaveAxesConfig(tempList.ToArray());
        }
        void CreateDefaultMotorTable()
        {
            List<MtrTable> tempList = new List<MtrTable>();
            for (int i = 0; i < 3; i++)
            {
                tempList.Add(new MtrTable { AxisNo = (short)(i+1)});
            }

            FileHandler.SaveAxesTable(tempList.ToArray());
        }
        void CreateDefaultMotorSpeed()
        {
            List<MtrSpeed> tempList = new List<MtrSpeed>();
            for (int i = 0; i < 3; i++)
            {
                tempList.Add(new MtrSpeed { AcsJerk = (double)(i+1) });
            }

            FileHandler.SaveAxesSpeed(tempList.ToArray());
        }
        void CreateDefaultMotorMisc()
        {
            List<MtrMisc> tempList = new List<MtrMisc>();
            for (int i = 0; i < 3; i++)
            {
                tempList.Add(new MtrMisc { UnitName = (i+1).ToString()});
            }

            FileHandler.SaveAxesMisc(tempList.ToArray());
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            mtr.Axis_01.MtrTable.Name = "Axis_01";
            mtr.Axis_02.MtrTable.Name = "Axis_02";
            mtr.Axis_03.MtrTable.Name = "Axis_03";

            FileHandler.SaveAxesConfig(mtr.LoadedConfig.ToArray());
            FileHandler.SaveAxesMisc(mtr.LoadedMics.ToArray());
            FileHandler.SaveAxesSpeed(mtr.LoadedSpeed.ToArray());
            FileHandler.SaveAxesTable(mtr.LoadedTable.ToArray());
        }
    }
}
