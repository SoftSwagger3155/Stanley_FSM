using Stanley_MCPNet.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Stanley_MCPNet.IOCard
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class IOCardObj : INotifyPropertyChanged
    {
        public int CardNo { get; set; }
        public string Vendor { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
        public int PortNum { get; set; }
        public int NumberOfInputPort { get; set; }
        public int NumberOfOutputPort { get; set; }
        public string IpAddress { get; set; }
        public int IpPort { get; set; }
        public bool Simulate { get; set; }
        public int StartOutputACSBuffer { get; set; }
        public int StopOutputACSBuffer { get; set; }
        [XmlIgnore]
        public bool HasInitialized
        {
            get
            {
                return this.hasInitialized;
            }
            set
            {
                this.hasInitialized = value;
                this.OnPropertyChanged("HasInitialized");
            }
        }

        [XmlIgnore]
        public IOBase Inst { get; set; }
        public IOCardObj()
        {
            this.Vendor = "";
            this.Model = "";
            this.PortNum = 0;
            this.CardNo = -1;
            this.IpAddress = "";
            this.IpPort = 0;
            this.Simulate = true;
            this.StartOutputACSBuffer = -1;
            this.StopOutputACSBuffer = -1;
            this.Port = new List<PortType>();
            this.ACSWagoVarNames = new List<string>();
            this.Name = string.Format("{0} {1} [{2}] {3}", new object[]
            {
                this.Vendor,
                this.Model,
                this.CardNo,
                this.PortNum
            });
        }

        public IOCardObj(int cardINo, string vendor, string model, int portNum, params PortType[] portType)
        {
            this.Vendor = vendor;
            this.Model = model;
            this.CardNo = cardINo;
            this.PortNum = portNum;
            this.Port = new List<PortType>();
            this.ACSWagoVarNames = new List<string>();
            for (int i = 0; i < portType.Length; i++)
            {
                this.Port.Add(portType[i]);
            }
            this.vendorEnum = IOCardObj.GetVendorEnumByString(vendor);
            this.Name = string.Format("{0} {1} [{2}] {3}", new object[]
            {
                vendor,
                model,
                cardINo,
                portNum
            });
        }

        public IOCardObj(int cardINo, string vendor, string model, string ipAddress, int portNum, string[] inputVarNames, string[] outputVarNames, int startOutputAcsBuffer, int stopOutputAcsBuffer)
        {
            this.Vendor = vendor;
            this.Model = model;
            this.CardNo = cardINo;
            this.PortNum = portNum;
            this.NumberOfInputPort = ((inputVarNames == null) ? 0 : inputVarNames.Length);
            this.NumberOfOutputPort = ((inputVarNames == null) ? 0 : outputVarNames.Length);
            this.Port = new List<PortType>();
            this.ACSWagoVarNames = new List<string>();
            this.ACSWagoInputVarNames = new List<string>();
            this.ACSWagoOutputVarNames = new List<string>();
            this.IpAddress = ipAddress;
            this.StartOutputACSBuffer = startOutputAcsBuffer;
            this.StopOutputACSBuffer = stopOutputAcsBuffer;
            if (inputVarNames != null)
            {
                for (int i = 0; i < inputVarNames.Length; i++)
                {
                    this.ACSWagoVarNames.Add(inputVarNames[i]);
                    this.ACSWagoInputVarNames.Add(inputVarNames[i]);
                }
            }
            if (outputVarNames != null)
            {
                for (int i = 0; i < outputVarNames.Length; i++)
                {
                    this.ACSWagoVarNames.Add(outputVarNames[i]);
                    this.ACSWagoOutputVarNames.Add(outputVarNames[i]);
                }
            }
            this.vendorEnum = IOCardObj.GetVendorEnumByString(vendor);
            this.Name = string.Format("{0} {1} [{2}] {3}", new object[]
            {
                this.vendorEnum,
                model,
                cardINo,
                portNum
            });
        }

        public IOCardObj(int cardINo, string vendor, string model, string ipAddress, int ipPort, int portNum, params PortType[] portType)
        {
            this.Vendor = vendor;
            this.Model = model;
            this.CardNo = cardINo;
            this.PortNum = portNum;
            this.IpAddress = ipAddress;
            this.IpPort = ipPort;
            this.Port = new List<PortType>();
            this.ACSWagoVarNames = new List<string>();
            for (int i = 0; i < portType.Length; i++)
            {
                this.Port.Add(portType[i]);
            }
            this.vendorEnum = IOCardObj.GetVendorEnumByString(vendor);
            this.Name = string.Format("{0} {1} [{2}] {3}", new object[]
            {
                this.vendorEnum,
                model,
                cardINo,
                portNum
            });
        }

        public IOCardVendor VendorEnum
        {
            get
            {
                return this.vendorEnum;
            }
            set
            {
                this.vendorEnum = value;
            }
        }

        public static IOCardVendor GetVendorEnumByString(string str)
        {
            IOCardVendor result = IOCardVendor.Unknown;
            string text = str.ToLower();
            string text2 = text;
            if (text2 != null)
            {
                if (!(text2 == "adlink"))
                {
                    if (!(text2 == "wago"))
                    {
                        if (text2 == "acswagoethercat")
                        {
                            result = IOCardVendor.ACSWagoEtherCAT;
                        }
                    }
                    else
                    {
                        result = IOCardVendor.Wago;
                    }
                }
                else
                {
                    result = IOCardVendor.Adlink;
                }
            }
            return result;
        }

        public static WagoCardModel GetWagoCardModelByString(string str)
        {
            WagoCardModel result = WagoCardModel.WagoStandardIO;
            string text = str.ToLower();
            string text2 = text;
            if (text2 != null)
            {
                if (text2 == "wago i/o" || text2 == "wago standard io")
                {
                    result = WagoCardModel.WagoStandardIO;
                }
            }
            return result;
        }

        public static AdlinkIOCardModel GetAdlinkCardModelByString(string str)
        {
            AdlinkIOCardModel result = AdlinkIOCardModel.Unknown;
            string text = str.ToLower();
            string text2 = text;
            if (text2 != null)
            {
                if (text2 == "pci7296" || text2 == "pci 7296" || text2 == "pci-7296")
                {
                    result = AdlinkIOCardModel.PCI7296;
                }
            }
            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private bool hasInitialized = false;
        public List<PortType> Port;
        public List<string> ACSWagoVarNames;
        public List<string> ACSWagoInputVarNames;
        public List<string> ACSWagoOutputVarNames;
        private IOCardVendor vendorEnum = IOCardVendor.Unknown;
    }
}
