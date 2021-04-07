using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanley_MCPNet.Motion.Mcc800
{
    public enum StopReason
    {
        //正常停止
        NORMAL_STOP = 1,
        //ALM 立即停止
        IMD_STOP_AT_ALM = 2,
        //ALM 減速停止
        DEC_STOP_AT_ALM = 4,
        //LTC 外部觸發立即停止
        IMD_STOP_AT_LTC =8,
        //EMG 立即停止
        IMD_STOP_AT_EMG=16,
        //正硬限位立即停止
        IMD_STOP_AT_ELP=32,
        //負硬限位立即停止
        IMD_STOP_ELN=64,
        //正硬限位減速停止
        DEC_STOP_AT_ELP = 128,
        //負硬限位減速停止
        DEC_STOP_AT_ELN = 256,
        //正軟限位立即停止
        IMD_STOP_AT_SOFT_ELP = 512,
        //負軟限位立即停止
        IMD_STOP_AT_SOFT_ELN = 1024,
        //正軟限位減速停止
        DEC_STOP_AT_SOFT_ELP = 2048,
        //負軟限位減速停止
        DEC_STOP_AT_SOFT_ELN = 4096,
        //命令立即停止
        IMD_STOP_AT_CMD = 8192,
        //命令減速停止
        DEC_STOP_AT_CMD = 16384,
        //其他原因停止
        IMD_STOP_AT_OTHER = 32768,
        //其他原因減速停止
        DEC_STOP_AT_OTHER = 65536,
        //未知原因立即停止
        IMD_STOP_AT_UNKNOWN = 131072,
        //未知原因減速停止
        DEC_STOP_AT_UNKNOWN = 262144,
        //外部IO減速停止
        DEC_STOP_AT_DEC = 524288
    }

    public enum MccMotionStatus
    {
        Running = 0,
        Stop = 1
    }
}
