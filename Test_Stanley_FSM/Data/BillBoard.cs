using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Test_Stanley_FSM.Data
{
    public class BillBoard: INotifyPropertyChanged
    {
        string messageA = "Hello FSM A";
        public string MessageA
        {
            get { return messageA; }
            set
            {
                messageA = value;
                OnPropertyChanged(nameof(MessageA));
            }
        }

        string messageB = "Hello FSM B";
        public string MessageB
        {
            get { return messageB; }
            set
            {
                messageB = value;
                OnPropertyChanged(nameof(MessageB));
            }
        }

        string messageC = "Hello FSM C";
        public string MessageC
        {
            get { return messageC; }
            set
            {
                messageC = value;
                OnPropertyChanged(nameof(MessageC));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }
    }
}
