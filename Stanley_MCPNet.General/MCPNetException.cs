using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanley_MCPNet.General
{
    public class MCPNetException : Exception
    {
        public MCPNetException(string msg, ErrorCode.ErrCode errCode) : base("Code: " + errCode.ToString() + "\r\n" + msg)
        {
            this.ErrorCode = errCode;
        }
        
        public ErrorCode.ErrCode ErrorCode = General.ErrorCode.ErrCode.NoError;
    }
}
