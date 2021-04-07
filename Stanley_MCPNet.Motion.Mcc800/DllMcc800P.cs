using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Stanley_MCPNet.Motion.Mcc800
{
    public static class DllMCC800P
    {
        //========================================================================================================================================================================
        //板卡配置
        [DllImport("MCC.dll", EntryPoint = "YK_board_init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_board_init();
        [DllImport("MCC.dll", EntryPoint = "YK_board_close", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_board_close();
        [DllImport("MCC.dll", EntryPoint = "YK_board_reset", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_board_reset();
        [DllImport("MCC.dll", EntryPoint = "YK_get_CardInfList", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_CardInfList(ref UInt16 CardNum, UInt32[] CardTypeList, UInt16[] CardIdList);
        [DllImport("MCC.dll", EntryPoint = "YK_get_card_version", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_card_version(UInt16 CardNo, ref UInt32 CardVersion);
        [DllImport("MCC.dll", EntryPoint = "YK_get_card_soft_version", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_card_soft_version(UInt16 CardNo, ref UInt32 FirmID, ref UInt32 SubFirmID);
        [DllImport("MCC.dll", EntryPoint = "YK_get_card_lib_version", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_card_lib_version(ref UInt32 LibVer);
        [DllImport("MCC.dll", EntryPoint = "YK_get_total_axes", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_total_axes(UInt16 CardNo, ref UInt32 TotalAxis);
        //下载参数文件------------------------------------------------------------------------------------------------------------------------------------
        [DllImport("MCC.dll", EntryPoint = "YK_download_configfile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_download_configfile(UInt16 CardNo, String FileName);
        //下载固件文件
        [DllImport("MCC.dll", EntryPoint = "YK_download_firmware", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_download_firmware(UInt16 CardNo, String FileName);
        //=======================================================================================================================================================================
        //MCC800P专用 轴IO映射配置
        [DllImport("MCC.dll", EntryPoint = "YK_set_AxisIoMap", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_AxisIoMap(UInt16 CardNo, UInt16 Axis, UInt16 IoType, UInt16 MapIoType, UInt16 MapIoIndex, UInt32 Filter);
        [DllImport("MCC.dll", EntryPoint = "YK_get_AxisIoMap", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_AxisIoMap(UInt16 CardNo, UInt16 Axis, UInt16 IoType, ref UInt16 MapIoType, ref UInt16 MapIoIndex, ref UInt32 Filter);
        //以上函数以毫秒为单位可继续使用，新函数将时间统一到秒为单位
        [DllImport("MCC.dll", EntryPoint = "YK_set_axis_io_map", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_axis_io_map(UInt16 CardNo, UInt16 Axis, UInt16 IoType, UInt16 MapIoType, UInt16 MapIoIndex, double Filter);
        [DllImport("MCC.dll", EntryPoint = "YK_get_axis_io_map", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_axis_io_map(UInt16 CardNo, UInt16 Axis, UInt16 IoType, ref UInt16 MapIoType, ref UInt16 MapIoIndex, ref double Filter);
        [DllImport("MCC.dll", EntryPoint = "YK_set_special_input_filter", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_special_input_filter(UInt16 CardNo, double Filter);      //设置所有专用IO滤波时间
                                                                                                   //3800专用 虚拟IO映射  用于读取滤波后的IO口电平状态
        [DllImport("MCC.dll", EntryPoint = "YK_set_io_map_virtual", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_io_map_virtual(UInt16 CardNo, UInt16 bitno, UInt16 MapIoType, UInt16 MapIoIndex, double Filter);
        [DllImport("MCC.dll", EntryPoint = "YK_get_io_map_virtual", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_io_map_virtual(UInt16 CardNo, UInt16 bitno, ref UInt16 MapIoType, ref UInt16 MapIoIndex, ref double Filter);
        [DllImport("MCC.dll", EntryPoint = "YK_read_inbit_virtual", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_read_inbit_virtual(UInt16 CardNo, UInt16 bitno);
        //==============================================================================================================================================================================
        //限位异常设置
        [DllImport("MCC.dll", EntryPoint = "YK_set_softlimit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_softlimit(UInt16 CardNo, UInt16 axis, UInt16 enable, UInt16 source_sel, UInt16 SL_action, Int32 N_limit, Int32 P_limit);
        [DllImport("MCC.dll", EntryPoint = "YK_get_softlimit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_softlimit(UInt16 CardNo, UInt16 axis, ref UInt16 enable, ref UInt16 source_sel, ref UInt16 SL_action, ref Int32 N_limit, ref Int32 P_limit);
        [DllImport("MCC.dll", EntryPoint = "YK_set_el_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_el_mode(UInt16 CardNo, UInt16 axis, UInt16 el_enable, UInt16 el_logic, UInt16 el_mode);
        [DllImport("MCC.dll", EntryPoint = "YK_get_el_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_el_mode(UInt16 CardNo, UInt16 axis, ref UInt16 el_enable, ref UInt16 el_logic, ref UInt16 el_mode);
        [DllImport("MCC.dll", EntryPoint = "YK_set_emg_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_emg_mode(UInt16 CardNo, UInt16 axis, UInt16 enable, UInt16 emg_logic);
        [DllImport("MCC.dll", EntryPoint = "YK_get_emg_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_emg_mode(UInt16 CardNo, UInt16 axis, ref UInt16 enbale, ref UInt16 emg_logic);
        //MCC800P专用 外部减速停止信号及减速停止时间设置-----------------------------------------------------------------------------------------
        [DllImport("MCC.dll", EntryPoint = "YK_set_dstp_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_dstp_mode(UInt16 CardNo, UInt16 axis, UInt16 enable, UInt16 logic, UInt32 time);
        [DllImport("MCC.dll", EntryPoint = "YK_get_dstp_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_dstp_mode(UInt16 CardNo, UInt16 axis, ref UInt16 enable, ref UInt16 logic, ref UInt32 time);
        [DllImport("MCC.dll", EntryPoint = "YK_set_dstp_time", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_dstp_time(UInt16 CardNo, UInt16 axis, UInt32 time);
        [DllImport("MCC.dll", EntryPoint = "YK_get_dstp_time", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_dstp_time(UInt16 CardNo, UInt16 axis, ref UInt32 time);
        //以上函数以毫秒为单位可继续使用，新函数将时间统一到秒为单位
        [DllImport("MCC.dll", EntryPoint = "YK_set_io_dstp_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_io_dstp_mode(UInt16 CardNo, UInt16 axis, UInt16 enable, UInt16 logic);
        [DllImport("MCC.dll", EntryPoint = "YK_get_io_dstp_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_io_dstp_mode(UInt16 CardNo, UInt16 axis, ref UInt16 enable, ref UInt16 logic);
        [DllImport("MCC.dll", EntryPoint = "YK_set_dec_stop_time", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_dec_stop_time(UInt16 CardNo, UInt16 axis, double stop_time);
        [DllImport("MCC.dll", EntryPoint = "YK_get_dec_stop_time", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_dec_stop_time(UInt16 CardNo, UInt16 axis, ref double stop_time);
        //==========================================================================================================================================================================================
        //速度设置
        [DllImport("MCC.dll", EntryPoint = "YK_set_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_profile(UInt16 CardNo, UInt16 axis, double Min_Vel, double Max_Vel, double Tacc, double Tdec, double stop_vel);
        [DllImport("MCC.dll", EntryPoint = "YK_get_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_profile(UInt16 CardNo, UInt16 axis, ref double Min_Vel, ref double Max_Vel, ref double Tacc, ref double Tdec, ref double stop_vel);
        [DllImport("MCC.dll", EntryPoint = "YK_set_s_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_s_profile(UInt16 CardNo, UInt16 axis, UInt16 s_mode, double s_para);
        [DllImport("MCC.dll", EntryPoint = "YK_get_s_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_s_profile(UInt16 CardNo, UInt16 axis, UInt16 s_mode, ref double s_para);
        [DllImport("MCC.dll", EntryPoint = "YK_set_vector_profile_multicoor", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_vector_profile_multicoor(UInt16 CardNo, UInt16 Crd, double Min_Vel, double Max_Vel, double Tacc, double Tdec, double Stop_Vel);
        [DllImport("MCC.dll", EntryPoint = "YK_get_vector_profile_multicoor", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_vector_profile_multicoor(UInt16 CardNo, UInt16 Crd, ref double Min_Vel, ref double Max_Vel, ref double Taccdec, ref double Tdec, ref double Stop_Vel);
        //===========================================================================================================================================================================================
        //运动模块脉冲模式
        [DllImport("MCC.dll", EntryPoint = "YK_set_pulse_outmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_pulse_outmode(UInt16 CardNo, UInt16 axis, UInt16 outmode);
        [DllImport("MCC.dll", EntryPoint = "YK_get_pulse_outmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_pulse_outmode(UInt16 CardNo, UInt16 axis, ref UInt16 outmode);
        //点位运动-------------------------------------------------------------------------------------------------------------------------------------
        [DllImport("MCC.dll", EntryPoint = "YK_pmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_pmove(UInt16 CardNo, UInt16 axis, Int32 Dist, UInt16 posi_mode);
        //JOG运动--------------------------------------------------------------------------------------------------------------------------------------
        [DllImport("MCC.dll", EntryPoint = "YK_vmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_vmove(UInt16 CardNo, UInt16 axis, UInt16 dir);
        //PVT运动------------------------------------------------------------------------------------------------------------------------------------------------------
        [DllImport("MCC.dll", EntryPoint = "YK_PvtTable", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_PvtTable(UInt16 CardNo, UInt16 iaxis, UInt32 count, double[] pTime, Int32[] pPos, double[] pVel);
        [DllImport("MCC.dll", EntryPoint = "YK_PtsTable", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_PtsTable(UInt16 CardNo, UInt16 iaxis, UInt32 count, double[] pTime, Int32[] pPos, double[] pPercent);
        [DllImport("MCC.dll", EntryPoint = "YK_PvtsTable", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_PvtsTable(UInt16 CardNo, UInt16 iaxis, UInt32 count, double[] pTime, Int32[] pPos, double velBegin, double velEnd);
        [DllImport("MCC.dll", EntryPoint = "YK_PttTable", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_PttTable(UInt16 CardNo, UInt16 iaxis, UInt32 count, double[] pTime, int[] pPos);
        [DllImport("MCC.dll", EntryPoint = "YK_PvtMove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_PvtMove(UInt16 CardNo, UInt16 AxisNum, UInt16[] AxisList);
        //==============================================================================================================================================================
        //在线变位/变速运动
        [DllImport("MCC.dll", EntryPoint = "YK_reset_target_position", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_reset_target_position(UInt16 CardNo, UInt16 axis, Int32 dist, UInt16 posi_mode);
        [DllImport("MCC.dll", EntryPoint = "YK_change_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_change_speed(UInt16 CardNo, UInt16 axis, double Curr_Vel, double Taccdec);
        [DllImport("MCC.dll", EntryPoint = "YK_update_target_position", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_update_target_position(UInt16 CardNo, UInt16 axis, Int32 dist, UInt16 posi_mode);
        //插补===================================================================================================================================================================
        //直线插补		
        [DllImport("MCC.dll", EntryPoint = "YK_line_multicoor", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_line_multicoor(UInt16 CardNo, UInt16 crd, UInt16 axisNum, UInt16[] axisList, double[] DistList, UInt16 posi_mode);
        //平面圆弧
        [DllImport("MCC.dll", EntryPoint = "YK_arc_move_multicoor", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_arc_move_multicoor(UInt16 CardNo, UInt16 crd, UInt16[] AxisList, double[] Target_Pos, double[] Cen_Pos, UInt16 Arc_Dir, UInt16 posi_mode);
        //=======================================================================================================================================================================
        //回零运动
        [DllImport("MCC.dll", EntryPoint = "YK_set_home_pin_logic", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_home_pin_logic(UInt16 CardNo, UInt16 axis, UInt16 org_logic, double filter);
        [DllImport("MCC.dll", EntryPoint = "YK_get_home_pin_logic", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_home_pin_logic(UInt16 CardNo, UInt16 axis, ref UInt16 org_logic, ref double filter);
        [DllImport("MCC.dll", EntryPoint = "YK_set_homemode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_homemode(UInt16 CardNo, UInt16 axis, UInt16 home_dir, double vel, UInt16 mode, UInt16 EZ_count);
        [DllImport("MCC.dll", EntryPoint = "YK_get_homemode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_homemode(UInt16 CardNo, UInt16 axis, ref UInt16 home_dir, ref double vel, ref UInt16 home_mode, ref UInt16 EZ_count);
        [DllImport("MCC.dll", EntryPoint = "YK_home_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_home_move(UInt16 CardNo, UInt16 axis);
        //MCC800P专用 原点锁存--------------------------------------------------------------------------------------------------------------------------------
        [DllImport("MCC.dll", EntryPoint = "YK_set_homelatch_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_homelatch_mode(UInt16 CardNo, UInt16 axis, UInt16 enable, UInt16 logic, UInt16 source);
        [DllImport("MCC.dll", EntryPoint = "YK_get_homelatch_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_homelatch_mode(UInt16 CardNo, UInt16 axis, ref UInt16 enable, ref UInt16 logic, ref UInt16 source);
        [DllImport("MCC.dll", EntryPoint = "YK_get_homelatch_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_homelatch_flag(UInt16 CardNo, UInt16 axis);
        [DllImport("MCC.dll", EntryPoint = "YK_reset_homelatch_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_reset_homelatch_flag(UInt16 CardNo, UInt16 axis);
        [DllImport("MCC.dll", EntryPoint = "YK_get_homelatch_value", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 YK_get_homelatch_value(UInt16 CardNo, UInt16 axis);
        //==========================================================================================================================================================
        //MCC800P专用 手轮运动 
        //手轮通道选择
        [DllImport("MCC.dll", EntryPoint = "YK_set_handwheel_channel", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_handwheel_channel(UInt16 CardNo, UInt16 index);
        [DllImport("MCC.dll", EntryPoint = "YK_get_handwheel_channel", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_handwheel_channel(UInt16 CardNo, ref UInt16 index);
        //一个手轮信号控制单个轴运动	
        [DllImport("MCC.dll", EntryPoint = "YK_set_handwheel_inmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_handwheel_inmode(UInt16 CardNo, UInt16 axis, UInt16 inmode, Int32 multi, double vh);
        [DllImport("MCC.dll", EntryPoint = "YK_get_handwheel_inmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_handwheel_inmode(UInt16 CardNo, UInt16 axis, ref UInt16 inmode, ref Int32 multi, ref double vh);
        //MCC800P专用 一个手轮信号控制多个轴运动
        [DllImport("MCC.dll", EntryPoint = "YK_set_handwheel_inmode_extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_handwheel_inmode_extern(UInt16 CardNo, UInt16 inmode, UInt16 AxisNum, UInt16[] AxisList, Int32[] multi);
        [DllImport("MCC.dll", EntryPoint = "YK_get_handwheel_inmode_extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_handwheel_inmode_extern(UInt16 CardNo, ref UInt16 inmode, ref UInt16 AxisNum, UInt16[] AxisList, Int32[] multi);
        //启动手轮运动
        [DllImport("MCC.dll", EntryPoint = "YK_handwheel_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_handwheel_move(UInt16 CardNo, UInt16 axis);
        //==========================================================================================================================================================
        //编码器
        [DllImport("MCC.dll", EntryPoint = "YK_set_counter_inmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_counter_inmode(UInt16 CardNo, UInt16 axis, UInt16 mode);
        [DllImport("MCC.dll", EntryPoint = "YK_get_counter_inmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_counter_inmode(UInt16 CardNo, UInt16 axis, ref UInt16 mode);
        [DllImport("MCC.dll", EntryPoint = "YK_set_encoder", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_encoder(UInt16 CardNo, UInt16 axis, Int32 encoder_value);
        [DllImport("MCC.dll", EntryPoint = "YK_get_encoder", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 YK_get_encoder(UInt16 CardNo, UInt16 axis);
        [DllImport("MCC.dll", EntryPoint = "YK_set_ez_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_ez_mode(UInt16 CardNo, UInt16 axis, UInt16 ez_logic, UInt16 ez_mode, double filter);
        [DllImport("MCC.dll", EntryPoint = "YK_get_ez_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_ez_mode(UInt16 CardNo, UInt16 axis, ref UInt16 ez_logic, ref UInt16 ez_mode, ref double filter);
        //==========================================================================================================================================================
        //高速锁存-------------------------------------------------------------------------------------------------------------
        [DllImport("MCC.dll", EntryPoint = "YK_set_ltc_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_ltc_mode(UInt16 CardNo, UInt16 axis, UInt16 ltc_logic, UInt16 ltc_mode, Double filter);
        [DllImport("MCC.dll", EntryPoint = "YK_get_ltc_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_ltc_mode(UInt16 CardNo, UInt16 axis, ref UInt16 ltc_logic, ref UInt16 ltc_mode, ref Double filter);
        [DllImport("MCC.dll", EntryPoint = "YK_set_latch_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_latch_mode(UInt16 CardNo, UInt16 axis, UInt16 all_enable, UInt16 latch_source, UInt16 latch_channel);
        [DllImport("MCC.dll", EntryPoint = "YK_get_latch_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_latch_mode(UInt16 CardNo, UInt16 axis, ref UInt16 all_enable, ref UInt16 latch_source, ref UInt16 latch_channel);
        [DllImport("MCC.dll", EntryPoint = "YK_reset_latch_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_reset_latch_flag(UInt16 CardNo, UInt16 axis);
        [DllImport("MCC.dll", EntryPoint = "YK_get_latch_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_latch_flag(UInt16 CardNo, UInt16 axis);
        [DllImport("MCC.dll", EntryPoint = "YK_get_latch_value", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 YK_get_latch_value(UInt16 CardNo, UInt16 axis);
        [DllImport("MCC.dll", EntryPoint = "YK_get_latch_flag_extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_latch_flag_extern(UInt16 CardNo, UInt16 axis);
        [DllImport("MCC.dll", EntryPoint = "YK_get_latch_value_extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 YK_get_latch_value_extern(UInt16 CardNo, UInt16 axis, UInt16 Index);
        //LTC端口触发延时急停时间 单位us
        [DllImport("MCC.dll", EntryPoint = "YK_set_latch_stop_time", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_latch_stop_time(UInt16 CardNo, UInt16 axis, Int32 time);
        [DllImport("MCC.dll", EntryPoint = "YK_get_latch_stop_time", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_latch_stop_time(UInt16 CardNo, UInt16 axis, ref Int32 time);
        //MCC800P专用 LTC反相输出------------------------------------------------------------------------------------------------------------------------------------
        [DllImport("MCC.dll", EntryPoint = "YK_SetLtcOutMode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_SetLtcOutMode(UInt16 CardNo, UInt16 axis, UInt16 enable, UInt16 bitno);
        [DllImport("MCC.dll", EntryPoint = "YK_GetLtcOutMode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_GetLtcOutMode(UInt16 CardNo, UInt16 axis, ref UInt16 enable, ref UInt16 bitno);
        //=============================================================================================================================================================
        //单轴低速位置比较
        [DllImport("MCC.dll", EntryPoint = "YK_compare_set_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_compare_set_config(UInt16 CardNo, UInt16 axis, UInt16 enable, UInt16 cmp_source);
        [DllImport("MCC.dll", EntryPoint = "YK_compare_get_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_compare_get_config(UInt16 CardNo, UInt16 axis, ref UInt16 enable, ref UInt16 cmp_source);
        [DllImport("MCC.dll", EntryPoint = "YK_compare_clear_points", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_compare_clear_points(UInt16 CardNo, UInt16 axis);
        [DllImport("MCC.dll", EntryPoint = "YK_compare_add_point", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_compare_add_point(UInt16 CardNo, UInt16 axis, Int32 pos, UInt16 dir, UInt16 action, UInt32 actpara);
        [DllImport("MCC.dll", EntryPoint = "YK_compare_get_current_point", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_compare_get_current_point(UInt16 CardNo, UInt16 axis, ref Int32 pos);
        [DllImport("MCC.dll", EntryPoint = "YK_compare_get_points_runned", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_compare_get_points_runned(UInt16 CardNo, UInt16 axis, ref Int32 pointNum);
        [DllImport("MCC.dll", EntryPoint = "YK_compare_get_points_remained", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_compare_get_points_remained(UInt16 CardNo, UInt16 axis, ref Int32 pointNum);
        //二维低速位置比较
        [DllImport("MCC.dll", EntryPoint = "YK_compare_set_config_extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_compare_set_config_extern(UInt16 CardNo, UInt16 enable, UInt16 cmp_source);
        [DllImport("MCC.dll", EntryPoint = "YK_compare_get_config_extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_compare_get_config_extern(UInt16 CardNo, ref UInt16 enable, ref UInt16 cmp_source);
        [DllImport("MCC.dll", EntryPoint = "YK_compare_clear_points_extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_compare_clear_points_extern(UInt16 CardNo);
        [DllImport("MCC.dll", EntryPoint = "YK_compare_add_point_extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_compare_add_point_extern(UInt16 CardNo, UInt16[] axis, Int32[] pos, UInt16[] dir, UInt16 action, UInt32 actpara);
        [DllImport("MCC.dll", EntryPoint = "YK_compare_get_current_point_extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_compare_get_current_point_extern(UInt16 CardNo, ref Int32 pos);
        [DllImport("MCC.dll", EntryPoint = "YK_compare_get_points_runned_extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_compare_get_points_runned_extern(UInt16 CardNo, ref Int32 pointNum);
        [DllImport("MCC.dll", EntryPoint = "YK_compare_get_points_remained_extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_compare_get_points_remained_extern(UInt16 CardNo, ref Int32 pointNum);
        //单轴高速位置比较       
        [DllImport("MCC.dll", EntryPoint = "YK_hcmp_set_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_hcmp_set_mode(UInt16 CardNo, UInt16 hcmp, UInt16 cmp_enable);
        [DllImport("MCC.dll", EntryPoint = "YK_hcmp_get_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_hcmp_get_mode(UInt16 CardNo, UInt16 hcmp, ref UInt16 cmp_enable);
        [DllImport("MCC.dll", EntryPoint = "YK_hcmp_set_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_hcmp_set_config(UInt16 CardNo, UInt16 hcmp, UInt16 axis, UInt16 cmp_source, UInt16 cmp_logic, Int32 time);
        [DllImport("MCC.dll", EntryPoint = "YK_hcmp_get_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_hcmp_get_config(UInt16 CardNo, UInt16 hcmp, ref UInt16 axis, ref UInt16 cmp_source, ref UInt16 cmp_logic, ref Int32 time);
        [DllImport("MCC.dll", EntryPoint = "YK_hcmp_add_point", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_hcmp_add_point(UInt16 CardNo, UInt16 hcmp, Int32 cmp_pos);
        [DllImport("MCC.dll", EntryPoint = "YK_hcmp_set_liner", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_hcmp_set_liner(UInt16 CardNo, UInt16 hcmp, Int32 Increment, Int32 Count);
        [DllImport("MCC.dll", EntryPoint = "YK_hcmp_get_liner", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_hcmp_get_liner(UInt16 CardNo, UInt16 hcmp, ref Int32 Increment, ref Int32 Count);
        [DllImport("MCC.dll", EntryPoint = "YK_hcmp_get_current_state", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_hcmp_get_current_state(UInt16 CardNo, UInt16 hcmp, ref Int32 remained_points, ref Int32 current_point, ref Int32 runned_points);
        [DllImport("MCC.dll", EntryPoint = "YK_hcmp_clear_points", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_hcmp_clear_points(UInt16 CardNo, UInt16 hcmp);
        [DllImport("MCC.dll", EntryPoint = "YK_read_cmp_pin", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_read_cmp_pin(UInt16 CardNo, UInt16 hcmp);
        [DllImport("MCC.dll", EntryPoint = "YK_write_cmp_pin", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_write_cmp_pin(UInt16 CardNo, UInt16 hcmp, UInt16 on_off);
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------
        //二维高速位置比较
        //[DllImport("MCC.dll", EntryPoint = "YK_hcmp_2d_set_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        //public static extern short YK_hcmp_2d_set_config(UInt16 CardNo, UInt16 cmp_en, UInt16 cmp_mode, UInt16 x_axis, UInt16 x_cmp_source, UInt16 y_axis, UInt16 y_cmp_source, Int32 m_error, UInt16 out_mode, Int32 time, UInt16 pwm_enable, double duty, Int32 freq, UInt16 port_sel, UInt16 pwm_counter, UInt16 pwm_num);
        //[DllImport("MCC.dll", EntryPoint = "YK_hcmp_2d_get_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        //public static extern short YK_hcmp_2d_get_config(UInt16 CardNo, ref UInt16 cmp_en, ref UInt16 cmp_mode, ref UInt16 x_axis, ref UInt16 x_cmp_source, ref UInt16 y_axis, ref UInt16 y_cmp_source, ref Int32 m_error, ref UInt16 out_mode, ref Int32 time, ref UInt16 pwm_enable, ref double duty, ref Int32 freq, ref UInt16 port_sel, ref UInt16 pwm_counter, ref UInt16 pwm_num);
        //[DllImport("MCC.dll", EntryPoint = "YK_hcmp_2d_clear_points", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        //public static extern short YK_hcmp_2d_clear_points(UInt16 CardNo);
        //[DllImport("MCC.dll", EntryPoint = "YK_hcmp_2d_add_point", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        //public static extern short YK_hcmp_2d_add_point(UInt16 CardNo, Int32 x_cmp_pos, Int32 y_cmp_pos);
        //[DllImport("MCC.dll", EntryPoint = "YK_hcmp_2d_get_current_state", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        //public static extern short YK_hcmp_2d_get_current_state(UInt16 CardNo, ref Int32 remained_points, ref Int32 x_current_point, ref Int32 y_current_point, ref Int32 runned_points, ref UInt16 current_state);
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        [DllImport("MCC.dll", EntryPoint = "YK_2DCompareClear", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_2DCompareClear(UInt16 CardNo, UInt16 chn);
        [DllImport("MCC.dll", EntryPoint = "YK_2DCompareData", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_2DCompareData(UInt16 CardNo, UInt16 chn, Int32 pX, Int32 pY);
        [DllImport("MCC.dll", EntryPoint = "YK_2DComparePulse", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_2DComparePulse(UInt16 CardNo, UInt16 chn, UInt16 level, UInt16 outputType, Int32 time, Int32 pwm_duty, Int32 pwm_freq, Int32 pwm_num);
        [DllImport("MCC.dll", EntryPoint = "YK_2DCompareSetPrm", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_2DCompareSetPrm(UInt16 CardNo, UInt16 chn, UInt16 encx, UInt16 ency, UInt16 source, UInt16 maxerr, UInt16 threhold);
        [DllImport("MCC.dll", EntryPoint = "YK_2DCompareStart", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_2DCompareStart(UInt16 CardNo, UInt16 chn, UInt16 cmp_en);
        [DllImport("MCC.dll", EntryPoint = "YK_2DCompareStatus", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_2DCompareStatus(UInt16 CardNo, UInt16 chn, ref UInt16 pStatus, ref UInt16 pCount, ref UInt16 pFifoCount);
        [DllImport("MCC.dll", EntryPoint = "YK_2DCompareStop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_2DCompareStop(UInt16 CardNo, UInt16 chn, UInt16 cmp_en);
        [DllImport("MCC.dll", EntryPoint = "YK_SetComparePort", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_SetComparePort(UInt16 CardNo, UInt16 chn, UInt16 compare_port);
        //================================================================================================================================================================
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
        //================================================================================================================================================================
        //MCC800专用 IO辅助功能(IO计数功能)
        [DllImport("MCC.dll", EntryPoint = "YK_SetIoCountMode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_SetIoCountMode(UInt16 CardNo, UInt16 bitno, UInt16 mode, UInt32 filter);
        [DllImport("MCC.dll", EntryPoint = "YK_GetIoCountMode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_GetIoCountMode(UInt16 CardNo, UInt16 bitno, ref UInt16 mode, ref UInt32 filter);
        [DllImport("MCC.dll", EntryPoint = "YK_SetIoCountValue", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_SetIoCountValue(UInt16 CardNo, UInt16 bitno, UInt32 CountValue);
        [DllImport("MCC.dll", EntryPoint = "YK_GetIoCountValue", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_GetIoCountValue(UInt16 CardNo, UInt16 bitno, ref UInt32 CountValue);
        //以上函数以毫秒为单位可继续使用，新函数将时间统一到秒为单位
        [DllImport("MCC.dll", EntryPoint = "YK_set_io_count_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_io_count_mode(UInt16 CardNo, UInt16 bitno, UInt16 mode, double filter_time);
        [DllImport("MCC.dll", EntryPoint = "YK_get_io_count_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_io_count_mode(UInt16 CardNo, UInt16 bitno, ref UInt16 mode, ref double filter_time);
        [DllImport("MCC.dll", EntryPoint = "YK_set_io_count_value", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_io_count_value(UInt16 CardNo, UInt16 bitno, UInt32 CountValue);
        [DllImport("MCC.dll", EntryPoint = "YK_get_io_count_value", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_io_count_value(UInt16 CardNo, UInt16 bitno, ref UInt32 CountValue);
        //================================================================================================================================================================
        //伺服专用IO
        [DllImport("MCC.dll", EntryPoint = "YK_set_alm_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_alm_mode(UInt16 CardNo, UInt16 axis, UInt16 enable, UInt16 alm_logic, UInt16 alm_action);
        [DllImport("MCC.dll", EntryPoint = "YK_get_alm_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_alm_mode(UInt16 CardNo, UInt16 axis, ref UInt16 enable, ref UInt16 alm_logic, ref UInt16 alm_action);
        [DllImport("MCC.dll", EntryPoint = "YK_set_inp_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_inp_mode(UInt16 CardNo, UInt16 axis, UInt16 enable, UInt16 inp_logic);
        [DllImport("MCC.dll", EntryPoint = "YK_get_inp_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_inp_mode(UInt16 CardNo, UInt16 axis, ref UInt16 enable, ref UInt16 inp_logic);
        [DllImport("MCC.dll", EntryPoint = "YK_read_rdy_pin", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_read_rdy_pin(UInt16 CardNo, UInt16 axis);
        [DllImport("MCC.dll", EntryPoint = "YK_write_erc_pin", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_write_erc_pin(UInt16 CardNo, UInt16 axis, UInt16 sel);
        [DllImport("MCC.dll", EntryPoint = "YK_read_erc_pin", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_read_erc_pin(UInt16 CardNo, UInt16 axis);
        [DllImport("MCC.dll", EntryPoint = "YK_write_sevon_pin", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_write_sevon_pin(UInt16 CardNo, UInt16 axis, UInt16 on_off);
        [DllImport("MCC.dll", EntryPoint = "YK_read_sevon_pin", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_read_sevon_pin(UInt16 CardNo, UInt16 axis);
        //==============================================================================================================================================================
        //运动状态检测
        [DllImport("MCC.dll", EntryPoint = "YK_read_current_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern double YK_read_current_speed(UInt16 CardNo, UInt16 axis);
        [DllImport("MCC.dll", EntryPoint = "YK_read_vector_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern double YK_read_vector_speed(UInt16 CardNo, UInt16 Crd);
        [DllImport("MCC.dll", EntryPoint = "YK_get_position", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 YK_get_position(UInt16 CardNo, UInt16 axis);
        [DllImport("MCC.dll", EntryPoint = "YK_set_position", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_position(UInt16 CardNo, UInt16 axis, Int32 current_position);
        [DllImport("MCC.dll", EntryPoint = "YK_get_target_position", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 YK_get_target_position(UInt16 CardNo, UInt16 axis);
        [DllImport("MCC.dll", EntryPoint = "YK_check_done", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_check_done(UInt16 CardNo, UInt16 axis);
        [DllImport("MCC.dll", EntryPoint = "YK_axis_io_status", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 YK_axis_io_status(UInt16 CardNo, UInt16 axis);
        [DllImport("MCC.dll", EntryPoint = "YK_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_stop(UInt16 CardNo, UInt16 axis, UInt16 stop_mode);
        [DllImport("MCC.dll", EntryPoint = "YK_check_done_multicoor", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_check_done_multicoor(UInt16 CardNo, UInt16 crd);
        [DllImport("MCC.dll", EntryPoint = "YK_stop_multicoor", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_stop_multicoor(UInt16 CardNo, UInt16 crd, UInt16 stop_mode);
        [DllImport("MCC.dll", EntryPoint = "YK_emg_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_emg_stop(UInt16 CardNo);
        //检测轴到位状态------------------------------------------------------------------------------------------------------------------------------------------
        [DllImport("MCC.dll", EntryPoint = "YK_set_factor_error", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_factor_error(UInt16 CardNo, UInt16 axis, double factor, Int32 error_pos);
        [DllImport("MCC.dll", EntryPoint = "YK_get_factor_error", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_factor_error(UInt16 CardNo, UInt16 axis, ref double factor, ref Int32 error_pos);
        [DllImport("MCC.dll", EntryPoint = "YK_check_success_pulse", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_check_success_pulse(UInt16 CardNo, UInt16 axis);
        [DllImport("MCC.dll", EntryPoint = "YK_check_success_encoder", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_check_success_encoder(UInt16 CardNo, UInt16 axis);
        //==========================================================================================================================================================
        //MCC800,MCC600,MCC800S专用, CAN-IO扩展
        [DllImport("MCC.dll", EntryPoint = "YK_set_can_state", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_can_state(UInt16 CardNo, UInt16 NodeNum, UInt16 state, UInt16 Baud);//0-断开；1-连接；2-复位后自动连接
        [DllImport("MCC.dll", EntryPoint = "YK_get_can_state", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_can_state(UInt16 CardNo, ref UInt16 NodeNum, ref UInt16 state);//0-断开；1-连接；2-异常
        [DllImport("MCC.dll", EntryPoint = "YK_get_can_errcode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_can_errcode(UInt16 CardNo, ref UInt16 Errcode);//读取CanIo通讯错误码
        [DllImport("MCC.dll", EntryPoint = "YK_write_can_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_write_can_outbit(UInt16 CardNo, UInt16 Node, UInt16 bitno, UInt16 on_off);
        [DllImport("MCC.dll", EntryPoint = "YK_read_can_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_read_can_outbit(UInt16 CardNo, UInt16 Node, UInt16 bitno);
        [DllImport("MCC.dll", EntryPoint = "YK_read_can_inbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_read_can_inbit(UInt16 CardNo, UInt16 Node, UInt16 bitno);
        [DllImport("MCC.dll", EntryPoint = "YK_write_can_outport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_write_can_outport(UInt16 CardNo, UInt16 Node, UInt16 PortNo, UInt32 outport_val);//写CanIo输出口
        [DllImport("MCC.dll", EntryPoint = "YK_read_can_outport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 YK_read_can_outport(UInt16 CardNo, UInt16 Node, UInt16 PortNo);//读取CanIo输出端口
        [DllImport("MCC.dll", EntryPoint = "YK_read_can_inport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 YK_read_can_inport(UInt16 CardNo, UInt16 Node, UInt16 PortNo);//读取CanIo输入端口
                                                                                                  //==========================================================================================================================================================
                                                                                                  //PWM输出 MCC800P MCC800S专用
        [DllImport("MCC.dll", EntryPoint = "YK_set_pwm_enable", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_pwm_enable(UInt16 CardNo, UInt16 enable);//7号轴切换为PWM输出
        [DllImport("MCC.dll", EntryPoint = "YK_get_pwm_enable", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_pwm_enable(UInt16 CardNo, ref UInt16 enable);
        [DllImport("MCC.dll", EntryPoint = "YK_set_pwm_output", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_pwm_output(UInt16 CardNo, UInt16 pwm_no, double fDuty, double fFre);//设置PWM输出
        [DllImport("MCC.dll", EntryPoint = "YK_get_pwm_output", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_pwm_output(UInt16 CardNo, UInt16 pwm_no, ref double fDuty, ref double fFre);//获取PWM输出设置
                                                                                                                      //MCC800专用 主卡与接线盒通讯状态-------------------------------------------------------------------------------------------------------------
        [DllImport("MCC.dll", EntryPoint = "YK_LinkState", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_LinkState(UInt16 CardNo, ref UInt16 State);
        //密码管理-------------------------------------------------------------------------------------------------------------------------------------------------
        [DllImport("MCC.dll", EntryPoint = "YK_check_sn", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_check_sn(UInt16 CardNo, string check_sn);
        [DllImport("MCC.dll", EntryPoint = "YK_write_sn", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_write_sn(UInt16 CardNo, string new_sn);
        //函数库打印输出----------------------------------------------------------------------------------------------------------------------------------------
        [DllImport("MCC.dll", EntryPoint = "YK_set_debug_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_debug_mode(UInt16 mode, string FileName);
        [DllImport("MCC.dll", EntryPoint = "YK_get_debug_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_debug_mode(ref UInt16 mode, IntPtr FileName);
        //-------------------------------------------------------------------------------------------------------------------------------------------------------
        //读取停止原因
        [DllImport("MCC.dll", EntryPoint = "YK_get_stop_reason", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_stop_reason(UInt16 CardNo, UInt16 axis, ref Int32 StopReason);     //读取轴停止原因
                                                                                                             //[DllImport("MCC.dll", EntryPoint = "YK_clear_stop_reason", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
                                                                                                             //public static extern short YK_clear_stop_reason(UInt16 CardNo, UInt16 axis);     //清除轴停止原因
                                                                                                             //基于脉冲当量的函数 MCC800P,MCC800S专用
                                                                                                             //1.1	脉冲当量设置
        [DllImport("MCC.dll", EntryPoint = "YK_set_equiv", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_equiv(UInt16 CardNo, UInt16 axis, double equiv);
        [DllImport("MCC.dll", EntryPoint = "YK_get_equiv", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_equiv(UInt16 CardNo, UInt16 axis, ref double equiv);   //脉冲当量
                                                                                                 //1.2 状态检测
        [DllImport("MCC.dll", EntryPoint = "YK_set_position_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_position_unit(UInt16 CardNo, UInt16 axis, double pos);   //当前指令位置
        [DllImport("MCC.dll", EntryPoint = "YK_get_position_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_position_unit(UInt16 CardNo, UInt16 axis, ref double pos);
        [DllImport("MCC.dll", EntryPoint = "YK_set_encoder_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_encoder_unit(UInt16 CardNo, UInt16 axis, double pos);     //当前反馈位置
        [DllImport("MCC.dll", EntryPoint = "YK_get_encoder_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_encoder_unit(UInt16 CardNo, UInt16 axis, ref double pos);
        [DllImport("MCC.dll", EntryPoint = "YK_read_current_speed_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_read_current_speed_unit(UInt16 CardNo, UInt16 Axis, ref double current_speed);   //轴当前运行速度
                                                                                                                       //1.3 点位运动
        [DllImport("MCC.dll", EntryPoint = "YK_set_profile_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_profile_unit(UInt16 CardNo, UInt16 Axis, double Min_Vel, double Max_Vel, double Tacc, double Tdec, double Stop_Vel);   //单轴速度参数
        [DllImport("MCC.dll", EntryPoint = "YK_get_profile_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_profile_unit(UInt16 CardNo, UInt16 Axis, ref double Min_Vel, ref double Max_Vel, ref double Tacc, ref double Tdec, ref double Stop_Vel);
        [DllImport("MCC.dll", EntryPoint = "YK_pmove_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_pmove_unit(UInt16 CardNo, UInt16 axis, double Dist, UInt16 posi_mode);   //定长
        [DllImport("MCC.dll", EntryPoint = "YK_reset_target_position_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_reset_target_position_unit(UInt16 CardNo, UInt16 Axis, double New_Pos);     //在线变位
        [DllImport("MCC.dll", EntryPoint = "YK_update_target_position_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_update_target_position_unit(UInt16 CardNo, UInt16 Axis, double New_Pos);      //强行变位
        [DllImport("MCC.dll", EntryPoint = "YK_change_speed_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_change_speed_unit(UInt16 CardNo, UInt16 Axis, double New_Vel, double Taccdec);      //在线变速
                                                                                                                          //1.4 插补速度
        [DllImport("MCC.dll", EntryPoint = "YK_set_vector_profile_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_vector_profile_unit(UInt16 CardNo, UInt16 Crd, double Min_Vel, double Max_Vel, double Tacc, double Tdec, double Stop_Vel);   //单段插补速度参数
        [DllImport("MCC.dll", EntryPoint = "YK_get_vector_profile_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_vector_profile_unit(UInt16 CardNo, UInt16 Crd, ref double Min_Vel, ref double Max_Vel, ref double Tacc, ref double Tdec, ref double Stop_Vel);
        [DllImport("MCC.dll", EntryPoint = "YK_read_vector_speed_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_read_vector_speed_unit(UInt16 CardNo, UInt16 Crd);   //设置S型速度曲线参数
                                                                                           //1.5 插补运动		
        [DllImport("MCC.dll", EntryPoint = "YK_line_multicoor_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_line_multicoor_unit(UInt16 CardNo, UInt16 crd, UInt16 axisNum, UInt16[] axisList, double[] DistList, UInt16 posi_mode);//直线插补
        [DllImport("MCC.dll", EntryPoint = "YK_arc_move_multicoor_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_arc_move_multicoor_unit(UInt16 CardNo, UInt16 crd, UInt16[] AxisList, double[] Target_Pos, double[] Cen_Pos, UInt16 Arc_Dir, UInt16 posi_mode);//平面圆弧
                                                                                                                                                                                     //**************************************************************************************************************************************************************************
                                                                                                                                                                                     //MCC800S专用 ,连续插补等功能---------------------------------------------------------------------------------------------------
        [DllImport("MCC.dll", EntryPoint = "YK_get_axis_run_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_axis_run_mode(UInt16 CardNo, UInt16 axis, ref UInt16 run_mode);    //轴运动模式
        [DllImport("MCC.dll", EntryPoint = "YK_set_backlash_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_backlash_unit(UInt16 CardNo, UInt16 axis, double backlash);    //反向间隙
        [DllImport("MCC.dll", EntryPoint = "YK_get_backlash_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_backlash_unit(UInt16 CardNo, UInt16 axis, ref double backlash);
        [DllImport("MCC.dll", EntryPoint = "YK_t_pmove_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_t_pmove_unit(UInt16 CardNo, UInt16 axis, double Dist, UInt16 posi_mode);   //对称T型定长
        [DllImport("MCC.dll", EntryPoint = "YK_ex_t_pmove_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_ex_t_pmove_unit(UInt16 CardNo, UInt16 axis, double Dist, UInt16 posi_mode);    //非对称T型定长
        [DllImport("MCC.dll", EntryPoint = "YK_s_pmove_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_s_pmove_unit(UInt16 CardNo, UInt16 axis, double Dist, UInt16 posi_mode);   //对称S型定长
        [DllImport("MCC.dll", EntryPoint = "YK_ex_s_pmove_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_ex_s_pmove_unit(UInt16 CardNo, UInt16 axis, double Dist, UInt16 posi_mode);    //非对称S型定长
        [DllImport("MCC.dll", EntryPoint = "YK_line_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_line_unit(UInt16 CardNo, UInt16 Crd, UInt16 AxisNum, UInt16[] AxisList, double[] Target_Pos, UInt16 posi_mode);    //单段直线
        [DllImport("MCC.dll", EntryPoint = "YK_arc_move_center_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_arc_move_center_unit(UInt16 CardNo, UInt16 Crd, UInt16 AxisNum, UInt16[] AxisList, double[] Target_Pos, double[] Cen_Pos, UInt16 Arc_Dir, Int32 Circle, UInt16 posi_mode);     //圆心终点式圆弧/螺旋线/渐开线
        [DllImport("MCC.dll", EntryPoint = "YK_arc_move_radius_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_arc_move_radius_unit(UInt16 CardNo, UInt16 Crd, UInt16 AxisNum, UInt16[] AxisList, double[] Target_Pos, double Arc_Radius, UInt16 Arc_Dir, Int32 Circle, UInt16 posi_mode);    //半径终点式圆弧/螺旋线
        [DllImport("MCC.dll", EntryPoint = "YK_arc_move_3points_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_arc_move_3points_unit(UInt16 CardNo, UInt16 Crd, UInt16 AxisNum, UInt16[] AxisList, double[] Target_Pos, double[] Mid_Pos, Int32 Circle, UInt16 posi_mode);     //三点式圆弧/螺旋线
        [DllImport("MCC.dll", EntryPoint = "YK_rectangle_move_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_rectangle_move_unit(UInt16 CardNo, UInt16 Crd, UInt16 AxisNum, UInt16[] AxisList, double[] TargetPos, double[] MaskPos, Int32 Count, UInt16 rect_mode, UInt16 posi_mode);     //矩形区域插补，单段插补指令        
        [DllImport("MCC.dll", EntryPoint = "YK_set_vector_s_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_vector_s_profile(UInt16 CardNo, UInt16 Crd, UInt16 s_mode, double s_para);   //设置S型速度曲线参数
        [DllImport("MCC.dll", EntryPoint = "YK_get_vector_s_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_vector_s_profile(UInt16 CardNo, UInt16 Crd, UInt16 s_mode, ref double s_para);
        //连续插补--------------------------------------------------------------------------------------------------------------------------------------------------
        [DllImport("MCC.dll", EntryPoint = "YK_conti_open_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_open_list(UInt16 CardNo, UInt16 Crd, UInt16 AxisNum, UInt16[] AxisList);     //打开连续缓存区
        [DllImport("MCC.dll", EntryPoint = "YK_conti_close_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_close_list(UInt16 CardNo, UInt16 Crd);     //关闭连续缓存区
        [DllImport("MCC.dll", EntryPoint = "YK_conti_stop_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_stop_list(UInt16 CardNo, UInt16 Crd, UInt16 stop_mode);      //连续插补中停止
        [DllImport("MCC.dll", EntryPoint = "YK_conti_pause_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_pause_list(UInt16 CardNo, UInt16 Crd);      //连续插补中暂停
        [DllImport("MCC.dll", EntryPoint = "YK_conti_start_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_start_list(UInt16 CardNo, UInt16 Crd);      //开始连续插补
        [DllImport("MCC.dll", EntryPoint = "YK_conti_remain_space", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 YK_conti_remain_space(UInt16 CardNo, UInt16 Crd);     //查连续插补剩余缓存数
        [DllImport("MCC.dll", EntryPoint = "YK_conti_read_current_mark", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 YK_conti_read_current_mark(UInt16 CardNo, UInt16 Crd);      //读取当前连续插补段的标号
        [DllImport("MCC.dll", EntryPoint = "YK_conti_set_blend", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_set_blend(UInt16 CardNo, UInt16 Crd, UInt16 enable);      //blend拐角过度模式
        [DllImport("MCC.dll", EntryPoint = "YK_conti_get_blend", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_get_blend(UInt16 CardNo, UInt16 Crd, ref UInt16 enable);
        [DllImport("MCC.dll", EntryPoint = "YK_conti_set_profile_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_set_profile_unit(UInt16 CardNo, UInt16 Crd, double Min_Vel, double Max_vel, double Tacc, double Tdec, double Stop_Vel); //设置连续插补速度
        [DllImport("MCC.dll", EntryPoint = "YK_conti_set_s_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_set_s_profile(UInt16 CardNo, UInt16 Crd, UInt16 s_mode, double s_para);     //设置连续插补平滑时间
        [DllImport("MCC.dll", EntryPoint = "YK_conti_get_s_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_get_s_profile(UInt16 CardNo, UInt16 Crd, UInt16 s_mode, ref double s_para);
        [DllImport("MCC.dll", EntryPoint = "YK_conti_set_pause_output", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_set_pause_output(UInt16 CardNo, UInt16 Crd, UInt16 action, Int32 mask, Int32 state);     //暂停时IO输出 action 0, 不工作；1， 暂停时输出io_state; 2 暂停时输出io_state, 继续运行时首先恢复原来的io; 3,在2的基础上，停止时也生效。
        [DllImport("MCC.dll", EntryPoint = "YK_conti_get_pause_output", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_get_pause_output(UInt16 CardNo, UInt16 Crd, ref UInt16 action, ref Int32 mask, ref Int32 state);
        [DllImport("MCC.dll", EntryPoint = "YK_conti_set_override", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_set_override(UInt16 CardNo, UInt16 Crd, double Percent);      //设置每段速度比例  缓冲区指令
        [DllImport("MCC.dll", EntryPoint = "YK_conti_change_speed_ratio", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_change_speed_ratio(UInt16 CardNo, UInt16 Crd, double Percent);    //连续插补动态变速
        [DllImport("MCC.dll", EntryPoint = "YK_conti_get_run_state", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_get_run_state(UInt16 CardNo, UInt16 Crd);      //读取连续插补状态：0-运行，1-暂停，2-正常停止，3-异常停止
        [DllImport("MCC.dll", EntryPoint = "YK_conti_check_done", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_check_done(UInt16 CardNo, UInt16 Crd);      //检测连续插补运动状态：0-运行，1-停止
        [DllImport("MCC.dll", EntryPoint = "YK_conti_wait_input", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_wait_input(UInt16 CardNo, UInt16 Crd, UInt16 bitno, UInt16 on_off, double TimeOut, Int32 mark);      //设置连续插补等待输入
        [DllImport("MCC.dll", EntryPoint = "YK_conti_delay_outbit_to_start", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_delay_outbit_to_start(UInt16 CardNo, UInt16 Crd, UInt16 bitno, UInt16 on_off, double delay_value, UInt16 delay_mode, double ReverseTime); //相对于轨迹段起点IO滞后输出
        [DllImport("MCC.dll", EntryPoint = "YK_conti_delay_outbit_to_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_delay_outbit_to_stop(UInt16 CardNo, UInt16 Crd, UInt16 bitno, UInt16 on_off, double delay_time, double ReverseTime); //相对于轨迹段终点IO滞后输出
        [DllImport("MCC.dll", EntryPoint = "YK_conti_ahead_outbit_to_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_ahead_outbit_to_stop(UInt16 CardNo, UInt16 Crd, UInt16 bitno, UInt16 on_off, double ahead_value, UInt16 ahead_mode, double ReverseTime);  //相对轨迹段终点IO提前输出
        [DllImport("MCC.dll", EntryPoint = "YK_conti_accurate_outbit_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_accurate_outbit_unit(UInt16 CardNo, UInt16 Crd, UInt16 cmp_no, UInt16 on_off, UInt16 map_axis, double abs_pos, UInt16 pos_source, double ReverseTime); //确定位置精确输出
        [DllImport("MCC.dll", EntryPoint = "YK_conti_write_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_write_outbit(UInt16 CardNo, UInt16 Crd, UInt16 bitno, UInt16 on_off, double ReverseTime);      //缓冲区立即IO输出
        [DllImport("MCC.dll", EntryPoint = "YK_conti_clear_io_action", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_clear_io_action(UInt16 CardNo, UInt16 Crd, UInt32 IoMask);     //清除段内未执行完的IO动作,防止在下一段执行 
        [DllImport("MCC.dll", EntryPoint = "YK_conti_delay", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_delay(UInt16 CardNo, UInt16 Crd, double delay_time, Int32 mark);     //添加延时指令
        [DllImport("MCC.dll", EntryPoint = "YK_conti_line_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_line_unit(UInt16 CardNo, UInt16 Crd, UInt16 AxisNum, UInt16[] AxisList, double[] Target_Pos, UInt16 posi_mode, Int32 mark); //连续插补直线
        [DllImport("MCC.dll", EntryPoint = "YK_conti_arc_move_center_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_arc_move_center_unit(UInt16 CardNo, UInt16 Crd, UInt16 AxisNum, UInt16[] AxisList, double[] Target_Pos, double[] Cen_Pos, UInt16 Arc_Dir, Int32 Circle, UInt16 posi_mode, Int32 mark);     //圆心终点式圆弧/螺旋线/渐开线
        [DllImport("MCC.dll", EntryPoint = "YK_conti_arc_move_radius_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_arc_move_radius_unit(UInt16 CardNo, UInt16 Crd, UInt16 AxisNum, UInt16[] AxisList, double[] Target_Pos, double Arc_Radius, UInt16 Arc_Dir, Int32 Circle, UInt16 posi_mode, Int32 mark);    //半径终点式圆弧/螺旋线
        [DllImport("MCC.dll", EntryPoint = "YK_conti_arc_move_3points_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_arc_move_3points_unit(UInt16 CardNo, UInt16 Crd, UInt16 AxisNum, UInt16[] AxisList, double[] Target_Pos, double[] Mid_Pos, Int32 Circle, UInt16 posi_mode, Int32 mark);     //三点式圆弧/螺旋线
        [DllImport("MCC.dll", EntryPoint = "YK_conti_set_involute_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_set_involute_mode(UInt16 CardNo, UInt16 Crd, UInt16 mode);      //设置螺旋线是否封闭
        [DllImport("MCC.dll", EntryPoint = "YK_conti_get_involute_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_get_involute_mode(UInt16 CardNo, UInt16 Crd, ref UInt16 mode);   //读取螺旋线是否封闭设置
        [DllImport("MCC.dll", EntryPoint = "YK_conti_rectangle_move_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_rectangle_move_unit(UInt16 CardNo, UInt16 Crd, UInt16 AxisNum, UInt16[] AxisList, double[] TargetPos, double[] MaskPos, Int32 Count, UInt16 rect_mode, UInt16 posi_mode, Int32 mark);     //矩形区域插补，连续插补指令
        [DllImport("MCC.dll", EntryPoint = "YK_calculate_arclength_center", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_calculate_arclength_center(double[] start_pos, double[] target_pos, double[] cen_pos, UInt16 arc_dir, double circle, ref double ArcLength);      //计算圆心圆弧弧长
        [DllImport("MCC.dll", EntryPoint = "YK_conti_pmove_unit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_pmove_unit(UInt16 CardNo, UInt16 Crd, UInt16 axis, double dist, UInt16 posi_mode, UInt16 mode, Int32 imark);      //控制外轴运动
        [DllImport("MCC.dll", EntryPoint = "YK_calculate_arclength_radius", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_calculate_arclength_radius(double[] start_pos, double[] target_pos, double arc_radius, UInt16 arc_dir, double circle, ref double ArcLength);     //计算半径圆弧弧长
        [DllImport("MCC.dll", EntryPoint = "YK_calculate_arclength_3point", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_calculate_arclength_3point(double[] start_pos, double[] mid_pos, double[] target_pos, double circle, ref double ArcLength);      //计算三点圆弧弧长
                                                                                                                                                                       //MCC800S小线段前瞻函数
        [DllImport("MCC.dll", EntryPoint = "YK_conti_set_lookahead_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_set_lookahead_mode(UInt16 CardNo, UInt16 Crd, UInt16 enable, Int32 LookaheadSegments, double PathError, double LookaheadAcc);
        [DllImport("MCC.dll", EntryPoint = "YK_conti_get_lookahead_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_get_lookahead_mode(UInt16 CardNo, UInt16 Crd, ref UInt16 enable, ref Int32 LookaheadSegments, ref double PathError, ref double LookaheadAcc);
        //MCC800S专用 连续插补PWM输出-------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //设置PWM开关的占空比
        [DllImport("MCC.dll", EntryPoint = "YK_set_pwm_onoff_duty", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_pwm_onoff_duty(UInt16 CardNo, UInt16 PwmNo, double fOnDuty, double fOffDuty);
        [DllImport("MCC.dll", EntryPoint = "YK_get_pwm_onoff_duty", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_pwm_onoff_duty(UInt16 CardNo, UInt16 PwmNo, ref double fOnDuty, ref double fOffDuty);
        [DllImport("MCC.dll", EntryPoint = "YK_conti_set_pwm_output", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_set_pwm_output(UInt16 CardNo, UInt16 Crd, UInt16 pwm_no, double fDuty, double fFre);//连续插补中设置PWM输出
        [DllImport("MCC.dll", EntryPoint = "YK_conti_set_pwm_follow_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_set_pwm_follow_speed(UInt16 CardNo, UInt16 Crd, UInt16 pwm_no, UInt16 mode, double MaxVel, double MaxValue, double OutValue);//PWM速度跟随
        [DllImport("MCC.dll", EntryPoint = "YK_conti_get_pwm_follow_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_get_pwm_follow_speed(UInt16 CardNo, UInt16 Crd, UInt16 pwm_no, ref UInt16 mode, ref double MaxVel, ref double MaxValue, ref double OutValue);
        [DllImport("MCC.dll", EntryPoint = "YK_conti_delay_pwm_to_start", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_delay_pwm_to_start(UInt16 UInt16, UInt16 Crd, UInt16 pwmno, UInt16 on_off, double delay_value, UInt16 delay_mode, double ReverseTime);//相对于轨迹段起点PWM滞后输出
        [DllImport("MCC.dll", EntryPoint = "YK_conti_ahead_pwm_to_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_ahead_pwm_to_stop(UInt16 CardNo, UInt16 Crd, UInt16 pwmno, UInt16 on_off, double ahead_value, UInt16 ahead_mode, double ReverseTime);//相对轨迹段终点PWM提前输出
        [DllImport("MCC.dll", EntryPoint = "YK_conti_write_pwm", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_conti_write_pwm(UInt16 CardNo, UInt16 Crd, UInt16 pwmno, UInt16 on_off, double ReverseTime);    //缓冲区立即PWM输出
                                                                                                                                      //圆弧限速-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        [DllImport("MCC.dll", EntryPoint = "YK_set_arc_limit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_arc_limit(UInt16 CardNo, UInt16 Crd, UInt16 Enable, double MaxCenAcc, double MaxArcError);
        [DllImport("MCC.dll", EntryPoint = "YK_get_arc_limit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_arc_limit(UInt16 CardNo, UInt16 Crd, ref UInt16 Enable, ref double MaxCenAcc, ref double MaxArcError);
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [DllImport("MCC.dll", EntryPoint = "YK_set_dec_stop_dist", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_dec_stop_dist(UInt16 CardNo, UInt16 axis, Int32 dist);//设置减速停止距离
        [DllImport("MCC.dll", EntryPoint = "YK_get_dec_stop_dist", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_dec_stop_dist(UInt16 CardNo, UInt16 axis, ref Int32 dist);
        //MCC600S定制接线盒----------------------------------------------------------------------------------------------------------------------------------------------
        [DllImport("MCC.dll", EntryPoint = "YK_set_da_enable", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_da_enable(UInt16 CardNo, UInt16 enable);//开启DA输出
        [DllImport("MCC.dll", EntryPoint = "YK_get_da_enable", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_da_enable(UInt16 CardNo, ref UInt16 enable);
        [DllImport("MCC.dll", EntryPoint = "YK_set_da_output", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_set_da_output(UInt16 CardNo, UInt16 channel, double Vout);//设置DA输出
        [DllImport("MCC.dll", EntryPoint = "YK_get_da_output", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_da_output(UInt16 CardNo, UInt16 channel, ref double Vout);
        //读取AD输入------------------------------------------------------------------------------------------------------------------------------------------------------
        [DllImport("MCC.dll", EntryPoint = "YK_get_ad_input", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short YK_get_ad_input(UInt16 CardNo, UInt16 channel, ref double Vout);
    }
}
