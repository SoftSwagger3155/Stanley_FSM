using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LX_MCPNet.Data
{
    public interface IMotionProfile
    {
        string FullName { get; }
        string AxisName { get; set; }
        string Name { get; }
        int SerialNo { get; }
        float StartVel { get; set; }
        float MaxVel { get; set; }
        float Distance { get; set; }
        double Acc { get; set; }
        double Dec { get; set; }
        double Jerk { get; set; }
        bool IsBasedProf { get; }
        bool IsDistanceProf { get; }
        bool IsRequiredJerk { get; set; }
        bool IsRequiredStartVelocity { get; set; }
        string AxisID { get; set; }
        string MtrUnit { get; set; }
        bool IsEnableDistanceProf { get; set; }
        void CopyValueFrom(IMotionProfile src);
    }
}
