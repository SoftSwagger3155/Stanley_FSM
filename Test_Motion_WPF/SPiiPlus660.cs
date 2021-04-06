using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LX_MCPNet.Motion;
using LX_MCPNet.Data;
using System.Runtime.InteropServices;
using SPIIPLUSCOM660Lib;
using System.Threading;

namespace Test_Motion_WPF
{
    public enum ComMode
    {
        COM_PCI_BUS,
        COM_ETHERNET,
        COM_SERIAL,
        COM_DIRECT
    }

    public class SPiiPlus660 : AxisBase
    {
        [DllImport("ACSCL_x86.dll", EntryPoint = "acsc_GetPCICards")]
        private static extern int acsc_GetPCICards32([In] [Out] SPiiPlus660.ACSC_PCI_SLOT[] Cards, int Count, out int ObtainedCards);
        
        [DllImport("ACSCL_x64.dll", EntryPoint = "acsc_GetPCICards")]
        private static extern int acsc_GetPCICards64([In] [Out] SPiiPlus660.ACSC_PCI_SLOT[] Cards, int Count, out int ObtainedCards);

        private static int acsc_GetPCICards([In] [Out] SPiiPlus660.ACSC_PCI_SLOT[] Cards, int Count, out int ObtainedCards)
        {
            return Environment.Is64BitProcess ? SPiiPlus660.acsc_GetPCICards64(Cards, Count, out ObtainedCards) : SPiiPlus660.acsc_GetPCICards32(Cards, Count, out ObtainedCards);
        }
        
