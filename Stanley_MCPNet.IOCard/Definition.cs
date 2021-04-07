using System;
using System.ComponentModel;

namespace Stanley_MCPNet.IOCard
{

    public enum AdlinkIOCardModel
    {
        [Description("Unkonwn")]
        Unknown,
        [Description("PCI-7296")]
        PCI7296
    }

    public enum WagoCardModel
    {
        [Description("Unkonwn")]
        Unknown,
        [Description("Wago Standard IO")]
        WagoStandardIO
    }

    public enum IOCardVendor
    {
        [Description("Unkonwn")]
        Unknown,
        [Description("Adlink")]
        Adlink,
        [Description("Wago")]
        Wago,
        [Description("ACSWagoEtherCAT")]
        ACSWagoEtherCAT,
        [Description("MC800P")]
        MC800P
    }
}
