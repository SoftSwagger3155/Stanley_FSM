using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LX_MachineMonitor;

namespace Test_FSM_WPF.Utility
{
    public static class StationCustomErrorCode
    {
         //-1~-10 通用錯誤碼
        //===================================================//
        public const int ActionNotTaken = -1;
        //===================================================//

        // -1000 ~ -1999 Sytem Error
        //===================================================//

        //===================================================//

        // -2000 ~ Above 自行定義
        //===================================================//

        //===================================================//

        static  StationCustomErrorCode()
        {
        }

       public static void BuildErrorMap()
        {
            SystemErrorCenter.Add(ActionNotTaken, "Action Not Taken");
        }

        public static string GetErrorMsg(int errorCode)
        {
            return SystemErrorCenter.GetErrorMessage(errorCode);
        }
    }
}
