using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanley_MCPNet.Motion
{
    public enum AdLinkIOSTATUS
    {
        RDY = 1,
        ALM,
        PEL = 4,
        MEL = 8,
        ORG = 16,
        DIR = 32,
        EMG = 64,
        PCS = 128,
        ERC = 256,
        EZ = 512,
        CLR = 1024,
        LTC = 2048,
        SD = 4096,
        INP = 8192,
        SVON = 16384
    }

    public enum MccIOSTATUS
    {
        ALM = 1,
        PEL = 2,
        MEL = 4,
        EMG = 8,
        ORG = 16,
        PSL = 32,
        MSL =64,
        INP = 128,
        EZ = 256,
        RDY = 512,
        DSTP = 1024
    }

    public enum ComMode
    {
        COM_PCI_BUS,
        COM_ETHERNET,
        COM_SERIAL,
        COM_DIRECT
    }
}
