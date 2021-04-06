using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace LX_MCPNet.Data
{
    public class ZSlowDownProfile : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                this.OnPropertyChanged("Name");
            }
        }

        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
            set
            {
                this.isEnabled = value;
                this.OnPropertyChanged("IsEnabled");
                this.OnPropertyChanged("Info");
            }
        }

        public float SlowdownGap
        {
            get
            {
                return this.slowdownGap;
            }
            set
            {
                this.slowdownGap = value;
                this.OnPropertyChanged("SlowdownGap");
                this.OnPropertyChanged("Info");
            }
        }

        [XmlIgnore]
        public float GetSlowFactor
        {
            get
            {
                return this.slowdownGapSpeedFactor / 100f;
            }
        }

        public float SlowdownGapSpeedFactor
        {
            get
            {
                return this.slowdownGapSpeedFactor;
            }
            set
            {
                this.slowdownGapSpeedFactor = value;
                this.OnPropertyChanged("SlowdownGapSpeedFactor");
                this.OnPropertyChanged("Info");
            }
        }

        [XmlIgnore]
        public string Info
        {
            get
            {
                return string.Format("<{3}> Enabled={0}, Gap={1:0.000} Speed={2:0.00}%", new object[]
                {
                    this.isEnabled,
                    this.slowdownGap,
                    this.slowdownGapSpeedFactor,
                    this.name
                });
            }
        }
        
        private string name = "";
        private bool isEnabled = true;
        private float slowdownGap = 1f;
        private float slowdownGapSpeedFactor = 10f;
    }
}
