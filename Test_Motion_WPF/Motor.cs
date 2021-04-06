using LX_MCPNet.Data;
using LX_MCPNet.Motion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Motion_WPF
{
    public class Motor
    {
        public const int TotalAxes = 3;
        public static bool EmergencyEvent()
        {
            return false;
        }

        bool simulation = false;
        public bool Simulation
        {
            get { return simulation; }
            set
            {
                simulation = value;
            }
        }
        public Motor(bool simulation)
        {
            this.simulation = simulation;
        }

        void AssignConfigObjects()
        {
            if (!IsLoadedConfigValid())
            {
                MtrData.InitMtrMiscArray(); return;
            }

            MtrData.InitMtrMiscArray(LoadedMics);

            int cnt = 0;
            MtrData.Axis_01_Config = LoadedConfig[cnt++];
            MtrData.Axis_02_Config = LoadedConfig[cnt++];
            MtrData.Axis_03_Config = LoadedConfig[cnt++];

            cnt = 0;
            MtrData.Axis_01_Tbl = LoadedTable[cnt++];
            MtrData.Axis_02_Tbl = LoadedTable[cnt++];
            MtrData.Axis_03_Tbl = LoadedTable[cnt++];

            cnt = 0;
            MtrData.Axis_01_Sp = LoadedSpeed[cnt++];
            MtrData.Axis_02_Sp = LoadedSpeed[cnt++];
            MtrData.Axis_03_Sp = LoadedSpeed[cnt++];
        }

        public AxisBase Axis_01 = null;
        public AxisBase Axis_02 = null;
        public AxisBase Axis_03 = null;


        public string Init( string acsIP)
        {
            //attach emergency event here
            int cnt = 0;

            //simulation = simulate;

            AssignConfigObjects();

            cnt = 0;
            Axis_01 = new SPiiPlus660(acsIP, false, ref MtrData.Axis_01_Config, ref MtrData.Axis_01_Tbl, ref MtrData.Axis_01_Sp, ref MtrData.MtrMiscArray[cnt++], simulation); 
            Axis_02 = new SPiiPlus660(acsIP, false, ref MtrData.Axis_02_Config, ref MtrData.Axis_02_Tbl, ref MtrData.Axis_02_Sp, ref MtrData.MtrMiscArray[cnt++], simulation);
            Axis_03 = new SPiiPlus660(acsIP, false, ref MtrData.Axis_03_Config, ref MtrData.Axis_03_Tbl, ref MtrData.Axis_03_Sp, ref MtrData.MtrMiscArray[cnt++], simulation);

            //and init
            return AxisBase.InitAllMotors();
        }

        public List<AxisBase> AllMotors
        {
            get { return AxisBase.MotorsList; }
        }

        public void StopAllMotors()
        {
            if (AxisBase.MotorsList == null) return;
            for (int i = 0; i < AxisBase.MotorsList.Count; i++)
                AxisBase.MotorsList[i].sd_stop();
        }



        public bool IsLoadedConfigValid()
        {

            bool valid = false;
            do
            {

                if (LoadedConfig == null || LoadedConfig.Count < TotalAxes) break;
                if (LoadedTable == null || LoadedTable.Count < TotalAxes) break;
                if (LoadedSpeed == null || LoadedConfig.Count < TotalAxes) break;
                if (LoadedMics == null || LoadedMics.Count < TotalAxes) break;

                valid = true;
            }
            while (false);

            return valid;
        }


        public List<MtrConfig> LoadedConfig = new List<MtrConfig>();
        public List<MtrTable> LoadedTable = new List<MtrTable>();
        public List<MtrSpeed> LoadedSpeed = new List<MtrSpeed>();
        public List<MtrMisc> LoadedMics = new List<MtrMisc>();

        public MtrConfig[] GetAllConfig()
        {
            MtrConfig[] arr = new MtrConfig[TotalAxes];
            for (int i = 0; i < AxisBase.MotorsList.Count; i++)
                arr[i] = AxisBase.MotorsList[i].MtrConfig;

            return arr;
        }
        public MtrTable[] GetAllTable()
        {
            MtrTable[] arr = new MtrTable[TotalAxes];
            for (int i = 0; i < AxisBase.MotorsList.Count; i++)
                arr[i] = AxisBase.MotorsList[i].MtrTable;

            return arr;
        }
        public MtrSpeed[] GetAllSpeed()
        {
            MtrSpeed[] arr = new MtrSpeed[TotalAxes];
            for (int i = 0; i < AxisBase.MotorsList.Count; i++)
                arr[i] = AxisBase.MotorsList[i].MtrSpeed;

            return arr;
        }
        public MtrMisc[] GetAllMics()
        {
            MtrMisc[] arr = new MtrMisc[TotalAxes];
            for (int i = 0; i < AxisBase.MotorsList.Count; i++)
                arr[i] = AxisBase.MotorsList[i].MtrMisc;

            return arr;
        }

        public void StopAllStatusReading()
        {
            if (AxisBase.MotorsList == null) return;
            for (int i = 0; i < AxisBase.MotorsList.Count; i++)
                AxisBase.MotorsList[i].StopStatusReadThread();
        }

        public void DisableAll()
        {
            if (AxisBase.MotorsList == null) return;
            for (int i = 0; i < AxisBase.MotorsList.Count; i++)
            {
                try
                {
                    AxisBase.MotorsList[i].set_servo(false);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
