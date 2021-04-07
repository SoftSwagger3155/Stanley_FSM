using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanley_MCPNet.IO
{
    public interface IIOBase
    {
        bool io_initialization(int card_number);
        bool port_config(int CardNo, int Port, int type);
        bool inp_chkbit(int card_no, int port_no, int bit);
        void out_bit(int card_no, int port_no, int obit, ref int obuf, bool sts);
        void outport(int card_no, int port_no, int do_data);
        int inport(int card_no, int port_no);
    }
}
