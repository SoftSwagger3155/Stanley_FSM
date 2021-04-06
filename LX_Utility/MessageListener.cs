using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace LX_Utility
{
    public enum MessageType
    {
        Statement,
        Warning,
        Error
    }
    public class MessageListener : DependencyObject, INotifyPropertyChanged
    {
        private static MessageListener mInstance;
        private Thread showWorkingThread;
        private DateTime lastRecivedMessageTime = DateTime.Now;
        private string currentMessage = "";
        private string workingProgressStr = "";
        private string newMessage = "";
        private volatile bool receivedNewMessage = false;

        private int listCount = 0;

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(MessageListener), new UIPropertyMetadata(null));
        public static readonly DependencyProperty SelectedMessageProperty = DependencyProperty.Register("MessageInfo", typeof(MessageInfo), typeof(MessageListener), new UIPropertyMetadata(null));
        public static readonly DependencyProperty MessageListProperty = DependencyProperty.Register("MessageList", typeof(ObservableCollection<MessageInfo>), typeof(MessageListener), new UIPropertyMetadata(null));

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private MessageListener()
        {
            this.MessageList = new ObservableCollection<MessageInfo>();
            this.SelectedMessage = null;
            this.showWorkingThread = new Thread(new ThreadStart(this.ShowWorkingOperation));
            this.showWorkingThread.IsBackground = true;
            this.showWorkingThread.Start();
        }

        public static MessageListener Instance
        {
            get
            {
                if (MessageListener.mInstance == null)
                {
                    MessageListener.mInstance = new MessageListener();
                }
                return MessageListener.mInstance;
            }
        }

        private void ShowWorkingOperation()
        {
        }

        public void ReceiveMessage(string message, MessageType type = MessageType.Statement)
        {
            this.Message = message;
            this.newMessage = message;
            this.receivedNewMessage = true;
            MessageInfo info = new MessageInfo(message, type);
            this.AddToList(info);
            Debug.WriteLine(this.Message);
            DispatcherHelper.DoEvents();
        }

        public void UpdateMessage(string info)
        {
            Action action = delegate ()
            {
                this.Message = info;
            };
            if (base.Dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                base.Dispatcher.BeginInvoke(action, new object[]
                {
                    ThreadPriority.AboveNormal
                });
            }
        }

        public void AddToList(MessageInfo info)
        {
            Action action = delegate ()
            {
                this.MessageList.Add(info);
                this.SelectedMessage = info;
                this.listCount = this.MessageList.Count;
            };
            if (base.Dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                base.Dispatcher.BeginInvoke(action, new object[0]);
            }
        }
        
        public void ClearList()
        {
            Action action = delegate ()
            {
                this.MessageList.Clear();
                this.SelectedMessage = null;
                this.listCount = this.MessageList.Count;
            };
            if (base.Dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                base.Dispatcher.BeginInvoke(action, new object[0]);
            }
        }

        public ObservableCollection<MessageInfo> MessageList
        {
            get
            {
                return (ObservableCollection<MessageInfo>)base.GetValue(MessageListener.MessageListProperty);
            }
            set
            {
                base.SetValue(MessageListener.MessageListProperty, value);
            }
        }

        public MessageInfo SelectedMessage
        {
            get
            {
                return (MessageInfo)base.GetValue(MessageListener.SelectedMessageProperty);
            }
            set
            {
                base.SetValue(MessageListener.SelectedMessageProperty, value);
                this.OnPropertyChanged("SelectedMessage");
            }
        }

        public string Message
        {
            get
            {
                return (string)base.GetValue(MessageListener.MessageProperty);
            }
            set
            {
                base.SetValue(MessageListener.MessageProperty, value);
            }
        }
    }

    public class MessageInfo
    {
        public string Message { get; set; }
        public MessageType MessageType { get; set; }

        public MessageInfo(string msg, MessageType type)
        {
            this.Message = msg;
            this.MessageType = type;
        }

        public MessageInfo(string msg)
        {
            this.Message = msg;
            this.MessageType = MessageType.Statement;
        }
    }
}
