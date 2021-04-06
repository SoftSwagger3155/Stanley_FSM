using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace LX_MCPNet.Data
{
    public delegate bool BrakeDel(bool set);
    public delegate bool MotionDel();
    public delegate bool MotionDel2(ref string sErr);
    public delegate bool MotionInterlockDel(double currentPos, double newPos);

    public enum CustomHomeType
    {
        HomeOnNEL,
        HomeOnPEL,
        HomeMidPoint
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class MtrTable
    {
        [Category("Axis Table")]
        [Description("Axis name")]
        [DisplayName("Name")]
        public string Name {
            get;
            set;
        } 

        [Description("Axis Short name")]
        [Category("Axis Table")]
        [DisplayName("ShortName")]
        public string ShortName { get; set; }

        [Description("AxisID")]
        [Category("Axis Table")]
        [DisplayName("Axis ID")]
        public string AxisID
        {
            get
            {
                return this.axisID;
            }
        }

        [DisplayName("Eanble Backlash Comp.")]
        [Description("Eanble Backlash Compensation Function")]
        [Category("Backlash Compensation")]
        public bool EnableBackLashCompensation
        {
            get
            {
                return this.enableBackLashCompensation;
            }
            set
            {
                this.enableBackLashCompensation = value;
            }
        }

        [DisplayName("Backlash Comp. Forword Add up")]
        [Category("Backlash Compensation")]
        [Description("Backlash Compensation Forword Add up")]
        public float BackLashCompenstationForworkAddUp
        {
            get
            {
                return this.backLashCompenstationForwardAddUp;
            }
            set
            {
                this.backLashCompenstationForwardAddUp = value;
            }
        }

        [DisplayName("Backlash Comp. Backward Add up")]
        [Category("Backlash Compensation")]
        [Description("Backlash Compensation Backward Add up")]
        public float BackLashCompenstationBackwardAddUp
        {
            get
            {
                return this.backLashCompenstationBackwardAddUp;
            }
            set
            {
                this.backLashCompenstationBackwardAddUp = value;
            }
        }

        [DisplayName("Settling Delay (ms)")]
        [Category("Inposition Settling")]
        [Description("Settling Delay After Inposition (ms)")]
        public int InpositionSettlingDwell
        {
            get
            {
                return this.inpositionSettlingDwell;
            }
            set
            {
                this.inpositionSettlingDwell = value;
            }
        }

        [DisplayName("Wait Sleep (ms)")]
        [Description("Motion Done Wait Check Sleep (ms)")]
        [Category("Inposition Settling")]
        public int MotionDoneWaitSleep
        {
            get
            {
                return this.motionDoneWaitSleep;
            }
            set
            {
                this.motionDoneWaitSleep = value;
            }
        }

        [Category("Axis Table")]
        [DisplayName("CardNo")]
        [Description("Card #No")]
        public short CardNo { get; set; }

        [Category("Axis Table")]
        [DisplayName("AxisNo")]
        [Description("Axis #No")]
        public short AxisNo { get; set; }

        [DisplayName("GlobalSpeed")]
        [Description("Global speed")]
        [Category("Misc")]
        public double Speed { get; set; }

        [Category("Misc")]
        [Description("InpositionR adius")]
        [DisplayName("InpositionRadius")]
        public double InpositionRadius { get; set; }

        [Category("Distance")]
        [DisplayName("MaxDistance")]
        [Description("Maximum distance motor can run in mm/Degree")]
        public double Maxmm { get; set; }

        [Description("Minimum distance motor can run in mm/Degree")]
        [DisplayName("MinDistance")]
        [Category("Distance")]
        public double Minmm { get; set; }

        [DisplayName("Direction")]
        [Category("Direction")]
        [Description("Direction (0:Reversed/1:Forward)")]
        public short Dir { get; set; }

        [Category("Direction")]
        [DisplayName("HomeDirection")]
        [Description("Home Direction (0:Reversed/1:Forward)")]
        public short HomeDir { get; set; }

        [DisplayName("ClockType")]
        [Category("Axis Table")]
        [Description("Clock type (0 - 7, Please refer to manual)")]
        public short ClockType { get; set; }

        [Category("Axis Table")]
        [DisplayName("MotorType")]
        [Description("Motor type (0:Stepper/1:Servo). If set 1, check Ready Signal for Stop Motion.")]
        public short MtrType { get; set; }

        [Description("Motor type (0:low, 1:high)")]
        [DisplayName("Servo On Logic")]
        [Category("Axis Table")]
        public short ServoOnLogic { get; set; }

        [DisplayName("Encoder")]
        [Category("Axis Table")]
        [Description("Encoder (0:No/1:Yes)")]
        public short Encoder { get; set; }

        [DisplayName("Encoder")]
        [Description("Encoder Ratio")]
        public double EncoderRatio { get; set; }

        [Description("Speed type (0:Linear or Trapezoidal/1:S-Curve)")]
        [Category("Axis Table")]
        [DisplayName("SpeedType")]
        public short SpeedType { get; set; }

        [Category("Distance")]
        [DisplayName("HomePosOffset")]
        [Description("Motor offset, for mechanical adjustment if physical position shifted")]
        public double Offset { get; set; }

        [Description("Motor brake (0:No brake/1:With brake)")]
        [Category("Axis Table")]
        [DisplayName("Brake")]
        public short Brake { get; set; }

        [Category("On Board")]
        [DisplayName("OnBoardHome")]
        [Description("On board home (0:No/1:Yes)")]
        public short OnBoardHome { get; set; }

        [Description("On board limit positive & negative inputs (0:No/1:Yes). If set 0, Limit Sensor Homing no error.")]
        [DisplayName("OnBoardLmt")]
        [Category("On Board")]
        public short OnBoardLmt { get; set; }

        [DisplayName("Use ACS Homing Buffer")]
        [Category("ACS Homing Buffer")]
        [Description("Enabled ACS Homing Buffer Mode")]
        public bool UseACSHomingBuffer { get; set; }

        [Description("ACS Buffer Program")]
        [Category("ACS Homing Buffer")]
        [DisplayName("ACS Buffer Program")]
        public short ACSHomingBuffer { get; set; }

        [Description("ACS Home Flag Name")]
        [Category("ACS Homing Buffer")]
        [DisplayName("ACS Home Flag Var. Name")]
        public string ACSHomeFlagName { get; set; }

        [Category("ACS Variables")]
        [DisplayName("Write ACS Distance Var")]
        [Description("Write ACS Distance Var")]
        public bool EnableWriteACSDistacneVar { get; set; }

        [DisplayName("ACS Distance Var Name")]
        [Category("ACS Variables")]
        [Description("ACS Distance Var Name")]
        public string ACSDistacneVarName { get; set; }

        [DisplayName("ACS Distance Var Write Delay")]
        [Description("ACS Distance Var Write Delay")]
        [Category("ACS Variables")]
        public int ACSDisVarNameWriteDelay { get; set; }

        [Category("Secondary Axis")]
        [DisplayName("Second Axis Number")]
        [Description("Second Axis Number")]
        public int SecondAxisNumber { get; set; }

        [Description("Encoder Axis Number")]
        [Category("Encoder Axis")]
        [DisplayName("Encoder Axis Number")]
        public int EncoderAxisNo
        {
            get
            {
                return this.encoderAxisNo;
            }
            set
            {
                this.encoderAxisNo = value;
            }
        }

        [Category("Encoder Axis")]
        [Description("Close Loop With Encoder Axis")]
        [DisplayName("Close Loop With Encoder Axis")]
        public bool CloseLoopWithEncoderAxis
        {
            get
            {
                return this.closeLoopWithEncoderAxis;
            }
            set
            {
                this.closeLoopWithEncoderAxis = value;
            }
        }

        [Description("Encoder Axis Close Try Count")]
        [Category("Encoder Axis")]
        [DisplayName("Encoder Axis Close Try Count")]
        public int EncoderAxisCloseTryCount
        {
            get
            {
                return this.encoderAxisCloseTryCount;
            }
            set
            {
                this.encoderAxisCloseTryCount = value;
            }
        }

        [DisplayName("Timing")]
        [Description("Timing")]
        [Category("Status Read Timing")]
        public int StatusReadTiming { get; set; }

        [Description("Motor step per revolution")]
        [Category("Conversion")]
        [DisplayName("StepPerRevolution")]
        public double StepPerRevolution { get; set; }

        [DisplayName("mmPerRevolution")]
        [Category("Conversion")]
        [Description("mm per motor revolution")]
        public double mmPerRevolution { get; set; }

        [Category("StepSize")]
        [Description("Default Step Size in Motion Panel")]
        [DisplayName("DefaultStepSize")]
        public double StepSize
        {
            get
            {
                return this.stepSize;
            }
            set
            {
                if (value <= 0.0)
                {
                    value = 1.0;
                }
                this.stepSize = value;
            }
        }

        [Description("Keep Last Use Step Size in Motion Panel")]
        [Category("StepSize")]
        [DisplayName("KeepLastUseStepSize")]
        public bool KeepLastUseStepSize { get; set; }

        [DisplayName("RoundOffNearestMinStepSize")]
        [Description("Round Off Command Postion to Nearest Min Step Size for relative distance")]
        [Category("StepSize")]
        public bool RoundOffNearestMinStepSize { get; set; }

        [Category("Conversion")]
        [DisplayName("DefaultMaxStepSize")]
        [Description("Default Maximum Step Size in Motion Panel")]
        public double MaxStepSize
        {
            get
            {
                return this._maxStepSize;
            }
            set
            {
                this._maxStepSize = value;
            }
        }

        [Description("Current Feedback Position Reading Reverse")]
        [Category("Conversion")]
        [DisplayName("Feedback Reading Reverse")]
        public bool FeedbackReadingReverse
        {
            get
            {
                return this._feedbackReadingReverse;
            }
            set
            {
                this._feedbackReadingReverse = value;
            }
        }

        [Category("Conversion")]
        [DisplayName("DefaultMinStepSize")]
        [Description("Default Minimum Step Size in Motion Panel")]
        public double MinStepSize
        {
            get
            {
                return this._minStepSize;
            }
            set
            {
                this._minStepSize = value;
            }
        }

        public void SetAxisID(string axID)
        {
            this.axisID = axID;
        }

        [DisplayName("HmStrVel")]
        [Category("Home setting")]
        [Description("Home start speed")]
        public double HmStrVel { get; set; }

        [Category("Home setting")]
        [Description("Home maximum speed")]
        [DisplayName("HmMaxVel")]
        public double HmMaxVel { get; set; }

        [DisplayName("HmAcc")]
        [Description("Home Acceleration")]
        [Category("Home setting")]
        public double HmAcceleration { get; set; }

        [Description("Home Deceleration")]
        [Category("Home setting")]
        [DisplayName("HmDec")]
        public double HmDeceleration { get; set; }

        [Description("Custom Home Type (0:NEL/1:PEL/2:MID)")]
        [Category("Home setting")]
        [DisplayName("CustomHomeType")]
        public CustomHomeType CustomHomeType { get; set; }

        [DisplayName("HmLimitMoveoutStep")]
        [Category("Home setting")]
        [Description("Home Limit Moveout Step")]
        public double HmLimitMoveoutStep { get; set; }

        [Category("Home setting")]
        [Description("Home Offset")]
        [DisplayName("HmHomeOffset")]
        public double HmHomeOffset { get; set; }

        [Category("Home setting")]
        [DisplayName("HmRevmm")]
        [Description("Home Reverse motion in mm")]
        public double HmRevmm { get; set; }

        [DisplayName("SpeedFactor")]
        [Description("SpeedFactor")]
        [Category("Axis Table")]
        public double SpeedFactor
        {
            get
            {
                return (this.speedFactor <= 0.0) ? 1.0 : this.speedFactor;
            }
            set
            {
                this.speedFactor = value;
            }
        }

        [DisplayName("MotionTimeOut")]
        [Category("Time Out")]
        [Description("Motion Time Out in mili-second")]
        public int MotionTimeOut { get; set; }

        [Category("Time Out")]
        [Description("Home Time Out in mili-second")]
        [DisplayName("HomeTimeOut")]
        public int HomeTimeOut { get; set; }

        [Description("True: Left-Limit Disable")]
        [Category("Disable Limit Error")]
        [DisplayName("Left-Limit")]
        public bool IsDisableLeftLimit { get; set; }

        [Description("True: Right-Limit Disable")]
        [DisplayName("Right-Limit")]
        [Category("Disable Limit Error")]
        public bool IsDisableRightLimit { get; set; }

        [XmlIgnore]
        public int GetFeedBackReadAxis
        {
            get
            {
                return (this.encoderAxisNo < 0) ? ((int)this.AxisNo) : this.encoderAxisNo;
            }
        }

        public void CopyFrom(MtrTable mtrTbl)
        {
            this.AxisNo = mtrTbl.AxisNo;
            this.Brake = mtrTbl.Brake;
            this.CardNo = mtrTbl.CardNo;
            this.ClockType = mtrTbl.ClockType;
            this.Dir = mtrTbl.Dir;
            this.Encoder = mtrTbl.Encoder;
            this.HmMaxVel = mtrTbl.HmMaxVel;
            this.HmRevmm = mtrTbl.HmRevmm;
            this.HmStrVel = mtrTbl.HmStrVel;
            this.HomeDir = mtrTbl.HomeDir;
            this.HomeFlag = mtrTbl.HomeFlag;
            this.HomeTimeOut = mtrTbl.HomeTimeOut;
            this.Maxmm = mtrTbl.Maxmm;
            this.mmPerRevolution = mtrTbl.mmPerRevolution;
            this.MotionTimeOut = mtrTbl.MotionTimeOut;
            this.MtrType = mtrTbl.MtrType;
            this.Name = mtrTbl.Name;
            this.Offset = mtrTbl.Offset;
            this.OnBoardHome = mtrTbl.OnBoardHome;
            this.OnBoardLmt = mtrTbl.OnBoardLmt;
            this.Speed = mtrTbl.Speed;
            this.SpeedFactor = mtrTbl.SpeedFactor;
            this.SpeedType = mtrTbl.SpeedType;
            this.StepPerRevolution = mtrTbl.StepPerRevolution;
            this.StepSize = mtrTbl.StepSize;
            this.ServoOnLogic = mtrTbl.ServoOnLogic;
            this.pIsInhibitToHome = mtrTbl.pIsInhibitToHome;
            this.pIsInhibitToMove = mtrTbl.pIsInhibitToMove;
            this.EncoderRatio = mtrTbl.EncoderRatio;
            this.HmHomeOffset = mtrTbl.HmHomeOffset;
            this.Minmm = mtrTbl.Minmm;
            this.ACSHomeFlagName = mtrTbl.ACSHomeFlagName;
            this.ACSHomingBuffer = mtrTbl.ACSHomingBuffer;
            this.UseACSHomingBuffer = mtrTbl.UseACSHomingBuffer;
            this.SecondAxisNumber = mtrTbl.SecondAxisNumber;
            this.IsDisableLeftLimit = mtrTbl.IsDisableLeftLimit;
            this.IsDisableRightLimit = mtrTbl.IsDisableRightLimit;
            this.InpositionRadius = mtrTbl.InpositionRadius;
            this.StatusReadTiming = mtrTbl.StatusReadTiming;
            this.MinStepSize = mtrTbl.MinStepSize;
            this.RoundOffNearestMinStepSize = mtrTbl.RoundOffNearestMinStepSize;
            this.inpositionSettlingDwell = mtrTbl.inpositionSettlingDwell;
            this.motionDoneWaitSleep = mtrTbl.motionDoneWaitSleep;
            this.EnableWriteACSDistacneVar = mtrTbl.EnableWriteACSDistacneVar;
            this.ACSDistacneVarName = mtrTbl.ACSDistacneVarName;
            this.ACSDisVarNameWriteDelay = mtrTbl.ACSDisVarNameWriteDelay;
            this.EncoderAxisNo = mtrTbl.EncoderAxisNo;
            this.encoderAxisCloseTryCount = mtrTbl.encoderAxisCloseTryCount;
            this.closeLoopWithEncoderAxis = mtrTbl.closeLoopWithEncoderAxis;
        }

        public MtrTable()
        {
            this.Name = string.Empty;
            this.ShortName = string.Empty;
            this.HmAcceleration = 0.1;
            this.HmDeceleration = 0.1;
            this.HmMaxVel = 1000.0;
            this.HmStrVel = 100.0;
            this.HmLimitMoveoutStep = 1.0;
            this.HmHomeOffset = 0.0;
            this.Minmm = 0.0;
            this.EncoderRatio = 1.0;
            this.StepPerRevolution = 1.0;
            this.mmPerRevolution = 1.0;
            this.SecondAxisNumber = -1;
            this.IsDisableLeftLimit = false;
            this.IsDisableRightLimit = false;
            this.InpositionRadius = 0.5;
            this.StepSize = 1.0;
            this.KeepLastUseStepSize = true;
            this.RoundOffNearestMinStepSize = false;
            this.MinStepSize = 0.001;
            this.StatusReadTiming = 300;
            this.InpositionSettlingDwell = 0;
            this.EnableWriteACSDistacneVar = false;
            this.ACSDistacneVarName = "";
            this.ACSDisVarNameWriteDelay = 1;
            this.EncoderAxisNo = -1;
            this.encoderAxisCloseTryCount = 5;
            this.closeLoopWithEncoderAxis = false;
        }

        private string axisID = "";
        //private string name = string.Empty;
        //private string shortName = string.Empty;

        private bool enableBackLashCompensation = false;

        private float backLashCompenstationForwardAddUp = 0f;

        private float backLashCompenstationBackwardAddUp = 0f;

        private int inpositionSettlingDwell = 0;

        private int motionDoneWaitSleep = 5;

        private int encoderAxisNo = -1;

        private bool closeLoopWithEncoderAxis = false;

        private int encoderAxisCloseTryCount = 5;

        [XmlIgnore]
        public BrakeDel MotorBrake = null;

        [XmlIgnore]
        public MotionDel pHomeSensing = null;

        [XmlIgnore]
        public MotionDel pFwdSensing = null;

        [XmlIgnore]
        public MotionDel pRevSensing = null;

        [XmlIgnore]
        public MotionDel pCriticalEvent = null;

        [XmlIgnore]
        public MotionDel pHangedEvent = null;

        [XmlIgnore]
        public MotionDel pResetAlarm = null;

        [XmlIgnore]
        public MotionDel pTimes10Res = null;

        [XmlIgnore]
        public MotionDel2 pIsInhibitToHome = null;

        [XmlIgnore]
        public MotionDel2 pIsInhibitToMove = null;

        [XmlIgnore]
        public MotionDel pInhibitToMoveManualStep = null;

        private double stepSize = 1.0;

        protected double _maxStepSize = 5.0;

        protected bool _feedbackReadingReverse = false;

        protected double _minStepSize = 0.001;

        private double speedFactor = 1.0;

        [XmlIgnore]
        public double NewPos;

        [XmlIgnore]
        public double CurPos = 2105.0;
        
        [XmlIgnore]
        public double Dist;

        [XmlIgnore]
        public short HomeFlag;

        [XmlIgnore]
        public short RunFlag;

        [XmlIgnore]
        public short ErrorFlag;

        [XmlIgnore]
        public double ArcCtr;

        [XmlIgnore]
        public double ArcEnd;

        [XmlIgnore]
        public double DistanceToMove;

        [XmlIgnore]
        public double EstimateTimeTaken;
    }
}
