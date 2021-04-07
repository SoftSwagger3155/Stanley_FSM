using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanley_MCPNet.IOCard
{
    public interface IIOCard
    {
        string CardName { get; set; }
        string ErrorMsg { get; set; }
        bool IsSimulate { get; set; }
        int CardNumber { get; set; }

        void AddDefaultIOs();
        bool IsDuplicateCard(IIOCard card);
        bool LoadXml(string fileName);
        bool SaveXml(string fileName);
        bool InitCard();
    }
}
