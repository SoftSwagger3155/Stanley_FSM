using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Stanley_FSM.Monitor
{
    public abstract class IMachineMonitor: INotifyPropertyChanged
    {
        protected StateMachineRunStatus runStatus = StateMachineRunStatus.Idle;
        protected FSMRunMode runMode = FSMRunMode.SingleState;

        public StateMachineRunStatus RunStatus
        {
            get { return runStatus; }
            set
            {
                runStatus = value;
                OnPropertyChanged("RunStatus");
            }
        }
        public FSMRunMode RunMode
        {
            get { return runMode; }
            set { runMode = value;
                OnPropertyChanged("RunMode");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }
    }
}
