using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Stanley_Utility
{
    public class WinObjectEditor : Window, IComponentConnector
    {
        public WinObjectEditor()
        {
            this.InitializeComponent();
        }

        public void Setup(FilterablePropertyBase[] objArr)
        {
            this.skipEvent = true;
            this.objArr = objArr;
            this.CbObjSelection.ItemsSource = objArr;
            if (objArr != null && objArr.Length > 0)
            {
                this.CbObjSelection.SelectedIndex = 0;
                this.AssignSelectedObj();
            }
            this.skipEvent = false;
        }

        private void AssignSelectedObj()
        {
            this.ProGrdConfig.SelectedObject = this.CbObjSelection.SelectedItem;
        }

        private void CbObjSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this.skipEvent && this.CbObjSelection.SelectedItem != null)
            {
                this.AssignSelectedObj();
            }
        }

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/Utilities;component/winobjecteditor.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        [DebuggerNonUserCode]
        internal Delegate _CreateDelegate(Type delegateType, string handler)
        {
            return Delegate.CreateDelegate(delegateType, this, handler);
        }

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.ProGrdConfig = (WpfPropertyGrid)target;
                    break;
                case 2:
                    this.CbObjSelection = (ComboBox)target;
                    this.CbObjSelection.SelectionChanged += this.CbObjSelection_SelectionChanged;
                    break;
                default:
                    this._contentLoaded = true;
                    break;
            }
        }

        private bool skipEvent = false;
        private FilterablePropertyBase[] objArr = null;
        internal WpfPropertyGrid ProGrdConfig;
        internal ComboBox CbObjSelection;
        private bool _contentLoaded;
    }
}
