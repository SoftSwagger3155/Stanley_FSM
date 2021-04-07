using System;
using System.IO;
using System.Reflection;

namespace Stanley_Utility
{
    public class DllFileInfo
    {
        public string FileName
        {
            get
            {
                return this.fileName;
            }
        }

        public DateTime DateOfModified
        {
            get
            {
                return this.dateOfModified;
            }
        }

        public string DateTimeStr
        {
            get
            {
                return this.dateTimeStr;
            }
        }

        public string Version
        {
            get
            {
                return this.version;
            }
        }

        public string Architecture
        {
            get
            {
                return this.architecture;
            }
        }

        public bool HasRetrivedInfo
        {
            get
            {
                return this.hasRetrivedInfo;
            }
        }

        public DllFileInfo(string fileName, int counter, string dir = "")
        {
            this.fileName = fileName;
            fileName = fileName.Replace(".dll", "");
            this.dir = dir;
            this.counter = counter;
        }

        public void RetriveVersionInfo()
        {
            string text = this.fileName.Contains(".dll") ? this.fileName : (this.fileName + ".dll");
            string text2 = (this.dir == "") ? text : Path.Combine(this.dir, text);
            if (File.Exists(text2))
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(text2);
                    Version version = assembly.GetName().Version;
                    this.dateOfModified = assembly.GetLinkerTime(null);
                    this.dateTimeStr = this.dateOfModified.ToString("dd-MMM-yy [HH:mm]");
                    this.version = version.ToString();
                    AssemblyName assemblyName = AssemblyName.GetAssemblyName(text2);
                    this.processorArchitecture = assemblyName.ProcessorArchitecture;
                    this.architecture = assemblyName.ProcessorArchitecture.ToString();
                    this.hasRetrivedInfo = true;
                }
                catch (Exception ex)
                {
                    this.errorInfo = ex.Message;
                }
            }
            else
            {
                this.errorInfo = text2 + " not found";
            }
        }

        public bool Is64Bit()
        {
            return this.hasRetrivedInfo && (this.processorArchitecture == ProcessorArchitecture.MSIL || this.processorArchitecture == ProcessorArchitecture.IA64 || this.processorArchitecture == ProcessorArchitecture.Amd64);
        }

        public override string ToString()
        {
            string text = " " + this.fileName + " ";
            text = text.PadLeft(45, '-');
            text = text.PadRight(47, '-');
            string text2 = this.version.PadRight(11, ' ');
            return (!this.hasRetrivedInfo) ? string.Format("[{0:000}] {1} <{2}>\n", this.counter, text, this.errorInfo) : string.Format("[{4:000}] {0} <{1}> -- <{2}> -- <{3}>", new object[]
            {
                text,
                text2,
                this.dateTimeStr,
                this.architecture,
                this.counter,
                this.errorInfo
            });
        }

        public static DllFileInfo[] GetDllFileInfo(string[] fileNames, string dir = "")
        {
            DllFileInfo[] array = new DllFileInfo[fileNames.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = new DllFileInfo(fileNames[i], i + 1, dir);
                array[i].RetriveVersionInfo();
            }
            return array;
        }

        public static string[] CommonHelperDllFileNames = new string[]
        {
            "CommonConveters",
            "COMPorts",
            "Utilities",
            "CustomWindow",
            "DefaultPassword",
            "LogWritter",
            "MessageHandler",
            "TCPSever",
            "UserAccess",
            "StorageMonitorService"
        };

        private string fileName = "";

        private DateTime dateOfModified = DateTime.Now;

        private string dateTimeStr = "";

        private string version = "";

        private string architecture = "";

        private bool hasRetrivedInfo = false;

        private int counter = 0;

        private string dir = "";

        private ProcessorArchitecture processorArchitecture = ProcessorArchitecture.None;

        private string errorInfo = "";
    }
}
