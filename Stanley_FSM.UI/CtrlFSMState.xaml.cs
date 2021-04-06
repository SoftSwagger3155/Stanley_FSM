using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Stanley_FSM.UI
{
    /// <summary>
    /// Interaction logic for CtrlFSMState.xaml
    /// </summary>
    public partial class CtrlFSMState : UserControl
    {
        public CtrlFSMState()
        {
            InitializeComponent();
        }

        IState fsmState = null;
        public void Setup(IState fsmState)
        {
            this.fsmState = fsmState;
            this.DataContext = fsmState;
        }
    }
}
