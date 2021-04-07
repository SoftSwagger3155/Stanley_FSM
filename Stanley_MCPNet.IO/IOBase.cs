using System;
using System.ComponentModel;
using System.Threading;

namespace Stanley_MCPNet.IO
{
    public class IOBase : IIOBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        public virtual int inport(int card_no, int port_no)
        {
            return 0;
        }

        public virtual bool inp_chkbit(int card_no, int port_no, int bit)
        {
            return true;
        }

        public virtual bool inp_chkbit(int card_no, int port_no, int bit, ref int obuf)
        {
            return true;
        }


        public virtual bool io_initialization(int card_number)
        {
            return true;
        }

        public virtual void outport(int card_no, int port_no, int do_data)
        {

        }

        public virtual void out_bit(int card_no, int port_no, int obit, ref int obuf, bool sts)
        {

        }

        public virtual bool port_config(int CardNo, int Port, int type)
        {
            return true;
        }

        public virtual bool outbit_readback(int card_no, int port_no, int obit, ref int obuf)
        {
            return (obuf & obit) == obit;
        }

        public virtual bool wait_inpbit(int card_no, int port_no, int bit, bool wait_sts, int wait_delay, int sleep_delay)
        {
            DateTime st = DateTime.Now;
            while ((DateTime.Now - st).TotalMilliseconds <= (double)wait_delay)
            {
                if (sleep_delay > 0)
                {
                    Thread.Sleep(sleep_delay);
                }
                if (this.inp_chkbit(card_no, port_no, bit) == wait_sts)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual void Dispose()
        {
        }
    }
}
