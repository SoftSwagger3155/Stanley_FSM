using System;
using System.Runtime.InteropServices;

namespace Stanley_MCPNet.IOCard
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class PortType
    {
        public string PortLabel { get; set; }
        public int PortNo { get; set; }
        public string Type { get; set; }
        public int OpInitValue { get; set; }
        public int TotalBit { get; set; }
        public PortType()
        {
            this.PortNo = -1;
            this.Type = "";
            this.PortLabel = "";
            this.OpInitValue = 0;
            this.TotalBit = 8;
        }

        public PortType(PortType portType)
        {
            this.PortNo = portType.PortNo;
            this.Type = portType.Type;
            this.PortLabel = portType.PortLabel;
            this.OpInitValue = portType.OpInitValue;
            this.TotalBit = 0;
        }

        public PortType(string portLabel, int portNo, string type, int opInitValue)
        {
            this.PortNo = portNo;
            this.Type = type;
            this.PortLabel = portLabel;
            this.OpInitValue = opInitValue;
            this.TotalBit = 0;
        }

        public PortType(int portNo, string type, int opInitValue)
        {
            this.PortNo = portNo;
            this.Type = type;
            this.PortLabel = "";
            this.OpInitValue = opInitValue;
            this.TotalBit = 0;
        }

        public PortType(string portLabel, int portNo, string type, int opInitValue, int totalBit)
        {
            this.PortNo = portNo;
            this.Type = type;
            this.PortLabel = portLabel;
            this.OpInitValue = opInitValue;
            this.TotalBit = totalBit;
        }
    }
}
