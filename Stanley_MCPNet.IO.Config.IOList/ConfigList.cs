using Stanley_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel;
using Stanley_MCPNet.IOCard;

namespace Stanley_MCPNet.IO.Config.IOList
{
    public class ConfigList : IDisposable, INotifyPropertyChanged
    {
        [XmlIgnore]
        public float ScanningTime
        {
            get
            {
                return this.scanningTime;
            }
            set
            {
                if (Math.Abs(this.scanningTime - value) >= 0.001f)
                {
                    this.scanningTime = value;
                    this.OnPropertyChanged("ScanningTime");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        [XmlIgnore]
        public int NumberOfGroup
        {
            get
            {
                return this.groupNameList.Count;
            }
        }

        [XmlIgnore]
        public List<string> GroupNameList
        {
            get
            {
                return this.groupNameList;
            }
        }

        public ConfigList()
        {
        }

        public ConfigList(ConfigList configList)
        {
            this.iOBase = configList.iOBase;
            this.iOCard = configList.iOCard;
            this.iOList = new List<IOElement>(configList.iOList);
        }

        [XmlIgnore]
        public bool Simulation
        {
            get
            {
                return this.simulation;
            }
            set
            {
                this.simulation = value;
            }
        }

        public void Dispose()
        {
        }

        public object LoadXml(string fileName)
        {
            XmlTextReader xmlTextReader = new XmlTextReader(fileName);
            while (xmlTextReader.Read())
            {
                XmlNodeType nodeType = xmlTextReader.NodeType;
                switch (nodeType)
                {
                    case XmlNodeType.Element:
                        break;
                    case XmlNodeType.Attribute:
                        break;
                    case XmlNodeType.Text:
                        break;
                    default:
                        if (nodeType != XmlNodeType.EndElement)
                        {
                        }
                        break;
                }
            }
            return null;
        }

        public object Load3(string fileName)
        {
            object obj = XmlHelper.LoadXml(typeof(List<IOElement>), fileName);
            ConfigList configList = new ConfigList(this);
            if (obj != null)
            {
                configList.iOList = (List<IOElement>)obj;
                configList.RefreshData();
            }
            return configList;
        }

        public bool LoadAndUpdateList(Type objectType, string fileName)
        {
            try
            {
                List<IOElement> list = (List<IOElement>)XmlHelper.LoadXml(typeof(List<IOElement>), fileName);
                if (list != null)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        IOElement ioelement = this.TryGetExistingIO(list[i]);
                        if (ioelement != null)
                        {
                            ioelement.UpdateInfo(list[i]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public IOElement TryGetExistingIO(IOElement newObj)
        {
            IOElement ioelement = this.GetIOByName(newObj.name);
            if (ioelement == null)
            {
                ioelement = this.GetIOByCardPortAndBit(newObj.cardNo, newObj.portNo, newObj.bit);
                if (ioelement != null)
                {
                }
            }
            return ioelement;
        }

        public IOElement GetIOByName(string name)
        {
            for (int i = 0; i < this.iOList.Count; i++)
            {
                if (this.iOList[i].name == name)
                {
                    return this.iOList[i];
                }
            }
            return null;
        }

        public IOElement GetIOByCardPortAndBit(int card, int port, int bit)
        {
            for (int i = 0; i < this.iOList.Count; i++)
            {
                if (this.iOList[i].cardNo == card && this.iOList[i].portNo == port && this.iOList[i].bit == bit)
                {
                    return this.iOList[i];
                }
            }
            return null;
        }

        public void UpdateSimulation()
        {
            for (int i = 0; i < this.iOList.Count; i++)
            {
                this.iOList[i].simulate = this.iOCard.IsSimulate;
            }
        }

        public object Load(Type objectType, string fileName)
        {
            object obj = XmlHelper.LoadXml(objectType, fileName);
            ConfigList configList = new ConfigList(this);
            if (obj != null)
            {
                ((ConfigList)obj).iOBase = configList.iOBase;
                ((ConfigList)obj).iOCard = configList.iOCard;
                ((ConfigList)obj).RefreshData();
            }
            return obj;
        }

        public bool SaveOnlyList(string fileName)
        {
            return XmlHelper.SaveXml(typeof(List<IOElement>), this.iOList, fileName);
        }

        public bool Save(Type objectType, object objectToStore, string fileName)
        {
            return XmlHelper.SaveXml(objectType, objectToStore, fileName);
        }

        public virtual object InitList(MccIOCard iOCard)
        {
            this.iOCard = iOCard;
            this.iOList = new List<IOElement>();
            this.iOBase = null;
            return this;
        }

        public void Add(bool simulate, params IOElement[] itemList)
        {
            for (int i = 0; i < itemList.Length; i++)
            {
                itemList[i].simulate = simulate;
                itemList[i].SetSimulationDefault();
                this.iOList.Add(itemList[i]);
                string groupName = itemList[i].GroupName;
                if (!this.groupNameList.Contains(groupName))
                {
                    this.groupNameList.Add(groupName);
                }
            }
        }

        public void Add(params IOElement[] itemList)
        {
            for (int i = 0; i < itemList.Length; i++)
            {
                itemList[i].SetSimulationDefault();
                this.iOList.Add(itemList[i]);
                string groupName = itemList[i].GroupName;
                if (!this.groupNameList.Contains(groupName))
                {
                    this.groupNameList.Add(groupName);
                }
            }
        }

        public virtual void StartScanning()
        {
            if (!this.scanningData)
            {
                this.scanningData = true;
                this.scanningThred = new Thread(new ThreadStart(this.ScanningOperation));
                this.scanningThred.IsBackground = true;
                this.scanningThred.Start();
            }
        }

        public virtual void StopScanning()
        {
            if (this.scanningData)
            {
                this.scanningData = false;
                Thread.Sleep(this.scanningDelay);
                if (this.scanningThred != null && this.scanningThred.IsAlive)
                {
                    this.scanningThred.Abort();
                }
                this.scanningThred = null;
            }
        }

        public List<IOElement> GetListByCardNumber(int cardNumber)
        {
            List<IOElement> list = new List<IOElement>();
            for (int i = 0; i < this.iOList.Count; i++)
            {
                if (this.iOList[i].cardNo == cardNumber)
                {
                    list.Add(this.iOList[i]);
                }
            }
            return list;
        }

        public List<IOElement> GetListByCardNumberAndGroupName(int cardNumber, string groupName)
        {
            List<IOElement> list = new List<IOElement>();
            for (int i = 0; i < this.iOList.Count; i++)
            {
                if (this.iOList[i].cardNo == cardNumber)
                {
                    if (!(this.iOList[i].GroupName != groupName))
                    {
                        list.Add(this.iOList[i]);
                    }
                }
            }
            return list;
        }

        private void RefreshData()
        {
            this.iOList = new List<IOElement>();
            MemberInfo[] members = base.GetType().GetMembers(BindingFlags.Instance | BindingFlags.Public);
            foreach (MemberInfo memberInfo in members)
            {
                FieldInfo fieldInfo = memberInfo as FieldInfo;
                if (!(fieldInfo == null))
                {
                    Type fieldType = fieldInfo.FieldType;
                    if (fieldType.BaseType == typeof(IOElement))
                    {
                        object value = fieldInfo.GetValue(this);
                        if (this.iOCard != null)
                        {
                            this.iOBase = this.iOCard.CardList[((IOElement)value).cardNo].Inst;
                        }
                        ((IOElement)value).iOBase = this.iOBase;
                        this.iOList.Add((IOElement)value);
                    }
                }
            }
        }

        public void RefreshOutputBuffer(int[] OutBuf)
        {
            MemberInfo[] members = base.GetType().GetMembers(BindingFlags.Instance | BindingFlags.Public);
            foreach (MemberInfo memberInfo in members)
            {
                FieldInfo fieldInfo = memberInfo as FieldInfo;
                if (!(fieldInfo == null))
                {
                    Type fieldType = fieldInfo.FieldType;
                    if (fieldType == typeof(OpObj))
                    {
                        object value = fieldInfo.GetValue(this);
                        if (value != null)
                        {
                            if (this.iOCard != null)
                            {
                                this.iOBase = this.iOCard.CardList[((OpObj)value).cardNo].Inst;
                            }
                            ((OpObj)value).PortBuf = OutBuf;
                        }
                    }
                }
            }
            foreach (IOElement ioelement in this.iOList)
            {
                OpObj opObj = (OpObj)ioelement;
                opObj.PortBuf = OutBuf;
            }
        }

        public virtual void ScanningOperation()
        {
        }

        [XmlIgnore]
        public IOBase iOBase;
        [XmlIgnore]
        public MccIOCard iOCard;
        [XmlIgnore]
        public List<IOElement> iOList;
        private Thread scanningThred;
        private float scanningTime = 0f;
        private List<string> groupNameList = new List<string>();
        private bool simulation = false;
        protected bool scanningData = false;
        protected int scanningDelay = 10;
    }
}