        public SPiiPlus660(int comPort, int baudRate, ref MtrConfig mtrConfig, ref MtrTable mtrTable, ref MtrSpeed mtrSpeed, bool simulation) : base(ref mtrConfig, ref mtrTable, ref mtrSpeed, simulation)
        {
            if (SPiiPlus660._ch == null)
            {
                SPiiPlus660._ch = (Channel)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("73C1FDD7-7A9F-45CB-AC12-73AC50F9A3C8")));
            }
            this.NumberOfAxisPerCard = 8;
            this._comPort = comPort;
            this._baudRate = baudRate;
            this._comMode = ComMode.COM_SERIAL;
            this.isShowPulse = false;
        }
        public SPiiPlus660(string ipAddress, bool dataGram, ref MtrConfig mtrConfig, ref MtrTable mtrTable, ref MtrSpeed mtrSpeed, bool simulation) : base(ref mtrConfig, ref mtrTable, ref mtrSpeed, simulation)
        {
            this.NumberOfAxisPerCard = 8;
            this._ipAddress = ipAddress;
            if (SPiiPlus660._ch == null)
            {
                SPiiPlus660._ch = (Channel)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("73C1FDD7-7A9F-45CB-AC12-73AC50F9A3C8")));
            }
            if (dataGram)
            {
                this._protocol = SPiiPlus660._ch.ACSC_SOCKET_DGRAM_PORT;
            }
            else
            {
                this._protocol = SPiiPlus660._ch.ACSC_SOCKET_STREAM_PORT;
            }
            this.isShowPulse = false;
            this._comMode = ComMode.COM_ETHERNET;
        }
        public SPiiPlus660(ref MtrConfig mtrConfig, ref MtrTable mtrTable, ref MtrSpeed mtrSpeed, bool pciBus, bool simulation) : base(ref mtrConfig, ref mtrTable, ref mtrSpeed, simulation)
        {
            this.NumberOfAxisPerCard = 8;
            if (SPiiPlus660._ch == null)
            {
                SPiiPlus660._ch = (Channel)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("73C1FDD7-7A9F-45CB-AC12-73AC50F9A3C8")));
            }
            if (pciBus)
            {
                this._comMode = ComMode.COM_PCI_BUS;
            }
            else
            {
                this._comMode = ComMode.COM_DIRECT;
            }
            this.isShowPulse = false;
        }
        public SPiiPlus660(int comPort, int baudRate, ref MtrConfig mtrConfig, ref MtrTable mtrTable, ref MtrSpeed mtrSpeed, ref MtrMisc mtrMisc, bool simulation) : base(ref mtrConfig, ref mtrTable, ref mtrSpeed, ref mtrMisc, simulation)
        {
            if (SPiiPlus660._ch == null)
            {
                SPiiPlus660._ch = (Channel)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("73C1FDD7-7A9F-45CB-AC12-73AC50F9A3C8")));
            }
            this.NumberOfAxisPerCard = 8;
            this._comPort = comPort;
            this._baudRate = baudRate;
            this._comMode = ComMode.COM_SERIAL;
            this.isShowPulse = false;
        }
        public SPiiPlus660(string ipAddress, bool dataGram, ref MtrConfig mtrConfig, ref MtrTable mtrTable, ref MtrSpeed mtrSpeed, ref MtrMisc mtrMisc, bool simulation) : base(ref mtrConfig, ref mtrTable, ref mtrSpeed, ref mtrMisc, simulation)
        {
            if (SPiiPlus660._ch == null)
            {
                SPiiPlus660._ch = (Channel)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("73C1FDD7-7A9F-45CB-AC12-73AC50F9A3C8")));
            }
            this.NumberOfAxisPerCard = 8;
            this._ipAddress = ipAddress;
            if (dataGram)
            {
                this._protocol = SPiiPlus660._ch.ACSC_SOCKET_DGRAM_PORT;
            }
            else
            {
                this._protocol = SPiiPlus660._ch.ACSC_SOCKET_STREAM_PORT;
            }
            this.isShowPulse = false;
            this._comMode = ComMode.COM_ETHERNET;
        }
        public SPiiPlus660(ref MtrConfig mtrConfig, ref MtrTable mtrTable, ref MtrSpeed mtrSpeed, ref MtrMisc mtrMisc, bool pciBus, bool simulation) : base(ref mtrConfig, ref mtrTable, ref mtrSpeed, ref mtrMisc, simulation)
        {
            if (SPiiPlus660._ch == null)
            {
                SPiiPlus660._ch = (Channel)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("73C1FDD7-7A9F-45CB-AC12-73AC50F9A3C8")));
            }
            this.NumberOfAxisPerCard = 8;
            if (pciBus)
            {
                this._comMode = ComMode.COM_PCI_BUS;
            }
            else
            {
                this._comMode = ComMode.COM_DIRECT;
            }
            this.isShowPulse = false;
        }
        
        public override void Dispose()
        {
            this.uninitialization(0);
        }
        
        public override bool IsRunning()
        {
            bool flag;
            if (this.isSimulation)
            {
                flag = false;
            }
            else
            {
                object obj = new object();
                int motorState = SPiiPlus660._ch.GetMotorState((int)base.MtrTable.AxisNo);
                flag = (Convert.ToBoolean(motorState & SPiiPlus660._ch.ACSC_MST_MOVE) || Convert.ToBoolean(motorState & SPiiPlus660._ch.ACSC_MST_ACC));
            }
            return flag;
        }
        
        public override bool IsHome()
        {
            return base.MtrTable.HomeFlag == 1;
        }
        
        public override void set_servo(bool set)
        {
            if (!this.isSimulation)
            {
                if (set)
                {
                    SPiiPlus660._ch.Enable((int)base.MtrTable.AxisNo);
                    SPiiPlus660._ch.WaitMotorEnabled((int)base.MtrTable.AxisNo, Convert.ToInt32(set), 1000);
                    if (base.MtrTable.SecondAxisNumber != -1)
                    {
                        SPiiPlus660._ch.Enable(base.MtrTable.SecondAxisNumber);
                        SPiiPlus660._ch.WaitMotorEnabled(base.MtrTable.SecondAxisNumber, Convert.ToInt32(set), 1000);
                    }
                }
                else
                {
                    SPiiPlus660._ch.Disable((int)base.MtrTable.AxisNo);
                    SPiiPlus660._ch.WaitMotorEnabled((int)base.MtrTable.AxisNo, Convert.ToInt32(set), 1000);
                    if (base.MtrTable.SecondAxisNumber != -1)
                    {
                        SPiiPlus660._ch.Disable(base.MtrTable.SecondAxisNumber);
                        SPiiPlus660._ch.WaitMotorEnabled(base.MtrTable.SecondAxisNumber, Convert.ToInt32(set), 1000);
                    }
                }
            }
            base.IsServoOn = set;
        }
        
        public override void set_newpos(double newPos)
        {
            base.MtrTable.NewPos = newPos;
        }
        
        public override int wait_home_done(int waitDelay)
        {
            this.isStopReq = false;
            int num = 0;
            if (!this.isSimulation && base.MtrTable.UseACSHomingBuffer)
            {
                bool flag = this.WaitForACSVariableValue(base.MtrTable.ACSHomeFlagName, this._homingCompletedValue, waitDelay);
                if (flag)
                {
                    this.sd_stop();
                    num = 9;
                }
                else
                {
                    this.set_servo(true);
                }
            }
            else if (this.isCustomHomingStarted)
            {
                DateTime now = DateTime.Now;
                TimeSpan timeSpan = DateTime.Now - now;
                while (this.isCustomHomingStarted && timeSpan.TotalMilliseconds < (double)waitDelay)
                {
                    timeSpan = DateTime.Now - now;
                    Thread.Sleep(10);
                }
                num = (int)this.customHomingFlag;
            }
            else if (this.isSimulation)
            {
                DateTime now = DateTime.Now;
                TimeSpan timeSpan = DateTime.Now - now;
                while (timeSpan.TotalMilliseconds < 500.0)
                {
                    timeSpan = DateTime.Now - now;
                    Thread.Sleep(10);
                }
                base.MtrTable.CurPos = 0.0;
                this.currentPulse = 0.0;
                base.CurrentPhysicalPos = 0.0;
                base.TargetPos = 0.0;
                num = 0;
            }
            else
            {
                num = this.wait_motion_done(waitDelay);
            }
            if (num == 0 || num == 2)
            {
                base.MtrTable.HomeFlag = 1;
                base.MtrTable.ErrorFlag = 0;
                if (!base.MtrTable.UseACSHomingBuffer)
                {
                    this.set_current_pos(0.0);
                }
                base.HasHome = true;
                num = 0;
            }
            base.IsBusy = false;
            return num;
        }
        
        protected override int wait_motion_complete()
        {
            return 1;
        }
        
        public override int wait_motion_done(int waitDelay)
        {
            this.isStopReq = false;
            object obj = new object();
            int acsc_MST_INPOS = SPiiPlus660._ch.ACSC_MST_INPOS;
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = DateTime.Now - now;
            base.IsBusy = true;
            bool flag = false;
            int num = base.MtrTable.MotionDoneWaitSleep;
            if (num < 1)
            {
                num = 1;
            }
            AxisBase.MotionStatus motionStatus;
            for (; ; )
            {
                motionStatus = this.check_motion_status();
                if (motionStatus != AxisBase.MotionStatus.MT_MOVING)
                {
                    break;
                }
                if (flag)
                {
                    break;
                }
                Thread.Sleep(num);
                if (this.isStopReq)
                {
                    Thread.Sleep(100);
                    flag = true;
                }
                if ((DateTime.Now - now).TotalMilliseconds > (double)waitDelay)
                {
                    goto Block_5;
                }
            }
            goto IL_CB;
        Block_5:
            motionStatus = AxisBase.MotionStatus.MT_TIMEOUT;
        IL_CB:
            if (!this.isStopReq && this.isSimulation)
            {
                base.MtrTable.CurPos = base.MtrTable.NewPos;
            }
            this.isStopReq = false;
            if (motionStatus == AxisBase.MotionStatus.MT_TIMEOUT)
            {
                this.sd_stop();
            }
            if (motionStatus == AxisBase.MotionStatus.MT_DONE)
            {
                int num2 = base.MtrTable.InpositionSettlingDwell;
                if (num2 < 0)
                {
                    num2 = 0;
                }
                Thread.Sleep(num2);
            }
            double current_pos = this.get_current_pos();
            base.IsBusy = false;
            return (int)motionStatus;
        }
        
        public override AxisBase.MotionStatus check_motion_status()
        {
            int acsc_MST_INPOS = SPiiPlus660._ch.ACSC_MST_INPOS;
            return this.isSimulation ? base.SimulateMotionStatusCheck() : this.check_mtr_status();
        }
        
        public override bool IsMoving()
        {
            int motorState = SPiiPlus660._ch.GetMotorState((int)base.MtrTable.AxisNo);
            bool flag = Convert.ToBoolean(motorState & SPiiPlus660._ch.ACSC_MST_MOVE);
            return base.IsMoving();
        }
        
        private AxisBase.MotionStatus check_mtr_status()
        {
            AxisBase.MotionStatus motionStatus = AxisBase.MotionStatus.MT_DONE;
            int motorState = SPiiPlus660._ch.GetMotorState((int)base.MtrTable.AxisNo);
            if (Convert.ToBoolean(motorState & SPiiPlus660._ch.ACSC_MST_MOVE))
            {
                motionStatus = AxisBase.MotionStatus.MT_MOVING;
            }
            else if (Convert.ToBoolean(motorState & SPiiPlus660._ch.ACSC_MST_INPOS) && !Convert.ToBoolean(motorState & SPiiPlus660._ch.ACSC_MST_MOVE))
            {
                motionStatus = AxisBase.MotionStatus.MT_DONE;
            }
            if (!this.BypassAlarmCheck)
            {
                if ((this.get_io_sts() & 2) == 2)
                {
                    base.MtrTable.HomeFlag = 0;
                    motionStatus = AxisBase.MotionStatus.MT_ALARM;
                }
            }
            if (!this.BypassLimitCheck)
            {
                if ((!base.MtrTable.IsDisableLeftLimit && this.get_nel_signal()) || (!base.MtrTable.IsDisableRightLimit && this.get_pel_signal()))
                {
                    motionStatus = AxisBase.MotionStatus.MT_LIMIT;
                }
            }
            if (base.MtrTable.pCriticalEvent != null)
            {
                if (base.MtrTable.pCriticalEvent())
                {
                    motionStatus = AxisBase.MotionStatus.MT_CRITICAL;
                    base.MtrTable.HomeFlag = 0;
                    this.emg_stop();
                }
            }
            if (base.MtrTable.pFwdSensing != null)
            {
                if (base.MtrTable.pFwdSensing())
                {
                    motionStatus = AxisBase.MotionStatus.MT_LIMIT;
                }
            }
            if (base.MtrTable.pRevSensing != null)
            {
                if (base.MtrTable.pRevSensing())
                {
                    motionStatus = AxisBase.MotionStatus.MT_LIMIT;
                }
            }
            Thread.Sleep(0);
            return motionStatus;
        }
        
        public override void set_zero_pos()
        {
            this.set_current_pos(0.0);
        }
        
        public override void clear_home_flag()
        {
            base.MtrTable.HomeFlag = 0;
        }
        
        public override void abnormal_stop()
        {
            this.emg_stop();
        }
        
        public override int get_motion_sts()
        {
            object obj = new object();
            return SPiiPlus660._ch.GetMotorState((int)base.MtrTable.AxisNo);
        }
        
        public override int get_io_sts()
        {
            object obj = new object();
            IOSTATUS iostatus = (IOSTATUS)0;
            int safetyInputPort = SPiiPlus660._ch.GetSafetyInputPort((int)base.MtrTable.AxisNo);
            if (Convert.ToBoolean(safetyInputPort & SPiiPlus660._ch.ACSC_SAFETY_LL))
            {
                iostatus |= IOSTATUS.MEL;
            }
            if (Convert.ToBoolean(safetyInputPort & SPiiPlus660._ch.ACSC_SAFETY_RL))
            {
                iostatus |= IOSTATUS.PEL;
            }
            return (int)iostatus;
        }
        
        public override int home_move(double revstep, double strvel, double maxvel, double tacc, int waitDelay = 0)
        {
            int num;
            if (this.inhibit)
            {
                num = 0;
            }
            else if (base.IsProhibitToHome())
            {
                num = 7;
            }
            else
            {
                base.MtrTable.HomeFlag = 0;
                base.HasHome = false;
                base.LastDirection = AxisBase.LastMoveDirection.Unknown;
                if (this.isSimulation)
                {
                    base.MtrTable.NewPos = 0.0;
                    base.MtrTable.DistanceToMove = 0.0 - this.currentPulse;
                    base.MtrTable.EstimateTimeTaken = 0.01 * (Math.Abs(base.MtrTable.DistanceToMove) / maxvel);
                    num = 0;
                }
                else
                {
                    AxisBase.MotionStatus motionStatus;
                    if (base.MtrTable.UseACSHomingBuffer)
                    {
                        motionStatus = this.StartBufferHoming((int)base.MtrTable.ACSHomingBuffer);
                    }
                    else if (base.MtrTable.OnBoardHome == 1)
                    {
                        motionStatus = this.StartOnBoardHoming(revstep, strvel, maxvel, tacc, waitDelay);
                    }
                    else if (base.MtrTable.pHomeSensing != null)
                    {
                        motionStatus = this.StartExternalSenseHoming(revstep, strvel, maxvel, tacc, waitDelay);
                    }
                    else
                    {
                        motionStatus = this.StartCustomHomingThread();
                    }
                    num = (int)motionStatus;
                }
            }
            return num;
        }
        
        private AxisBase.MotionStatus StartBufferHoming(int bufferNo)
        {
            AxisBase.MotionStatus motionStatus = AxisBase.MotionStatus.MT_DONE;
            this.result = 1;
            try
            {
                base.IsBusy = true;
                SPiiPlus660._ch.StopBuffer(bufferNo);
                this.set_servo(false);
                SPiiPlus660._ch.WriteVariable(2, base.MtrTable.ACSHomeFlagName, SPiiPlus660._ch.ACSC_NONE, SPiiPlus660._ch.ACSC_NONE, SPiiPlus660._ch.ACSC_NONE, SPiiPlus660._ch.ACSC_NONE, SPiiPlus660._ch.ACSC_NONE);
                SPiiPlus660._ch.RunBuffer(bufferNo, "");
                bool flag = this.WaitForACSVariableValue(base.MtrTable.ACSHomeFlagName, 0, 5000);
                if (flag)
                {
                    motionStatus = AxisBase.MotionStatus.MT_HOMING_ERROR;
                }
            }
            catch (Exception ex)
            {
                motionStatus = AxisBase.MotionStatus.MT_HOMING_ERROR;
            }
            return motionStatus;
        }
        
        public static bool StartRunBuffer(int bufferNo)
        {
            bool flag = false;
            if (SPiiPlus660._ch != null)
            {
                try
                {
                    SPiiPlus660._ch.RunBuffer(bufferNo, "");
                    flag = true;
                }
                catch (Exception ex)
                {
                }
            }
            return flag;
        }
        
        public static bool WriteBufferVariable(double val, string varName, int bufferNumber = -1)
        {
            bool flag = false;
            if (SPiiPlus660._ch != null)
            {
                try
                {
                    if (bufferNumber == -1)
                    {
                        bufferNumber = SPiiPlus660._ch.ACSC_NONE;
                    }
                    SPiiPlus660._ch.WriteVariable(val, varName, SPiiPlus660._ch.ACSC_NONE, -1, -1, -1, -1);
                    flag = true;
                }
                catch (Exception ex)
                {
                }
            }
            return flag;
        }
        
        public static bool WriteBufferVariable(ref int val, string varName, int bufferNumber = -1)
        {
            bool flag = false;
            if (SPiiPlus660._ch != null)
            {
                try
                {
                    if (bufferNumber == -1)
                    {
                        bufferNumber = SPiiPlus660._ch.ACSC_NONE;
                    }
                    SPiiPlus660._ch.WriteVariable(val, varName, bufferNumber, -1, -1, -1, -1);
                    flag = true;
                }
                catch (Exception ex)
                {
                }
            }
            return flag;
        }
        
        public static bool ReadBufferVariable(ref double val, string varName, int bufferNumber = -1)
        {
            bool flag = false;
            if (SPiiPlus660._ch != null)
            {
                if (bufferNumber == -1)
                {
                    bufferNumber = SPiiPlus660._ch.ACSC_NONE;
                }
                object obj = SPiiPlus660._ch.ReadVariable(varName, bufferNumber, -1, -1, -1, -1);
                if (double.TryParse(obj.ToString(), out val))
                {
                    flag = true;
                }
            }
            return flag;
        }
        
        public static bool ReadBufferVariable(ref int val, string varName)
        {
            bool flag = false;
            if (SPiiPlus660._ch != null)
            {
                object obj = SPiiPlus660._ch.ReadVariable(varName, SPiiPlus660._ch.ACSC_NONE, -1, -1, -1, -1);
                if (int.TryParse(obj.ToString(), out val))
                {
                    flag = true;
                }
            }
            return flag;
        }
        
        public static bool StopBufferRunning(int bufferNumber)
        {
            bool flag = false;
            if (SPiiPlus660._ch != null)
            {
                try
                {
                    SPiiPlus660._ch.StopBuffer(bufferNumber);
                    flag = true;
                }
                catch (Exception ex)
                {
                }
            }
            return flag;
        }
        
        public static bool WaitForBufferVariableChange(string varName, double oldVal, ref double newVal, int timeoutMillisecond)
        {
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = DateTime.Now - now;
            object obj = new object();
            while (SPiiPlus660._ch != null)
            {
                Thread.Sleep(1);
                obj = SPiiPlus660._ch.ReadVariable(varName, SPiiPlus660._ch.ACSC_NONE, -1, -1, -1, -1);
                timeSpan = DateTime.Now - now;
                if (double.TryParse(obj.ToString(), out newVal))
                {
                    if (oldVal == newVal)
                    {
                        if (timeSpan.TotalMilliseconds < (double)timeoutMillisecond)
                        {
                            continue;
                        }
                    }
                }
            //IL_98:
                return oldVal != newVal;
            }
            //goto IL_98;

            return oldVal != newVal;
        }
        
        public static bool WaitForBufferVariableChange(string varName, int oldVal, ref int newVal, int timeoutMillisecond)
        {
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = DateTime.Now - now;
            object obj = new object();
            while (SPiiPlus660._ch != null)
            {
                Thread.Sleep(1);
                obj = SPiiPlus660._ch.ReadVariable(varName, SPiiPlus660._ch.ACSC_NONE, -1, -1, -1, -1);
                timeSpan = DateTime.Now - now;
                if (int.TryParse(obj.ToString(), out newVal))
                {
                    if (oldVal == newVal)
                    {
                        if (timeSpan.TotalMilliseconds < (double)timeoutMillisecond)
                        {
                            continue;
                        }
                    }
                }
            //IL_98:
                return oldVal != newVal;
            }
            //goto IL_98;

            return oldVal != newVal;
        }
        
        private bool WaitForACSVariableValue(string varName, int expectedVal, int timeoutMillisecond)
        {
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = DateTime.Now - now;
            this.isStopReq = false;
            bool flag = true; ;
            while (!this.isStopReq)
            {
                Thread.Sleep(10);
                this.result = SPiiPlus660._ch.ReadVariable(varName, SPiiPlus660._ch.ACSC_NONE, -1, -1, -1, -1);
                flag = ((DateTime.Now - now).TotalMilliseconds > (double)timeoutMillisecond);
                if ((int)this.result == expectedVal || flag)
                {
                //IL_96:
                    if (flag)
                    {
                        SPiiPlus660._ch.StopBuffer((int)base.MtrTable.ACSHomingBuffer);
                    }
                    return flag;
                }
            }
            return flag;
            //flag = true;
            //goto IL_96;
        }
        
        private AxisBase.MotionStatus StartOnBoardHoming(double revstep, double strvel, double maxvel, double tacc, int waitDelay = 0)
        {
            AxisBase.MotionStatus motionStatus = AxisBase.MotionStatus.MT_DONE;
            this.set_current_pos(0.0);
            strvel *= base.MtrTable.SpeedFactor;
            maxvel *= base.MtrTable.SpeedFactor;
            if ((base.MtrTable.HomeDir == 0 && (this.get_io_sts() & 8) == 8) || (base.MtrTable.HomeDir == 1 && (this.get_io_sts() & 4) == 4))
            {
                this.start_ta_move(revstep, strvel, maxvel, tacc, tacc);
                motionStatus = (AxisBase.MotionStatus)this.wait_motion_done(waitDelay);
            }
            if (motionStatus == AxisBase.MotionStatus.MT_DONE)
            {
                if (base.MtrTable.HomeDir == 1)
                {
                    this.set_current_pos(0.0);
                    this.start_ta_move(this.MaxStep, strvel, maxvel, tacc, tacc);
                }
                else
                {
                    this.set_current_pos(this.MaxStep);
                    this.start_ta_move(0.0, strvel, maxvel, tacc, tacc);
                }
            }
            return motionStatus;
        }
        
        private AxisBase.MotionStatus StartExternalSenseHoming(double revstep, double strvel, double maxvel, double tacc, int waitDelay = 0)
        {
            AxisBase.MotionStatus motionStatus = AxisBase.MotionStatus.MT_DONE;
            if (base.MtrTable.pHomeSensing())
            {
                if (base.MtrTable.HomeDir == 1)
                {
                    this.set_current_pos(0.0);
                    this.start_ta_move(revstep, strvel, maxvel, tacc, tacc);
                    motionStatus = (AxisBase.MotionStatus)this.wait_motion_done(waitDelay);
                }
                else
                {
                    this.set_current_pos(revstep);
                    this.start_ta_move(0.0, strvel, maxvel, tacc, tacc);
                    motionStatus = (AxisBase.MotionStatus)this.wait_motion_done(waitDelay);
                }
            }
            if (motionStatus == AxisBase.MotionStatus.MT_DONE)
            {
                if (base.MtrTable.HomeDir == 1)
                {
                    this.set_current_pos(this.MaxStep);
                    this.start_ta_move(0.0, strvel, maxvel, tacc, tacc);
                }
                else
                {
                    this.set_current_pos(0.0);
                    this.start_ta_move(this.MaxStep, strvel, maxvel, tacc, tacc);
                }
                while (!base.MtrTable.pHomeSensing())
                {
                    motionStatus = (AxisBase.MotionStatus)this.wait_motion_done(waitDelay);
                }
                this.sd_stop();
            }
            return motionStatus;
        }
        
        private AxisBase.MotionStatus StartCustomHomingThread()
        {
            AxisBase.MotionStatus motionStatus = AxisBase.MotionStatus.MT_DONE;
            if (this.customHomingThrd != null && this.customHomingThrd.IsAlive)
            {
                motionStatus = AxisBase.MotionStatus.MT_HOMING_ERROR;
            }
            else
            {
                this.isCustomHomingStarted = true;
                this.customHomingThrd = new Thread(new ThreadStart(this.DoCustomHoming));
                this.customHomingThrd.IsBackground = true;
                this.customHomingThrd.Start();
            }
            return motionStatus;
        }
        
        private void DoCustomHoming()
        {
            try
            {
                base.IsBusy = true;
                this.customHomingFlag = AxisBase.MotionStatus.MT_HOMING_ERROR;
                int num = 0;
                switch (base.MtrTable.CustomHomeType)
                {
                    case CustomHomeType.HomeOnNEL:
                        num = this.home_to_NEL(true);
                        break;
                    case CustomHomeType.HomeOnPEL:
                        num = this.home_to_PEL(true);
                        break;
                    case CustomHomeType.HomeMidPoint:
                        num = this.home_mid_point(true);
                        break;
                }
                base.IsBusy = false;
                if (num != 0)
                {
                    this.customHomingFlag = AxisBase.MotionStatus.MT_HOMING_ERROR;
                }
                else
                {
                    this.customHomingFlag = AxisBase.MotionStatus.MT_DONE;
                }
            }
            catch (Exception ex)
            {
                this.customHomingFlag = AxisBase.MotionStatus.MT_HOMING_ERROR;
            }
            finally
            {
                this.isCustomHomingStarted = false;
            }
        }
        
        public int home_mid_point(bool reset = true)
        {
            int num = this.home_to_NEL(true);
            if (num == 0)
            {
                num = this.home_to_PEL(false);
                if (num == 0)
                {
                    double current_pos = this.get_current_pos();
                    for (int i = 0; i < 3; i++)
                    {
                        base.start_ta_move(current_pos / 2.0, base.MtrTable.HmStrVel, base.MtrTable.HmMaxVel);
                        num = base.wait_motion_done();
                        if (num != 0)
                        {
                            break;
                        }
                    }
                    if (num == 0)
                    {
                        if (reset)
                        {
                            base.set_current_pos_f(0.0);
                        }
                        num = 0;
                    }
                }
            }
            return num;
        }
        
        public int home_to_PEL(bool reset = true)
        {
            int num = 0;
            base.DisableMotionInterlock = true;
            this.BypassLimitCheck = true;
            this.tv_jog(base.MtrTable.HmStrVel, base.MtrTable.HmMaxVel, base.MtrTable.HmAcceleration);
            while (!this.isPosLimit)
            {
                Thread.Sleep(5);
            }
            Thread.Sleep(20);
            while (this.isPosLimit)
            {
                SPiiPlus660._ch.ExtToPoint(SPiiPlus660._ch.ACSC_AMF_RELATIVE, (int)base.MtrTable.AxisNo, -base.MtrTable.HmLimitMoveoutStep, base.MtrTable.HmMaxVel, 0.0);
                Thread.Sleep(20);
                if (num != 2 && num != 0)
                {
                    break;
                }
            }
            if (num == 0)
            {
                if (reset)
                {
                    base.set_current_pos_f(0.0);
                }
                base.start_ta_move(this.get_current_pos() - base.get_step(base.MtrTable.HmHomeOffset), base.MtrTable.HmStrVel, base.MtrTable.HmMaxVel);
                num = base.wait_motion_done();
                if (num != 0)
                {
                }
            }
            base.DisableMotionInterlock = false;
            this.BypassLimitCheck = false;
            return num;
        }
        
        public int home_to_NEL(bool reset = true)
        {
            int num = 0;
            base.DisableMotionInterlock = true;
            this.BypassLimitCheck = true;
            this.tv_jog(-base.MtrTable.HmStrVel, -base.MtrTable.HmMaxVel, base.MtrTable.HmAcceleration);
            while (!base.IsNegLimit)
            {
                Thread.Sleep(5);
            }
            Thread.Sleep(30);
            while (this.isNegLimit)
            {
                SPiiPlus660._ch.ExtToPoint(SPiiPlus660._ch.ACSC_AMF_RELATIVE, (int)base.MtrTable.AxisNo, base.MtrTable.HmLimitMoveoutStep, base.MtrTable.HmMaxVel, 0.0);
                num = base.wait_motion_done();
                Thread.Sleep(20);
                if (num != 2 && num != 0)
                {
                    break;
                }
            }
            if (num == 0)
            {
                if (reset)
                {
                    base.set_current_pos_f(0.0);
                }
                base.start_ta_move(this.get_current_pos() + base.get_step(base.MtrTable.HmHomeOffset), base.MtrTable.HmStrVel, base.MtrTable.HmMaxVel);
                num = base.wait_motion_done();
                if (num != 0)
                {
                }
            }
            base.DisableMotionInterlock = false;
            this.BypassLimitCheck = false;
            return num;
        }
        
        public override int home_move(int waitDelay = 0)
        {
            return this.home_move(base.get_step(base.MtrTable.HmRevmm), base.MtrTable.HmStrVel, base.MtrTable.HmMaxVel, base.MtrSpeed.Tacc, waitDelay);
        }
        
        public override void tv_jog(double strvel, double maxvel, double tacc)
        {
            if (!this.inhibit)
            {
                if (!base.IsProhibitToMove(100.0))
                {
                    if (base.MtrTable.Dir == 1)
                    {
                        SPiiPlus660._ch.Jog(SPiiPlus660._ch.ACSC_AMF_VELOCITY, (int)base.MtrTable.AxisNo, maxvel);
                    }
                    else
                    {
                        SPiiPlus660._ch.Jog(SPiiPlus660._ch.ACSC_AMF_VELOCITY, (int)base.MtrTable.AxisNo, -maxvel);
                    }
                }
            }
        }
        
        public override int jog_till_neg_limit(float velocity, int timeout = 60000)
        {
            bool flag = false;
            bool flag2 = false;
            this.tv_jog((double)(-(double)velocity), base.MtrSpeed.Tacc, base.MtrSpeed.Tdec);
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = DateTime.Now - now;
            while (!this.get_nel_signal() || !flag2)
            {
                Thread.Sleep(1);
                flag2 = ((DateTime.Now - now).TotalMilliseconds >= (double)timeout);
            }
            if (!flag2)
            {
                this.sd_stop();
                flag = true;
            }
            return flag ? 0 : 6;
        }
        
        public override int jog_till_pos_limit(float velocity, int timeout = 60000)
        {
            bool flag = false;
            bool flag2 = false;
            this.tv_jog((double)velocity, base.MtrSpeed.Tacc, base.MtrSpeed.Tdec);
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = DateTime.Now - now;
            while (!this.get_pel_signal() || !flag2)
            {
                Thread.Sleep(1);
                flag2 = ((DateTime.Now - now).TotalMilliseconds >= (double)timeout);
            }
            if (!flag2)
            {
                this.sd_stop();
                flag = true;
            }
            return flag ? 0 : 6;
        }
        
        public override void tv_jog()
        {
            if (!this.inhibit)
            {
                if (!base.IsProhibitToMove(1.0))
                {
                    if (base.MtrTable.Dir == 1)
                    {
                        SPiiPlus660._ch.Jog(SPiiPlus660._ch.ACSC_AMF_VELOCITY, (int)base.MtrTable.AxisNo, base.MtrSpeed.MaxVel);
                    }
                    else
                    {
                        SPiiPlus660._ch.Jog(SPiiPlus660._ch.ACSC_AMF_VELOCITY, (int)base.MtrTable.AxisNo, -base.MtrSpeed.MaxVel);
                    }
                }
            }
        }
        
        public override void sv_jog(double strvel, double maxvel, double tacc, double vsacc)
        {
            if (!this.inhibit)
            {
                if (!base.IsProhibitToMove(1.0))
                {
                    if (base.MtrTable.Dir == 1)
                    {
                        SPiiPlus660._ch.Jog(SPiiPlus660._ch.ACSC_AMF_VELOCITY, (int)base.MtrTable.AxisNo, maxvel);
                    }
                    else
                    {
                        SPiiPlus660._ch.Jog(SPiiPlus660._ch.ACSC_AMF_VELOCITY, (int)base.MtrTable.AxisNo, -maxvel);
                    }
                }
            }
        }
        
        public override void sv_jog()
        {
            if (!this.inhibit)
            {
                if (!base.IsProhibitToMove(1.0))
                {
                    if (base.MtrTable.Dir == 1)
                    {
                        SPiiPlus660._ch.Jog(SPiiPlus660._ch.ACSC_AMF_VELOCITY, (int)base.MtrTable.AxisNo, base.MtrSpeed.MaxVel);
                    }
                    else
                    {
                        SPiiPlus660._ch.Jog(SPiiPlus660._ch.ACSC_AMF_VELOCITY, (int)base.MtrTable.AxisNo, -base.MtrSpeed.MaxVel);
                    }
                }
            }
        }
        
        public override void emg_stop()
        {
            this.isStopReq = true;
            if (!this.isSimulation)
            {
                if (base.MtrTable.UseACSHomingBuffer && base.MtrTable.ACSHomingBuffer != -1)
                {
                    SPiiPlus660._ch.StopBuffer((int)base.MtrTable.ACSHomingBuffer);
                }
                SPiiPlus660._ch.Kill((int)base.MtrTable.AxisNo);
            }
        }
        
        public override void sd_stop(double tdec)
        {
            this.isStopReq = true;
            if (!this.isSimulation)
            {
                if (base.MtrTable.UseACSHomingBuffer && base.MtrTable.ACSHomingBuffer != -1)
                {
                    SPiiPlus660._ch.StopBuffer((int)base.MtrTable.ACSHomingBuffer);
                }
                SPiiPlus660._ch.Halt((int)base.MtrTable.AxisNo);
            }
        }
        
        public override void sd_stop()
        {
            if (!this.isSimulation)
            {
                if (base.MtrTable.UseACSHomingBuffer && base.MtrTable.ACSHomingBuffer != -1)
                {
                    SPiiPlus660._ch.StopBuffer((int)base.MtrTable.ACSHomingBuffer);
                }
                SPiiPlus660._ch.Halt((int)base.MtrTable.AxisNo);
                Thread.Sleep(10);
                this.isStopReq = true;
                if (base.MtrTable.SecondAxisNumber >= 0)
                {
                    SPiiPlus660._ch.Halt(base.MtrTable.SecondAxisNumber);
                }
            }
        }
        
        public override void start_ta_move(double pos, double strvel, double maxvel, double tacc, double tdec)
        {
            this.start_ta_move(pos, strvel, maxvel, tacc, tdec, base.MtrSpeed.AcsJerk);
        }
        
        protected override bool WriteDistanceToBuffer(double distance, ref string serr)
        {
            bool flag;
            if (base.MtrTable.ACSDistacneVarName == "")
            {
                flag = false;
            }
            else if (this.isSimulation)
            {
                flag = true;
            }
            else
            {
                try
                {
                    int millisecondsTimeout = (base.MtrTable.ACSDisVarNameWriteDelay > 0) ? base.MtrTable.ACSDisVarNameWriteDelay : 1;
                    SPiiPlus660._ch.WriteVariable((float)distance, base.MtrTable.ACSDistacneVarName, SPiiPlus660._ch.ACSC_NONE, SPiiPlus660._ch.ACSC_NONE, SPiiPlus660._ch.ACSC_NONE, SPiiPlus660._ch.ACSC_NONE, SPiiPlus660._ch.ACSC_NONE);
                    Thread.Sleep(millisecondsTimeout);
                }
                catch (Exception ex)
                {
                    serr = ex.Message;
                    return false;
                }
                flag = true;
            }
            return flag;
        }
        
        public override void start_ta_move(double pos, double strvel, double maxvel, double tacc, double tdec, double jerk)
        {
            base.MtrTable.NewPos = pos;
            this.currentPulse = this.get_current_pos();
            double num = pos - this.currentPulse;
            base.TargetPos = base.get_mm(pos);
            if (this.inhibit)
            {
                base.ResetTargetPos();
            }
            else if (base.IsProhibitToMove(base.get_mm(num)))
            {
                base.ResetTargetPos();
            }
            else if (this.IsExceedMaxStep(pos))
            {
                base.ResetTargetPos();
            }
            else if (base.HasHome)
            {
                strvel *= base.MtrTable.SpeedFactor;
                maxvel *= base.MtrTable.SpeedFactor;
                this.ApplyMotionProfile(maxvel, tacc, tdec, jerk);
                base.MtrTable.DistanceToMove = num;
                base.MtrTable.EstimateTimeTaken = 0.01 * (Math.Abs(base.MtrTable.DistanceToMove) / maxvel);
                this.isStopReq = false;
                this.commmandStartTime = DateTime.Now;
                if (!this.isSimulation)
                {
                    double num2 = (double)((base.MtrTable.Dir == 1) ? -1 : 1);
                    SPiiPlus660._ch.ExtToPoint(SPiiPlus660._ch.ACSC_AMF_VELOCITY, (int)base.MtrTable.AxisNo, num2 * pos, maxvel, 0.0);
                }
            }
        }
        
        public override void start_sa_move(double pos, double strvel, double maxvel, double tacc, double tdec, double vsacc, double vsdec)
        {
            this.start_sa_move(pos, strvel, maxvel, tacc, tdec, vsacc, vsdec);
        }
        
        public override void start_sa_move(double pos, double strvel, double maxvel, double tacc, double tdec, double vsacc, double vsdec, double jerk)
        {
            base.MtrTable.NewPos = pos;
            this.currentPulse = this.get_current_pos();
            double step = pos - this.currentPulse;
            base.TargetPos = base.get_mm(pos);
            if (this.inhibit)
            {
                base.ResetTargetPos();
            }
            else if (base.IsProhibitToMove(base.get_mm(step)))
            {
                base.ResetTargetPos();
            }
            else if (this.IsExceedMaxStep(pos))
            {
                base.ResetTargetPos();
            }
            else if (base.HasHome)
            {
                strvel *= base.MtrTable.SpeedFactor;
                maxvel *= base.MtrTable.SpeedFactor;
                this.ApplyMotionProfile(maxvel, tacc, tdec, jerk);
                base.MtrTable.DistanceToMove = pos - this.currentPhysicalPos;
                base.MtrTable.EstimateTimeTaken = 0.0099999997764825821 * (Math.Abs(base.MtrTable.DistanceToMove) / maxvel);
                this.isStopReq = false;
                this.commmandStartTime = DateTime.Now;
                if (!this.isSimulation)
                {
                    double num = (double)((base.MtrTable.Dir == 1) ? -1 : 1);
                    SPiiPlus660._ch.ExtToPoint(SPiiPlus660._ch.ACSC_AMF_VELOCITY, (int)base.MtrTable.AxisNo, num * pos, maxvel, 0.0);
                }
            }
        }
        
        private void ApplyMotionProfile(double vel, double tacc, double tdec, double jerk)
        {
            this.ApplyMotionProfile(vel, tacc, tdec, base.MtrSpeed.AcsKillDec, jerk);
        }
        
        private void ApplyMotionProfile(double vel, double tacc, double tdec, double killDec, double jerk)
        {
            if (!this.isSimulation)
            {
                SPiiPlus660._ch.SetVelocity((int)base.MtrTable.AxisNo, vel);
                SPiiPlus660._ch.SetAcceleration((int)base.MtrTable.AxisNo, tacc);
                SPiiPlus660._ch.SetDeceleration((int)base.MtrTable.AxisNo, tdec);
                SPiiPlus660._ch.SetKillDeceleration((int)base.MtrTable.AxisNo, killDec);
                SPiiPlus660._ch.SetJerk((int)base.MtrTable.AxisNo, jerk);
            }
        }
        
        public override double get_current_pos()
        {
            if (this.NumberOfAxisPerCard > 0 && !base.IsSimulation)
            {
                lock (this.syncLock)
                {
                    this.tempReadPos = SPiiPlus660._ch.GetFPosition((int)base.MtrTable.AxisNo);
                }
                base.MtrTable.CurPos = (base.MtrTable.FeedbackReadingReverse ? (-this.tempReadPos) : this.tempReadPos);
                base.CurrentPhysicalPos = base.get_mm(this.tempReadPos);
            }
            double num;
            if (base.MtrTable.Dir == 1)
            {
                num = base.MtrTable.CurPos - base.MtrTable.Offset;
            }
            else
            {
                num = base.MtrTable.CurPos + base.MtrTable.Offset;
            }
            return num;
        }
        
        public override void set_current_pos(double pos)
        {
            pos = Math.Round(pos, 0, MidpointRounding.AwayFromZero);
            if (base.MtrTable.Dir == 1)
            {
                base.MtrTable.NewPos = pos + base.MtrTable.Offset;
            }
            else
            {
                base.MtrTable.NewPos = -pos - base.MtrTable.Offset;
            }
            if (!this.isSimulation)
            {
                SPiiPlus660._ch.SetFPosition((int)base.MtrTable.AxisNo, pos);
            }
        }
        
        public override void brake_free(bool set)
        {
        }
        
        public override bool get_origin_signal()
        {
            return false;
        }
        
        public override bool get_pel_signal()
        {
            bool flag;
            if (this.isSimulation)
            {
                flag = false;
            }
            else
            {
                int fault = SPiiPlus660._ch.GetFault((int)base.MtrTable.AxisNo);
                bool flag2 = (fault & 1) == 1;
                flag = flag2;
            }
            return flag;
        }
        
        public override bool get_nel_signal()
        {
            bool flag;
            if (this.isSimulation)
            {
                flag = false;
            }
            else
            {
                int fault = SPiiPlus660._ch.GetFault((int)base.MtrTable.AxisNo);
                bool flag2 = (fault & 2) == 2;
                flag = flag2;
            }
            return flag;
        }
        
        public override bool get_inpos_signal()
        {
            bool flag;
            if (this.isSimulation)
            {
                flag = true;
            }
            else
            {
                bool flag2 = false;
                if (this.NumberOfAxisPerCard > 0 && !base.IsSimulation)
                {
                    flag2 = Convert.ToBoolean(SPiiPlus660._ch.GetMotorState((int)base.MtrTable.AxisNo) & SPiiPlus660._ch.ACSC_MST_INPOS);
                }
                flag = flag2;
            }
            return flag;
        }
        
        public override bool get_alarm_signal()
        {
            return false;
        }
        
        public static void AssignChannel(Channel ch)
        {
            SPiiPlus660._ch = ch;
            SPiiPlus660.openedEtherCom = true;
        }
        
        public override bool initialization(int card, bool autoAddress)
        {
            bool flag;
            if (this.initDel == null)
            {
                if (SPiiPlus660._ch == null)
                {
                    SPiiPlus660._ch = (Channel)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("73C1FDD7-7A9F-45CB-AC12-73AC50F9A3C8")));
                }
                switch (this._comMode)
                {
                    case ComMode.COM_PCI_BUS:
                        if (SPiiPlus660._obtainedCards <= 0)
                        {
                            if (SPiiPlus660.acsc_GetPCICards(SPiiPlus660._pciCards, SPiiPlus660._pciCards.Length, out SPiiPlus660._obtainedCards) == 1)
                            {
                                for (int i = 0; i < SPiiPlus660._obtainedCards; i++)
                                {
                                    SPiiPlus660._ch.OpenCommPCI(SPiiPlus660._pciCards[i].SlotNumber);
                                }
                            }
                        }
                        break;
                    case ComMode.COM_ETHERNET:
                        if (!SPiiPlus660.openedEtherCom)
                        {
                            SPiiPlus660._ch.OpenCommEthernet(this._ipAddress, this._protocol);
                        }
                        SPiiPlus660.openedEtherCom = true;
                        break;
                    case ComMode.COM_SERIAL:
                        SPiiPlus660._ch.OpenCommSerial(this._comPort, this._baudRate);
                        break;
                    case ComMode.COM_DIRECT:
                        SPiiPlus660._ch.OpenCommDirect();
                        break;
                }
                int num = 0;
                if (base.MtrConfig.limit_mode == 1)
                {
                    num = (SPiiPlus660._ch.ACSC_SAFETY_RL | SPiiPlus660._ch.ACSC_SAFETY_LL);
                }
                num |= SPiiPlus660._ch.ACSC_SAFETY_HOT;
                num |= SPiiPlus660._ch.ACSC_SAFETY_DRIVE;
                num |= (SPiiPlus660._ch.ACSC_SAFETY_PE | SPiiPlus660._ch.ACSC_SAFETY_CPE);
                num |= SPiiPlus660._ch.ACSC_SAFETY_CL;
                num |= SPiiPlus660._ch.ACSC_SAFETY_SP;
                num |= SPiiPlus660._ch.ACSC_SAFETY_PROG;
                num |= SPiiPlus660._ch.ACSC_SAFETY_MEM;
                num |= SPiiPlus660._ch.ACSC_SAFETY_TIME;
                num |= SPiiPlus660._ch.ACSC_SAFETY_ES;
                num |= SPiiPlus660._ch.ACSC_SAFETY_INT;
                num |= SPiiPlus660._ch.ACSC_SAFETY_INTGR;
                if (base.MtrTable.MtrType == 0)
                {
                    num |= (SPiiPlus660._ch.ACSC_SAFETY_ENC | SPiiPlus660._ch.ACSC_SAFETY_ENC2 | SPiiPlus660._ch.ACSC_SAFETY_ENCNC | SPiiPlus660._ch.ACSC_SAFETY_ENC2NC);
                }
                flag = true;
            }
            else
            {
                flag = this.initDel(card);
            }
            return flag;
        }
        
        public override bool uninitialization(int card)
        {
            if (SPiiPlus660._ch != null)
            {
                SPiiPlus660._ch.CloseComm();
            }
            return true;
        }
        
        public override void start_tr_move(double dist, double strvel, double maxvel, double tacc, double tdec)
        {
        }
        
        public override void start_sr_move(double dist, double strvel, double maxvel, double tacc, double tdec, double vsacc, double vsdec)
        {
            throw new NotImplementedException();
        }
        
        public override void start_ta_move_xy(ref AxisBase pxmtr, ref AxisBase pymtr, double strvel, double maxvel, double tacc, double tdec)
        {
            throw new NotImplementedException();
        }
        
        public override void start_ta_move_zu(ref AxisBase pxmtr, ref AxisBase pymtr, double strvel, double maxvel, double tacc, double tdec)
        {
            throw new NotImplementedException();
        }
        
        public override void start_ta_move_xy_line_to_pos(AxisBase secondAxis, double posX, double posY, double strvel, double maxvel, double tacc, double tdec)
        {
            base.MtrTable.NewPos = posX;
            secondAxis.MtrTable.NewPos = posY;
            double current_pos = this.get_current_pos();
            double current_pos2 = secondAxis.get_current_pos();
            double num = posX - current_pos;
            double num2 = posY - current_pos2;
            double targetPos = base.get_mm(posX);
            double targetPos2 = secondAxis.get_mm(posY);
            base.TargetPos = targetPos;
            secondAxis.TargetPos = targetPos2;
            if (base.IsProhibitToMove(base.get_mm(num)))
            {
                base.ResetTargetPos();
                secondAxis.ResetTargetPos();
            }
            else if (secondAxis.IsProhibitToMove(base.get_mm(num2)))
            {
                base.ResetTargetPos();
                secondAxis.ResetTargetPos();
            }
            else if (this.IsExceedMaxStep(posX))
            {
                base.ResetTargetPos();
                secondAxis.ResetTargetPos();
            }
            else if (secondAxis.IsExceedMaxStep(posY))
            {
                base.ResetTargetPos();
                secondAxis.ResetTargetPos();
            }
            else if (base.HasHome)
            {
                if (secondAxis.HasHome)
                {
                    double num3 = Math.Max(Math.Abs(num), Math.Abs(num2));
                    base.MtrTable.DistanceToMove = num;
                    secondAxis.MtrTable.DistanceToMove = num2;
                    base.MtrTable.EstimateTimeTaken = 0.01 * num3 / maxvel;
                    this.isStopReq = false;
                    this.commmandStartTime = DateTime.Now;
                    if (!this.isSimulation)
                    {
                        int[] axes = new int[]
                        {
                            (int)base.MtrTable.AxisNo,
                            (int)secondAxis.MtrTable.AxisNo,
                            -1
                        };
                        double[] array = new double[]
                        {
                            current_pos,
                            current_pos2
                        };
                        SPiiPlus660._ch.SegmentedMotion(SPiiPlus660._ch.ACSC_AMF_VELOCITY, axes, array);
                        array[0] = posX;
                        array[1] = posY;
                        SPiiPlus660._ch.SegmentLine(0, axes, array, strvel, maxvel, null, null, SPiiPlus660._ch.ACSC_NONE, null);
                        SPiiPlus660._ch.EndSequenceM(axes);
                    }
                }
            }
        }
        
        public override void start_sa_move_xy(ref AxisBase pxmtr, ref AxisBase pymtr, double strvel, double maxvel, double tacc, double tdec, double vsacc, double vsdec)
        {
            throw new NotImplementedException();
        }
        
        public override void start_sa_move_zu(ref AxisBase pxmtr, ref AxisBase pymtr, double strvel, double maxvel, double tacc, double tdec, double vsacc, double vsdec)
        {
            throw new NotImplementedException();
        }
        
        public override void start_ta_line2(ref AxisBase pxmtr, ref AxisBase pymtr, double strvel, double maxvel, double tacc, double tdec)
        {
            throw new NotImplementedException();
        }
        
        public override void start_sa_line2(ref AxisBase pxmtr, ref AxisBase pymtr, double strvel, double maxvel, double tacc, double tdec, double vsacc, double vsdec)
        {
            throw new NotImplementedException();
        }
        
        public override void start_ta_line3(ref AxisBase pxmtr, ref AxisBase pymtr, ref AxisBase pzmtr, double strvel, double maxvel, double tacc, double tdec)
        {
            throw new NotImplementedException();
        }
        
        public override void start_sa_line3(ref AxisBase pxmtr, ref AxisBase pymtr, ref AxisBase pzmtr, double strvel, double maxvel, double tacc, double tdec, double vsacc, double vsdec)
        {
            throw new NotImplementedException();
        }
        
        public override void start_ta_line4(ref AxisBase pxmtr, ref AxisBase pymtr, ref AxisBase pzmtr, ref AxisBase pumtr, double strvel, double maxvel, double tacc, double tdec)
        {
            throw new NotImplementedException();
        }
        
        public override void start_sa_line4(ref AxisBase pxmtr, ref AxisBase pymtr, ref AxisBase pzmtr, ref AxisBase pumtr, double strvel, double maxvel, double tacc, double tdec, double vsacc, double vsdec)
        {
            throw new NotImplementedException();
        }
        
        public override void start_a_arc_xy(ref AxisBase pxmtr, ref AxisBase pymtr, short Dir)
        {
            throw new NotImplementedException();
        }
        
        public override void start_a_arc_zu(ref AxisBase pxmtr, ref AxisBase pymtr, short Dir)
        {
            throw new NotImplementedException();
        }
        
        public override void start_a_arc2(ref AxisBase pxmtr, ref AxisBase pymtr, short Dir)
        {
            throw new NotImplementedException();
        }
        
        protected object _arrBusNumber;
        protected object _arrSlotNumber;
        protected object _arrFunction;
        protected static Channel _ch;
        protected ComMode _comMode;
        protected int _comPort = -1;
        protected int _baudRate;
        protected int _homingCompletedValue = 1;
        protected int _homingStartedValue = 0;
        protected string _ipAddress;
        protected int _protocol;
        private static SPiiPlus660.ACSC_PCI_SLOT[] _pciCards = new SPiiPlus660.ACSC_PCI_SLOT[16];
        private static int _obtainedCards = 0;
        private object result = new object();
        private object syncLock = new object();
        private double tempReadPos = 0.0;
        private static bool openedEtherCom = false;
        public enum FAULTSTATUS
        {
            PEL = 1,
            NEL
        }
        
        private struct ACSC_PCI_SLOT
        {
            public int BusNumber;
            public int SlotNumber;  
            public int Function;
        }
    }
}
