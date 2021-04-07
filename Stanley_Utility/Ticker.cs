using System;
using System.ComponentModel;
using System.Timers;

namespace Stanley_Utility
{
    public class Ticker : INotifyPropertyChanged
    {
        public Ticker()
        {
            Timer timer = new Timer();
            timer.Interval = 800.0;
            timer.Elapsed += this.timer_Elapsed;
            timer.Start();
        }

        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs("Now"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
