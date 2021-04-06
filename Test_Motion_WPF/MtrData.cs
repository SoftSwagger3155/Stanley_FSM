using LX_MCPNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Motion_WPF
{
    public class MtrData
    {
        public static MtrMisc[] MtrMiscArray = new MtrMisc[Motor.TotalAxes];

        public static void InitMtrMiscArray(List<MtrMisc> LoadedMics = null)
        {
            for (int i = 0; i < MtrMiscArray.Length; i++)
            {
                MtrMiscArray[i] = (LoadedMics != null && i < LoadedMics.Count) ? LoadedMics[i] : new MtrMisc();
            }
        }

        #region Axis_01
        public static MtrConfig Axis_01_Config = new MtrConfig
        {

        };
        public static MtrTable Axis_01_Tbl = new MtrTable
        {
            AxisNo = 0,
            Name = "Axis_01",
            UseACSHomingBuffer = true
        };
        public static MtrSpeed Axis_01_Sp = new MtrSpeed
        {

        };
        #endregion
        #region Axis_02
        public static MtrConfig Axis_02_Config = new MtrConfig
        {

        };
        public static MtrTable Axis_02_Tbl = new MtrTable
        {
            AxisNo = 0,
            Name = "Axis_02",
            UseACSHomingBuffer = true
        };
        public static MtrSpeed Axis_02_Sp = new MtrSpeed
        {

        };
        #endregion
        #region Axis_03
        public static MtrConfig Axis_03_Config = new MtrConfig
        {

        };
        public static MtrTable Axis_03_Tbl = new MtrTable
        {
            AxisNo = 0,
            Name = "Axis_03",
            UseACSHomingBuffer = true
        };
        public static MtrSpeed Axis_03_Sp = new MtrSpeed
        {

        };
        #endregion
    }
}
