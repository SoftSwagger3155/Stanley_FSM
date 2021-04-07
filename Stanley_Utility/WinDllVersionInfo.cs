using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Stanley_Utility
{
    public class WinDllVersionInfo : Window, IComponentConnector
    {
        public WinDllVersionInfo(DllFileInfo[] info, string title)
        {
            this.InitializeComponent();
            this.info = info;
            base.Title = title;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ListBoxFileInfo.ItemsSource = this.info;
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/Utilities;component/windllversioninfo.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    ((WinDllVersionInfo)target).Loaded += this.Window_Loaded;
                    break;
                case 2:
                    this.ListBoxFileInfo = (ListBox)target;
                    break;
                default:
                    this._contentLoaded = true;
                    break;
            }
        }

        private DllFileInfo[] info = null;

        internal ListBox ListBoxFileInfo;

        private bool _contentLoaded;
    }
}
