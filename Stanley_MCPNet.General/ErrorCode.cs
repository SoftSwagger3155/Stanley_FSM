using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanley_MCPNet.General
{
    public class ErrorCode
    {
        public ErrorCode()
        {
            this.ClearData();
        }
        
        public void ClearData()
        {
            this.Code = ErrorCode.ErrCode.NoError;
            this.Msg = "";
        }

        public ErrorCode.ErrCode Code = ErrorCode.ErrCode.NoError;
        public string Msg = "";
        public enum ErrCode
        {
            NoError,
            System,
            Sequence,
            Timeout,
            Motion,
            MotionCard,
            Motion_Limit,
            Motion_Alarm,
            VisionCamera,
            Vision,
            ExceedRetry,
            VisionCard,
            File,
            Memory,
            IO,
            IOCard,
            License,
            Config,
            Bypassed,
            OutOfRange,
            LightingError,
            EStop,
            HomingError,
            Others,
            Unknowned
        }
    }
}
