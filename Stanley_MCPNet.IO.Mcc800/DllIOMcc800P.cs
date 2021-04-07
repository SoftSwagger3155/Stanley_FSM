using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Stanley_MCPNet.IO.Mcc800
{
    public static class DllIOMcc800P
    {
        //通用IO
        [DllImport("MCC.dll", EntryPoint = "YK_read_inbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_read_inbit(UInt16 CardNo, UInt16 bitno);
        [DllImport("MCC.dll", EntryPoint = "YK_write_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_write_outbit(UInt16 CardNo, UInt16 bitno, UInt16 on_off);
        [DllImport("MCC.dll", EntryPoint = "YK_read_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_read_outbit(UInt16 CardNo, UInt16 bitno);
        [DllImport("MCC.dll", EntryPoint = "YK_read_inport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 YK_read_inport(UInt16 CardNo, UInt16 portno);
        [DllImport("MCC.dll", EntryPoint = "YK_read_outport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 YK_read_outport(UInt16 CardNo, UInt16 portno);
        [DllImport("MCC.dll", EntryPoint = "YK_write_outport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_write_outport(UInt16 CardNo, UInt16 portno, UInt32 outport_val);
        //--------IO输出延时翻转--------------------------------------------------------------------------------------------------------------------------
        [DllImport("MCC.dll", EntryPoint = "YK_IO_TurnOutDelay", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_IO_TurnOutDelay(UInt16 CardNo, UInt16 bitno, UInt32 DelayTime);
        //以上函数以毫秒为单位可继续使用，新函数将时间统一到秒为单位
        [DllImport("MCC.dll", EntryPoint = "YK_reverse_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_reverse_outbit(UInt16 CardNo, UInt16 bitno, double reverse_time);
        //
    }
}
