using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanley_MCPNet.IO.Mcc800
{
    public class IOMcc800P:IOBase, IIOBase
    {
        public override bool io_initialization(int card_number)
        {
            return true;
        }

        public override bool port_config(int CardNo, int Port, int type)
        {
            throw new NotSupportedException("MCC800P Has No Config Support");
        }

        public override bool inp_chkbit(int card_no, int port_no, int bit)
        {
            short num;
            if (bit == 0)
            {

            }
            num = DllIOMcc800P.YK_read_inbit((ushort)card_no, (ushort)bit);
            //0:Low
            //1:High

            return num == 0 ? true : false;
        }

        public override void out_bit(int card_no, int port_no, int obit, ref int obuf, bool sts)
        {
            //bit Range: 0-15
            //0:Low
            //1:High
            ushort ists = sts ? (ushort)1 : (ushort)0;
            DllIOMcc800P.YK_write_outbit((ushort)card_no, (ushort)obit, ists);
        }

        public override void outport(int card_no, int port_no, int do_data)
        {
            throw new NotSupportedException("MCC800P No Need");
        }

        public override int inport(int card_no, int port_no)
        {
            throw new NotSupportedException("MCC800P No Need");
        }

        private const int MAX_CARD = 32;
        private ushort[] CardArray = new ushort[32];
    }
}
