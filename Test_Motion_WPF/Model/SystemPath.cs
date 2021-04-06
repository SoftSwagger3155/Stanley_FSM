using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Motion_WPF.Model
{
    public class SystemPath
    {
        public static string RootDirectory = Directory.GetCurrentDirectory();
        public static string RootInfoDirection = "C:\\LX";
        public static string RootLogDirectory = "C:\\LX";

        public const string RecipeDirectory = "Recipe";
        public const string LastCntDirectory = "LastCnt";
        public const string LogDirectory = "Log";
        public const string DataLogDirectory = "DataLog";
        public const string LotDirectory = "Lot";
        public const string SystemDirectory = "System";
        public const string WorkOrderDirectory = "WorkOrder";
        public const string ControlDirectory = "Control";
        public const string TimingLogDirectory = "TimingLog";
        public const string IconDirectory = "Icons";
        public const string LotExt = ".csv";
        public const string VisExt = "mvp";
        public const string ControlExt = "mcp";
        public const string BatExt = ".txt";
        public const string LogExt = ".log";

        public const string ConfigFileName = "Config.xml";

        public const string AxesConfigFileName = "AxesConfig.xml";
        public const string AxesSpeedFileName = "AxesSpeed.xml";
        public const string AxesTableFileName = "AxesTable.xml";
        public const string AxesMiscFileName = "AxesMisc.xml";

        public const string TowerLightFileName = "TowerLightConfig.xml";
        public const string OtherDataFileName = "OtherData.xml";

        public const string IoCardFileName = "IoCard.xml";
        public const string InputFileName = "Input.xml";
        public const string OutputFileName = "Output.xml";

        public const string PreWeldListName = "PreWeldPoint.xml";
        public const string PstWeldListName = "PostWeldPoint.xml";

        static public string GetTimingPath
        {
            get { return RootLogDirectory + "\\" + TimingLogDirectory; }
        }

        static public string GetRecipePath
        {
            get { return RootInfoDirection + "\\" + RecipeDirectory; }
        }

        static public string GetControlRecipePath
        {
            get { return RootInfoDirection + "\\" + RecipeDirectory + "\\" + ControlDirectory; }
        }

        static public string GetSystemPath
        {
            get { return RootDirectory + "\\" + SystemDirectory; }
        }

        static public string GetLastCntPath
        {
            get { return RootInfoDirection + "\\" + LastCntDirectory; }
        }

        static public string GetWorkOrderPath
        {
            get { return RootInfoDirection + "\\" + WorkOrderDirectory; }
        }

        static public string GetLogPath
        {
            get { return RootLogDirectory + "\\" + LogDirectory; }
        }

        static public string GetDataLogPath
        {
            get { return RootLogDirectory + "\\" + DataLogDirectory; }
        }

        static public string GetLotPath
        {
            get { return RootInfoDirection + "\\" + LotDirectory; }
        }

        public string LoadedSystemPath
        {
            get { return GetSystemPath; }
        }

        public static  void CreateDefaultDirectory(bool excludelogs = false)
        {
            string path = string.Empty;
            CreateDirectoryIfDontHave(SystemPath.GetSystemPath);

            path = SystemPath.GetSystemPath + "\\" + SystemPath.AxesConfigFileName;
            CreateFileIFDontHave(path);
            path = SystemPath.GetSystemPath + "\\" + SystemPath.AxesTableFileName;
            CreateFileIFDontHave(path);
            path = SystemPath.GetSystemPath + "\\" + SystemPath.AxesSpeedFileName;
            CreateFileIFDontHave(path);
            path = SystemPath.GetSystemPath + "\\" + SystemPath.AxesMiscFileName;
            CreateFileIFDontHave(path);

            if (!excludelogs)
                CreateDirectoryIfDontHave(GetTimingPath);
            if (!excludelogs)
                CreateDirectoryIfDontHave(GetLogPath);
        }


        private static void CreateDirectoryIfDontHave(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static void CreateFileIFDontHave(string path)
        {
            if (!File.Exists(path))
                File.Create(path).Close();
        }
    }
}
