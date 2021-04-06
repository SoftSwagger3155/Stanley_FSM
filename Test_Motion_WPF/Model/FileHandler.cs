using LX_MCPNet.Data;
using LX_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Motion_WPF.Model
{
    public class FileHandler
    {
        public static void SaveAxesConfig(MtrConfig[] arr)
        {
            string path = SystemPath.GetSystemPath;
            path += "\\" + SystemPath.AxesConfigFileName;
            XmlHelper.SerializeFile<MtrConfig[]>(path, arr);
        }

        public static void SaveAxesTable(MtrTable[] arr)
        {
            string path = SystemPath.GetSystemPath;
            path += "\\" + SystemPath.AxesTableFileName;
            XmlHelper.SerializeFile<MtrTable[]>(path, arr);
        }

        public static void SaveAxesSpeed(MtrSpeed[] arr)
        {
            string path = SystemPath.GetSystemPath;
            path += "\\" + SystemPath.AxesSpeedFileName;
            XmlHelper.SerializeFile<MtrSpeed[]>(path, arr);
        }

        public static void SaveAxesMisc(MtrMisc[] arr)
        {
            string path = SystemPath.GetSystemPath;
            path += "\\" + SystemPath.AxesMiscFileName;
            XmlHelper.SerializeFile<MtrMisc[]>(path, arr);
        }

        public static List<MtrConfig> LoadAxesConfig()
        {
            string path = SystemPath.GetSystemPath;
            path += "\\" + SystemPath.AxesConfigFileName;
            var fileEle = XmlHelper.LoadXmlFile(path);
            MtrConfig[] tmpData = XmlHelper.DeserializeFile<MtrConfig[]>(path);

            return tmpData.ToList();
        }

        public static List<MtrTable> LoadAxesTable()
        {
            string path = SystemPath.GetSystemPath;
            path += "\\" + SystemPath.AxesTableFileName;
            MtrTable[] tmpData = XmlHelper.DeserializeFile<MtrTable[]>(path);
            return tmpData.ToList();
        }

        public static List<MtrSpeed> LoadAxesSpeed()
        {
            string path = SystemPath.GetSystemPath;
            path += "\\" + SystemPath.AxesSpeedFileName;
            MtrSpeed[] tmpData = XmlHelper.DeserializeFile<MtrSpeed[]>(path);
            return tmpData.ToList();
        }

        public static List<MtrMisc> LoadAxesMisc()
        {
            string path = SystemPath.GetSystemPath;
            path += "\\" + SystemPath.AxesMiscFileName;
            MtrMisc[] tmpData = XmlHelper.DeserializeFile<MtrMisc[]>(path);
            return tmpData.ToList();
        }
    }
}
