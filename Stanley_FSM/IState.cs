using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace Stanley_FSM
{
    public abstract class IState: INotifyPropertyChanged
    {
        
        protected FSMStateStatus currentStatus = FSMStateStatus.UnKnown;
        protected void OnPropertyChanged(string info) { if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(info)); }
       
        
        public int Id { get; set; }
        public string Name { get; set; }
        public IState CurrentState { get; set; }
        public IState PreviousState { get; set; }
        public IState NextState { get; set; }
        public FSMStateStatus CurrentStatus
        {
            get { return currentStatus; }
            set { currentStatus = value;
                OnPropertyChanged("CurrentStatus");
                OnPropertyChanged("FSMStateStatusDisplay");
            }
        }
        public string FSMStateStatusDisplay
        {
            get { return CurrentStatus.ToString(); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        //public void SetNextState(IState nextState)
        //{
        //    this.NextState = nextState;
        //}

        //public void SetPreviousState(IState previous)
        //{
        //    this.PreviousState = previous;
        //}

        public abstract int Execute();
    }
}
