using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stanley_Utility;
using Stanley_MCPNet.IO;
using System.ComponentModel;

namespace Stanley_MCPNet.IOCard
{
    public class MccIOCard : IIOCard, IDisposable, INotifyPropertyChanged
    {
        public string CardName { get; set; }
        public string ErrorMsg { get; set; }
        public bool IsSimulate { get; set; }
        public int CardNumber { get; set; }
        public List<IOCardObj> CardList { get; set; }

        public MccIOCard()
        {
            CardList = new List<IOCardObj>();
        }

        public void AddDefaultIOs()
        {
            IOCardObj iOCardObj = new IOCardObj(CardNumber, "YangKon", "MCC800P", 3, new PortType[]
            {
                new PortType("MCC800P Input",0, "i",0),
                new PortType("MCC800P Input",1,"i",0),
                new PortType("MCC800P Output",0, "o",255)
            });
            iOCardObj.Simulate = this.IsSimulate;
            if (this.IsDuplicateCard(iOCardObj))
            {
                throw new Exception("Duplicate Card");
            }
            this.CardList.Add(iOCardObj);
        }

        public bool InitCard()
        {
            //MCC Is a Combination Of IO And Motion 
            //Init Card Outside One Time For Two 
            return true;
        }
        public void Dispose()
        {
            for (int i = 0; i < this.CardList.Count; i++)
            {
                this.CardList[i].Inst.Dispose();
            }
        }
        public bool IsDuplicateCard(IOCardObj card)
        {
            for (int i = 0; i < this.CardList.Count; i++)
            {
                if (card.CardNo == this.CardList[i].CardNo)
                {
                    if (card.VendorEnum == this.CardList[i].VendorEnum)
                    {
                        if (!(card.Model.ToLower() != this.CardList[i].Model.ToLower()))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool IsDuplicateCard(IIOCard card)
        {
            for (int i = 0; i < this.CardList.Count; i++)
            {
                if (card.CardNumber == this.CardList[i].CardNo)
                {
                    if (card.CardName == this.CardList[i].Name)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool LoadXml(string fileName)
        {
            this.CardList = (List<IOCardObj>)XmlHelper.LoadXml(typeof(List<IOCardObj>), fileName);
            return this.CardList != null;
        }
        public bool SaveXml(string fileName)
        {
            return XmlHelper.SaveXml(typeof(List<IOCardObj>), this.CardList, fileName);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }
    }
}
