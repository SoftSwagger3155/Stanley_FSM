using Stanley_MCPNet.Data;
using Stanley_MCPNet.General;
using Stanley_MCPNet.Motion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Stanley_MCPNet.Motion.Mcc800
{
    public class Mcc800P : AxisBase
    {

        public Mcc800P(ref MtrConfig mtrConfig, ref MtrTable mtrTable, ref MtrSpeed mtrSpeed, bool simulation) : base(ref mtrConfig, ref mtrTable, ref mtrSpeed, simulation)
        {
            this.NumberOfAxisPerCard = 15;
        }

        public Mcc800P(ref MtrConfig mtrConfig, ref MtrTable mtrTable, ref MtrSpeed mtrSpeed, ref MtrMisc mtrMisc, bool simulation) : base(ref mtrConfig, ref mtrTable, ref mtrSpeed, ref mtrMisc, simulation)
        {
            this.NumberOfAxisPerCard = 15;
        }

        //...................................................................................................................................................................................................................................................................................//
        public override bool get_origin_signal()
        {
            if (isSimulation)
                return currentPulse == 0;

            return (((MccIOSTATUS)get_io_sts() & MccIOSTATUS.ORG) == MccIOSTATUS.ORG);
        }

        //...................................................................................................................................................................................................................................................................................//
        public override bool get_nel_signal()
        {
            if (isSimulation)
            {
                //return currentPulse == 0;
                return false;
            }


            return (((MccIOSTATUS)get_io_sts() & MccIOSTATUS.MEL) == MccIOSTATUS.MEL);
        }

        //...................................................................................................................................................................................................................................................................................//
        public override bool get_pel_signal()
        {
            if (isSimulation)
            {
                //return currentPulse == 0;
                return false;
            }


            return (((MccIOSTATUS)get_io_sts() & MccIOSTATUS.PEL) == MccIOSTATUS.PEL);
        }

        //...................................................................................................................................................................................................................................................................................//
        public override bool get_inpos_signal()
        {
            if (isSimulation)
                return currentPulse == 0;

            return (((MccIOSTATUS)get_io_sts() & MccIOSTATUS.INP) == MccIOSTATUS.INP);
        }

        //...................................................................................................................................................................................................................................................................................//
        public override bool get_alarm_signal()
        {
            if (isSimulation)
            {
                var aa = (MccIOSTATUS)get_io_sts();
                return false;
            }


            return (((MccIOSTATUS)get_io_sts() & MccIOSTATUS.ALM) == MccIOSTATUS.ALM);
        }

        //...................................................................................................................................................................................................................................................................................//
        public override void Dispose()
        {
            uninitialization(0);
        }

        //...................................................................................................................................................................................................................................................................................//
        public override bool IsRunning()
        {
            if (isSimulation) return false;
            //返回值:0:指定轴正在运行,1:指定轴已停止； 
            int status = (int)DllMCC800P.YK_check_done((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo);
            return status == 0;
        }

        //...................................................................................................................................................................................................................................................................................//
        public override bool IsHome()
        {
            return MtrTable.HomeFlag == 1 ? true : false;
        }

        //...................................................................................................................................................................................................................................................................................//
        public override bool IsExceedMaxStep(double step)
        {
            if (step > MaxStep) return true;
            if (step < MinStep) return true;
            return false;
        }

        //...................................................................................................................................................................................................................................................................................//
        public override void set_servo(bool set)
        {
            do
            {

                if (IsSimulation)
                {
                    IsServoOn = set;
                    break;
                }
                DllMCC800P.YK_write_sevon_pin((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, set ? (ushort)MtrTable.ServoOnLogic : (ushort)(MtrTable.ServoOnLogic ^ 1));
                IsServoOn = Convert.ToBoolean(set ? MtrTable.ServoOnLogic : (MtrTable.ServoOnLogic ^ 1));

            } while (false);
        }

        //...................................................................................................................................................................................................................................................................................//
        public override void set_newpos(double newPos)
        {
            MtrTable.NewPos = newPos;
        }

        //...................................................................................................................................................................................................................................................................................//
        public override int wait_home_done(int waitDelay)
        {
            try
            {
                DisableMotionInterlock = true;
                BypassLimitCheck = true;

                int flag = (int)MotionStatus.MT_DONE;
                if (isCustomHomingStarted)
                {
                    DateTime st = DateTime.Now;
                    TimeSpan ts = DateTime.Now - st;
                    while (isCustomHomingStarted && ts.TotalMilliseconds < waitDelay)
                    {
                        ts = DateTime.Now - st;
                        Thread.Sleep(10);
                    }
                    flag = (int)customHomingFlag;
                }
                else
                {
                    flag = wait_motion_done(waitDelay);
                }

                if (flag == (int)MotionStatus.MT_DONE)
                {

                    HasHome = true;
                    MtrTable.HomeFlag = 1;
                    MtrTable.ErrorFlag = 0;
                    TargetPos = 0;
                }
                return flag;
            }
            finally
            {
                DisableMotionInterlock = false;
                BypassLimitCheck = false;
            }
        }

        //...................................................................................................................................................................................................................................................................................//
        protected override int wait_motion_complete()
        {
            //返回值:0:指定轴正在运行,1:指定轴已停止； 
            int status = (int)DllMCC800P.YK_check_done((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo);
            return status;
        }


        //...................................................................................................................................................................................................................................................................................//
        public override int wait_motion_done(int waitDelay)
        {
            MotionStatus flag = MotionStatus.MT_DONE;
            DateTime st = DateTime.Now;
            TimeSpan timespan = DateTime.Now - st;
            int motionSts = 0;
            int stopReason =0;

            IsBusy = true;
            bool Break = false;
            do
            {
                //Simulation Mode
                if (isSimulation)
                {
                    flag = MotionStatus.MT_DONE;
                    TimeSpan ts = MtrTable.EstimateTimeTaken < 0.5 ?
                                            TimeSpan.FromSeconds(0.5) :
                                            TimeSpan.FromSeconds(MtrTable.EstimateTimeTaken);


                    TimeSpan ts2 = DateTime.Now - commmandStartTime;
                    double temppos = MtrTable.CurPos;
                    double tempfliction = 1;
                    while (ts2.TotalMilliseconds < ts.TotalMilliseconds)
                    {
                        ts2 = DateTime.Now - commmandStartTime;
                        tempfliction = ts2.TotalMilliseconds / ts.TotalMilliseconds;
                        if (tempfliction > 1)
                            tempfliction = 1;

                        MtrTable.CurPos = temppos + tempfliction * MtrTable.DistanceToMove;
                        Thread.Sleep(5);
                        if (isStopReq) break;
                    }
                    if (!isStopReq)
                        MtrTable.CurPos = MtrTable.NewPos;

                    isStopReq = false;
                    break;
                }

                //Real Move Check Stop Validation Flag
                while(true)
                {
                    timespan = DateTime.Now - st;
                    motionSts = DllMCC800P.YK_check_done((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo); //返回值:0:指定轴正在运行,1:指定轴已停止； 
                    DllMCC800P.YK_get_stop_reason((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, ref stopReason);

                    if(MtrTable.MtrType == 0)
                    {
                        flag = motionSts == 1 && stopReason == 0 ? MotionStatus.MT_DONE : MotionStatus.MT_MOVING;
                        if (flag == MotionStatus.MT_DONE) break;
                    }
                    else
                    {
                        bool isRDY = ((MccIOSTATUS)get_io_sts() & MccIOSTATUS.RDY) != MccIOSTATUS.RDY;
                        flag = motionSts == 1 && stopReason == 0 ? MotionStatus.MT_DONE : MotionStatus.MT_MOVING;

                        if (flag == MotionStatus.MT_DONE && isRDY) break;
                    }

                    if (!BypassAlarmCheck)
                    {
                        //ALM Trigger
                        if (((MccIOSTATUS)get_io_sts() & MccIOSTATUS.ALM) == MccIOSTATUS.ALM)                            
                        {
                            //clear home flag
                            MtrTable.HomeFlag = 0;              
                            flag = MotionStatus.MT_ALARM;
                            break;
                        }
                    }

                    if (!BypassLimitCheck)
                    {
                        //Limit Trigger
                        if (MtrTable.OnBoardLmt == 1 &&
                            (((MccIOSTATUS)get_io_sts() & MccIOSTATUS.PEL) == MccIOSTATUS.PEL ||
                            ((MccIOSTATUS)get_io_sts() & MccIOSTATUS.MEL) == MccIOSTATUS.MEL))   
                        {
                            flag = MotionStatus.MT_LIMIT;
                            break;
                        }
                    }

                   
                    if (waitDelay > 0)                     
                    {
                        //If Delay Defined
                        if (timespan.TotalMilliseconds > waitDelay)
                        {
                            //Set Timeout Flag
                            flag = MotionStatus.MT_TIMEOUT;             
                            Break = true;
                        }
                    }

                    if (MtrTable.pCriticalEvent != null)         
                    {
                        //Critical Event Activated
                        if (MtrTable.pCriticalEvent())          
                        {
                            flag = MotionStatus.MT_CRITICAL;
                            
                            //Clear home flag
                            MtrTable.HomeFlag = 0;             
                            emg_stop();
                            break;
                        }
                    }

                    if (MtrTable.pHangedEvent != null)
                    {    
                        //Hanged event Activated,Loop till release
                        while (MtrTable.pHangedEvent())     
                        {
                            flag = MotionStatus.MT_HANGED; 
                            Thread.Sleep(10);
                        }
                    }

                    if (MtrTable.pFwdSensing != null)   
                    {
                        //Forward Event Activated
                        if (MtrTable.pFwdSensing())         
                        {
                            //sd_stop();
                            flag = MotionStatus.MT_LIMIT;
                            break;
                        }
                    }

                    if (MtrTable.pRevSensing != null)        
                    {
                        //Reversed Event Activated
                        if (MtrTable.pRevSensing())     
                        {
                            //sd_stop();
                            flag = MotionStatus.MT_LIMIT;
                            break;
                        }
                    }

                    Thread.Sleep(20);
                }

                get_current_pos();

            }
            while (false);

            IsBusy = false;
            switch (flag)
            {
                case MotionStatus.MT_LIMIT:
                    throw new MCPNetException(this.MtrTable.Name + "- Limit sensor activated!", ErrorCode.ErrCode.Motion_Limit);

                case MotionStatus.MT_CRITICAL:
                    throw new MCPNetException(this.MtrTable.Name + "- Critical event occured!", ErrorCode.ErrCode.EStop);

                case MotionStatus.MT_ALARM:
                    throw new MCPNetException(this.MtrTable.Name + "- Alarm signal activated!", ErrorCode.ErrCode.Motion_Alarm);

                case MotionStatus.MT_HOMING_ERROR:
                    throw new MCPNetException(this.MtrTable.Name + "- Homing Error!", ErrorCode.ErrCode.HomingError);
            }
            return (int)flag;
        }

        public override void set_zero_pos()
        {
            set_current_pos(0.0);
        }

        //Set Physical Pos
        public override void set_current_pos(double pos)
        {
            pos = Math.Round(pos, 0, MidpointRounding.AwayFromZero);

            if (MtrTable.Dir == 1)
                MtrTable.NewPos = pos + MtrTable.Offset;
            else
                MtrTable.NewPos = -pos - MtrTable.Offset;


            if (MtrTable.EncoderAxisNo >= 0)
                DllMCC800P.YK_set_position((ushort)MtrTable.CardNo, (ushort)MtrTable.EncoderAxisNo, (int)(MtrTable.NewPos * mmPerStep));

            DllMCC800P.YK_set_position((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, (int)(MtrTable.NewPos * mmPerStep));
        }

        public override void clear_home_flag()
        {
            MtrTable.HomeFlag = 0;
        }

        public override void abnormal_stop()
        {
            //enable：ALM使能 0:禁止 1:允許
            //alm_logic: ALM信號有效電平 0:低有效 1:高有效
            //alm_action: ALM信號制動方式 0:立即停止(只支持這方式) 
            DllMCC800P.YK_set_alm_mode((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, 0, (ushort)MtrConfig.alm_logic, 0);
            Thread.Sleep(50);
            DllMCC800P.YK_set_alm_mode((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, 1, (ushort)MtrConfig.alm_logic, 0);
        }

        public override int get_motion_sts()
        {
            if (NumberOfAxisPerCard > 0 && !IsSimulation)
                return (DllMCC800P.YK_check_done((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo));
            else return 0;
        }

        public override int get_io_sts()
        {
            int sts = 0;
            if (NumberOfAxisPerCard > 0 && !IsSimulation)
                sts = (int)DllMCC800P.YK_axis_io_status((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo);// //
            return sts;
        }

        public override void emg_stop()
        {
            isStopReq = true;

            if (!isSimulation)
            {
                DllMCC800P.YK_emg_stop((ushort)MtrTable.CardNo);
            }

            MtrTable.ErrorFlag = 1;
            MtrTable.HomeFlag = 0;
            HasHome = false;
        }

        public override double get_current_pos()
        {
            int axis = MtrTable.GetFeedBackReadAxis;

            if (NumberOfAxisPerCard > 0 && !IsSimulation)
            {
                int curpos = DllMCC800P.YK_get_position((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo);
                MtrTable.CurPos = curpos;
                CurrentPhysicalPos = curpos / StepPermm;
                //set_current_pos(curpos);
                //set_current_pos_f(CurrentPhysicalPos);
            }
            //Dll8154._8154_get_position((short)axis, out MtrTable.CurPos);

            if (MtrTable.Dir == 1)
                return (MtrTable.CurPos - MtrTable.Offset);
            else
                return (-MtrTable.CurPos + MtrTable.Offset);
        }

        public override int home_move(double revstep, double strvel, double maxvel, double tacc, int WaitDelay = 0)
        {

            if (inhibit) return (int)MotionStatus.MT_DONE;

            if (IsProhibitToHome())
                return (int)MotionStatus.MT_INHIBIT_TO_HOME;

            //if (IsProhibitToMove())
            //	return (int)MotionStatus.MT_INHIBIT_TO_MOVE;
            HasHome = false;
            isStopReq = false;
            set_servo(false);
            Thread.Sleep(100);
            set_servo(true);

            MotionStatus flag = MotionStatus.MT_DONE;
            MtrTable.HomeFlag = 0;



            isStopReq = false;

            if (isSimulation)
            {
                MtrTable.NewPos = 0;
                MtrTable.DistanceToMove = 0 - currentPulse;
                MtrTable.EstimateTimeTaken = 0.01 * (Math.Abs(MtrTable.DistanceToMove) / maxvel);
                return (int)MotionStatus.MT_DONE;
            }


            if (MtrTable.Encoder == 1)
            {
                int servonSet = DllMCC800P.YK_write_sevon_pin((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, (ushort)MtrTable.ServoOnLogic);
                int seronRead = DllMCC800P.YK_read_sevon_pin((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo);
            }

            //Use OnBoard Home
            if (MtrTable.OnBoardHome == 1)					
            {
                set_current_pos(0.0);
                strvel *= MtrTable.SpeedFactor;
                maxvel *= MtrTable.SpeedFactor;

                //If Already At Home Position
                if (((MccIOSTATUS)get_io_sts() & MccIOSTATUS.ORG) != MccIOSTATUS.ORG)			
                {
                    //Move Reversed To Clear Home Sensor
                    start_ta_move(revstep, strvel, maxvel, tacc, tacc);	
                    flag = (MotionStatus)wait_motion_done(WaitDelay);
                }

                //No Motion Error
                if (flag == MotionStatus.MT_DONE)					
                {
                    IsBusy = true;
                    DllMCC800P.YK_set_homemode((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, (ushort)MtrTable.HomeDir, (ushort)MtrTable.HmMaxVel, (ushort)MtrConfig.home_mode, 0);
                    DllMCC800P.YK_set_profile((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, MtrTable.HmStrVel, MtrTable.HmMaxVel, MtrTable.HmAcceleration, MtrTable.HmDeceleration, 1000);
                    DllMCC800P.YK_home_move((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo);
                }
                else return (int)flag;			
            }
            else
            {
                flag = StartCustomHomingThread();
            }

            return (int)flag;
        }

        MotionStatus StartCustomHomingThread()
        {
            MotionStatus flag = MotionStatus.MT_DONE;
            if (customHomingThrd != null && customHomingThrd.IsAlive)
            {
                flag = MotionStatus.MT_HOMING_ERROR;
            }
            else
            {
                isCustomHomingStarted = true;
                customHomingThrd = new Thread(DoCustomHoming);
                customHomingThrd.IsBackground = true;
                customHomingThrd.Start();
            }

            return flag;
        }

        void DoCustomHoming()
        {
            try
            {
                IsBusy = true;
                customHomingFlag = MotionStatus.MT_HOMING_ERROR;
                int tempHomeStatus = 0;
                switch (MtrTable.CustomHomeType)
                {
                    case CustomHomeType.HomeMidPoint:
                        tempHomeStatus = home_mid_point();
                        break;
                    case CustomHomeType.HomeOnNEL:
                        tempHomeStatus = home_to_NEL();
                        break;
                    case CustomHomeType.HomeOnPEL:
                        tempHomeStatus = home_to_PEL();
                        break;
                    default:
                        break;
                }
                IsBusy = false;
                if (tempHomeStatus != 0)
                    customHomingFlag = MotionStatus.MT_HOMING_ERROR;
                else
                    customHomingFlag = MotionStatus.MT_DONE;
            }
            catch (Exception ex)
            {
                customHomingFlag = MotionStatus.MT_HOMING_ERROR;
            }
            finally
            {
                isCustomHomingStarted = false;
            }
        }

        public int home_mid_point(bool reset = true)
        {
            int status = 0;
            do
            {

                status = home_to_NEL(true);
                if (status != 0)
                    break;

                status = home_to_PEL(false);
                if (status != 0)
                    break;

                double tempPos = get_current_pos();
                for (int i = 0; i < 3; i++)
                {

                    start_ta_move(tempPos / 2, MtrTable.HmStrVel, MtrTable.HmMaxVel); //Move to home offset  
                    status = wait_motion_done(); //Wait motion complete
                    if (status != 0 && status != 11)
                        break;
                }
                if (status != 0 && status != 11)
                    break;

                if (reset)
                    set_current_pos_f(0);

                status = 0;
            }
            while (false);

            return status;
        }

        private void SetHomeProfile()
        {
            double stop_Vel = 1000;
            ushort s_Mode = 0;
            double s_Para = 0;
            DllMCC800P.YK_set_profile((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, MtrTable.HmStrVel, MtrTable.HmMaxVel, MtrTable.HmAcceleration, MtrTable.HmDeceleration, stop_Vel);
            DllMCC800P.YK_set_s_profile((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, s_Mode, s_Para);

        }

        public int home_to_PEL(bool reset = true)
        {
            int status = 0;

            do
            {
                DisableMotionInterlock = true;
                BypassLimitCheck = true; //disable limit check


                //Move Continous CCW
                tv_jog(MtrTable.HmStrVel, MtrTable.HmMaxVel, MtrTable.HmAcceleration);


                DateTime st = DateTime.Now;
                bool timeout = false;
                while (!get_pel_signal() && !timeout) //Until MEL sensed
                {
                    Thread.Sleep(5);
                    TimeSpan ts = DateTime.Now - st;
                    timeout = ts.TotalMilliseconds > MtrTable.HomeTimeOut;
                }
                sd_stop();
                if (timeout)
                {
                    status = (int)MotionStatus.MT_HOMING_ERROR;
                    break;
                }

                Thread.Sleep(20);

                while(get_pel_signal())
                {
                    int direct = (ushort)MtrTable.HomeDir == 1 ? 0 : 1;
                    DllMCC800P.YK_set_profile((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, MtrTable.HmStrVel, MtrTable.HmMaxVel, MtrTable.HmAcceleration, MtrTable.HmDeceleration, 0);
                    DllMCC800P.YK_set_s_profile((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, 0, 0);
                    curMovePos = DllMCC800P.YK_get_position((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo);
                    //Unit: Pulse
                    curMovePos -= (int)MtrTable.HmRevmm;
                    //Disable limit check
                    BypassLimitCheck = true;

                    DllMCC800P.YK_pmove((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, curMovePos, 1);
                    while (DllMCC800P.YK_check_done((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo) == 0)
                    {
                        Thread.Sleep(5);
                    }

                    Thread.Sleep(10);
                    TimeSpan ts = DateTime.Now - st;
                    timeout = ts.TotalMilliseconds > MtrTable.HomeTimeOut;
                    if (timeout) break;
                }


                //while (get_pel_signal()) //while PEL is ON
                //{

                //    //SetHomeProfile();
                //    //int dist = (int)get_mm(MtrTable.HmLimitMoveoutStep);
                //    //start_ta_move_f(-MtrTable.HmLimitMoveoutStep);
                //    //status = wait_motion_complete(); //Wait motion complete
                //    SetProfileMode(false, 100, 300, 0.1, 0.1);
                //    int direct = (ushort)MtrTable.HomeDir == 1 ? 0 : 1;
                //    DllMCC800P.YK_vmove((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, (ushort)direct);//MtrTable.Dir

                //    Thread.Sleep(1);
                //    TimeSpan ts = DateTime.Now - st;
                //    timeout = ts.TotalMilliseconds > MtrTable.HomeTimeOut;
                //    if (timeout) break;
                //}

                sd_stop();
                if (timeout)
                {
                    status = (int)MotionStatus.MT_HOMING_ERROR;
                    break;
                }

                if (reset)
                {
                    set_current_pos_f(0.0); 
                }

                //Move to home offset 
                start_ta_move(get_current_pos() - get_step(MtrTable.HmHomeOffset), MtrTable.HmStrVel, MtrTable.HmMaxVel);
               
                //Wait motion complete
                status = wait_motion_done(); 
                if (status != 0)
                    break;

            }
            while (false);
            DisableMotionInterlock = false;
            BypassLimitCheck = false;
            return status;
        }

        volatile int curMovePos = 0;
        public int home_to_NEL(bool reset = true)
        {
            int status = 0;
            //int homeCount = 0;
            bool timeout = false;

            do
            {

                DisableMotionInterlock = true;
                BypassLimitCheck = true; //disable limit check


                //Start:
                //move continous CCW
                tv_jog_Home(MtrTable.HmStrVel, MtrTable.HmMaxVel, MtrTable.HmAcceleration); 

                DateTime st = DateTime.Now;

                //Until MEL sensed
                while (!get_nel_signal() && !timeout) 
                {
                    Thread.Sleep(5);
                    TimeSpan ts = DateTime.Now - st;
                    timeout = ts.TotalMilliseconds > MtrTable.HomeTimeOut;
                }

                sd_stop();
                if (timeout)
                {
                    status = (int)MotionStatus.MT_HOMING_ERROR;
                    break;
                }

                Thread.Sleep(30);

                //While MEL is ON
                while (get_nel_signal()) 
                {
                    int direct = (ushort)MtrTable.HomeDir == 1 ? 0 : 1;
                    DllMCC800P.YK_set_profile((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, MtrTable.HmStrVel, MtrTable.HmMaxVel, MtrTable.HmAcceleration, MtrTable.HmDeceleration, 0);
                    DllMCC800P.YK_set_s_profile((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, 0, 0);
                    curMovePos = DllMCC800P.YK_get_position((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo);
                    //Unit: Pulse
                    curMovePos += (int)MtrTable.HmRevmm;
                    //Disable limit check
                    BypassLimitCheck = true; 

                    DllMCC800P.YK_pmove((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, curMovePos, 1);
                    while (DllMCC800P.YK_check_done((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo) == 0)
                    {
                        Thread.Sleep(5);
                    }
                  
                    Thread.Sleep(10);
                    TimeSpan ts = DateTime.Now - st;
                    timeout = ts.TotalMilliseconds > MtrTable.HomeTimeOut;
                    if (timeout) break;

                }
         
                sd_stop();

                if (timeout)
                {
                    status = (int)MotionStatus.MT_HOMING_ERROR;
                    break;
                }

                if (reset)
                {
                    this.set_zero_pos();
                }


                if (MtrTable.HmHomeOffset != 0)
                {
                    //Move to Home Offset 
                    start_ta_move(get_current_pos() + MtrTable.HmHomeOffset, MtrTable.HmStrVel, MtrTable.HmMaxVel);
                    //Wait motion complete
                    status = wait_motion_done(); 

                    if (status != 0)
                        break;
                }


            }
            while (false);

            DisableMotionInterlock = false;
            BypassLimitCheck = false;
            return status;
        }

        public override int home_move(int waitDelay = 0)
        {
            return (home_move(get_step(MtrTable.HmRevmm), MtrTable.HmStrVel, MtrTable.HmMaxVel, MtrSpeed.Tacc, waitDelay));
        }

        static int CardID_InBit = -1;
        static int CardInstalled = 0;
        public override bool initialization(int card, bool autoAddress)
        {
            bool tempstatus = false;
            do
            {

                if (isSimulation)
                {
                    tempstatus = true;
                    break;
                }

                if (initDel != null)
                {
                    tempstatus = initDel(card);
                    break;
                }


                DllMCC800P.YK_set_homemode((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, (ushort)MtrTable.HomeDir, (ushort)MtrTable.HmMaxVel, (ushort)MtrConfig.home_mode, 0);
                DllMCC800P.YK_set_alm_mode((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, 1, (ushort)MtrConfig.alm_logic, 0);
                DllMCC800P.YK_set_pulse_outmode((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, (ushort)MtrTable.ClockType);
                DllMCC800P.YK_set_inp_mode((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, (ushort)MtrConfig.inp_enable, (ushort)MtrConfig.inp_logic);
                DllMCC800P.YK_write_erc_pin((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, (ushort)MtrConfig.erc_logic);
                DllMCC800P.YK_set_el_mode((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, 1, (ushort)MtrConfig.limit_logic, (ushort)MtrConfig.limit_mode);


                tempstatus = true;

                break;
            } while (false);

            return tempstatus;
        }

        public override void sd_stop(double tdec)
        {
            isStopReq = true;

            if (isSimulation)
                return;
            SetProfileMode(true, tdec: tdec);
            DllMCC800P.YK_stop((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, 0);
        }

        public override void sd_stop()
        {
            isStopReq = true;

            if (isSimulation)
                return;
            SetProfileMode(true);
            DllMCC800P.YK_stop((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, (ushort)MtrConfig.sd_mode);
        }

        public override void start_ta_move(double pos, double strvel, double maxvel, double tacc, double tdec)
        {
            //Pos: Pulse Pos
            TargetPos = get_mm(pos);

            double dist = pos - currentPulse;
            if (inhibit) return;
            if (IsProhibitToMove(get_mm(dist))) return;
            if (IsExceedMaxStep(pos)) return;

            TargetPos = get_mm(pos);
            MtrTable.NewPos = pos;
            strvel *= MtrTable.SpeedFactor;
            maxvel *= MtrTable.SpeedFactor;

            MtrTable.DistanceToMove = pos - currentPulse;
            MtrTable.EstimateTimeTaken = 0.01 * (Math.Abs(MtrTable.DistanceToMove) / maxvel);
            isStopReq = false;
            pos = Math.Round(pos, 0, MidpointRounding.AwayFromZero);
            commmandStartTime = DateTime.Now;
            if (isSimulation) return;

            SetProfileMode(true, strvel, maxvel, tacc, tdec);
            if (MtrTable.Dir == 1)
                DllMCC800P.YK_pmove((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, (int)pos, 1);
            //DllMCC800P.YK_pmove_unit((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, targetPos, 0);
            else
                DllMCC800P.YK_pmove((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, (int)-pos, 1);
            //DllMCC800P.YK_pmove_unit((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, -targetPos, 0);
        }

        //...................................................................................................................................................................................................................................................................................//
        public override void sv_jog(double strvel, double maxvel, double tacc, double vsacc)
        {
            if (inhibit) return;
            if (IsProhibitToMove(100)) return;
            SetProfileMode(false, strvel, maxvel, tacc, vsacc);
            DllMCC800P.YK_vmove((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, (ushort)MtrTable.Dir);
        }
        public override void sv_jog()
        {
            if (inhibit) return;
            if (IsProhibitToMove(100)) return;
            SetProfileMode(false);
            DllMCC800P.YK_vmove((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, (ushort)MtrTable.Dir);
        }

        //...................................................................................................................................................................................................................................................................................//
        void SetProfileMode(bool tMode, double strvel = 0, double maxvel = 0, double tacc = 0, double tdec = 0, double vsacc = 0)
        {
            ushort cardNo = (ushort)MtrTable.CardNo;
            ushort axisNo = (ushort)MtrTable.AxisNo;
            double min_Vel = strvel == 0 ? MtrSpeed.StrVel : strvel;
            double max_Vel = maxvel == 0 ? MtrSpeed.MaxVel : maxvel;
            double tAcc = tacc == 0 ? MtrSpeed.VSacc : tacc;
            double tDec = tdec == 0 ? MtrSpeed.VSdec : tdec;
            double stop_Vel = 1000;

            ushort s_Mode = 0;
            double s_Para = tMode == false ? 0 : 0;

            DllMCC800P.YK_set_profile(cardNo, axisNo, min_Vel, max_Vel, tAcc, tDec, stop_Vel);
            DllMCC800P.YK_set_s_profile(cardNo, axisNo, s_Mode, s_Para);

        }
        public void tv_jog_Home(double strvel, double maxvel, double tacc)
        {
            if (inhibit) return;
            if (IsProhibitToMove(1)) return;
            SetProfileMode(true, strvel, maxvel, tacc);
            DllMCC800P.YK_vmove((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, (ushort)MtrTable.HomeDir);
        }
        public override void tv_jog(double strvel, double maxvel, double tacc)
        {
            if (inhibit) return;
            if (IsProhibitToMove(1)) return;
            SetProfileMode(true, strvel, maxvel, tacc);
            DllMCC800P.YK_vmove((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, (ushort)MtrTable.Dir);
        }
        public override void tv_jog()
        {
            if (inhibit) return;
            if (IsProhibitToMove(100)) return;

            SetProfileMode(true);
            DllMCC800P.YK_vmove((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, (ushort)MtrTable.Dir);
        }

        public override void start_sa_move(double pos, double strvel, double maxvel, double tacc, double tdec, double vsacc, double vsdec)
        {
            MtrTable.NewPos = pos;
            TargetPos = get_mm(pos);
            double dist = get_mm(pos - currentPhysicalPos);
            if (inhibit) return;
            if (IsProhibitToMove(dist)) return;
            if (IsExceedMaxStep(pos)) return;

            //if (IsRunning())
            //	return;

            strvel *= MtrTable.SpeedFactor;
            maxvel *= MtrTable.SpeedFactor;
            vsacc *= MtrTable.SpeedFactor;
            vsdec *= MtrTable.SpeedFactor;
            TargetPos = get_mm(pos);

            MtrTable.DistanceToMove = pos - currentPhysicalPos;
            MtrTable.EstimateTimeTaken = 0.01f * (Math.Abs(MtrTable.DistanceToMove) / maxvel);
            pos = Math.Round(pos, 0, MidpointRounding.AwayFromZero);
            isStopReq = false;
            //TargetPos = pos;
            commmandStartTime = DateTime.Now;
            if (isSimulation) return;
            SetProfileMode(false, strvel, maxvel, tacc, tdec, vsacc);

            if (MtrTable.Dir == 1)
                DllMCC800P.YK_pmove((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, (int)pos, 1);
            else
                DllMCC800P.YK_pmove((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, (int)-pos, 1);


        }

        public override void start_tr_move(double dist, double strvel, double maxvel, double tacc, double tdec)
        {
            this.start_ta_move(dist, strvel, maxvel, tacc, tdec);
        }
        
        public override void start_sr_move(double dist, double strvel, double maxvel, double tacc, double tdec, double vsacc, double vsdec)
        {
            double dirMul = MtrTable.Dir == 1 ? 1 : -1;
            MtrTable.NewPos = currentPhysicalPos + dirMul * dist;

            if (inhibit) return;
            if (IsProhibitToMove(get_mm(dist))) return;
            if (IsExceedMaxStep(dist)) return;

            //if (IsRunning())
            //	return;

            strvel *= MtrTable.SpeedFactor;
            maxvel *= MtrTable.SpeedFactor;
            vsacc *= MtrTable.SpeedFactor;
            vsdec *= MtrTable.SpeedFactor;

            MtrTable.EstimateTimeTaken = 0.01f * (Math.Abs(dist) / maxvel);
            dist = Math.Round(dist, 0, MidpointRounding.AwayFromZero);
            isStopReq = false;

            commmandStartTime = DateTime.Now;
            if (isSimulation) return;
            SetProfileMode(false, strvel, maxvel, tacc, tdec, vsacc);

            if (MtrTable.Dir == 1)
                DllMCC800P.YK_pmove((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, (int)dist, 1);
            else
                DllMCC800P.YK_pmove((ushort)MtrTable.CardNo, (ushort)MtrTable.AxisNo, (int)-dist, 1);
        }

        public override void brake_free(bool set)
        {
            if (MtrTable.Brake != 0)
            {
                if (MtrTable.MotorBrake != null)
                    MtrTable.MotorBrake(set);
            }
        }

        public override bool uninitialization(int card)
        {
            DllMCC800P.YK_board_close();
            return true;
        }

        public override void start_a_arc2(ref AxisBase pxmtr, ref AxisBase pymtr, short Dir)
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
        public override void start_sa_line2(ref AxisBase pxmtr, ref AxisBase pymtr, double strvel, double maxvel, double tacc, double tdec, double vsacc, double vsdec)
        {
            throw new NotImplementedException();
        }
        public override void start_sa_line3(ref AxisBase pxmtr, ref AxisBase pymtr, ref AxisBase pzmtr, double strvel, double maxvel, double tacc, double tdec, double vsacc, double vsdec)
        {
            throw new NotImplementedException();
        }
        public override void start_sa_line4(ref AxisBase pxmtr, ref AxisBase pymtr, ref AxisBase pzmtr, ref AxisBase pumtr, double strvel, double maxvel, double tacc, double tdec, double vsacc, double vsdec)
        {
            throw new NotImplementedException();
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
        public override void start_ta_line3(ref AxisBase pxmtr, ref AxisBase pymtr, ref AxisBase pzmtr, double strvel, double maxvel, double tacc, double tdec)
        {
            throw new NotImplementedException();
        }
        public override void start_ta_line4(ref AxisBase pxmtr, ref AxisBase pymtr, ref AxisBase pzmtr, ref AxisBase pumtr, double strvel, double maxvel, double tacc, double tdec)
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

        public override int GetAnalogValue()
        {
            return 0;
        }
    }
}
