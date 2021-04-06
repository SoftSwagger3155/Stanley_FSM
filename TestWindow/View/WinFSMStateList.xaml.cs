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
using System.Windows.Shapes;

namespace Test_FSM_WPF.View
{
    /// <summary>
    /// Interaction logic for WinFSMStateList.xaml
    /// </summary>
    public partial class WinFSMStateList : Window
    {
        public WinFSMStateList()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            List<CtrlFSMState> states = new List<CtrlFSMState>
            {
                new CtrlFSMState(){ Margin = new Thickness(10)},
                new CtrlFSMState(){ Margin = new Thickness(10)},
                new CtrlFSMState(){ Margin = new Thickness(10)},
                new CtrlFSMState(){ Margin = new Thickness(10)},
                new CtrlFSMState(){ Margin = new Thickness(10)},
                new CtrlFSMState(){ Margin = new Thickness(10)},
                new CtrlFSMState(){ Margin = new Thickness(10)},
                new CtrlFSMState(){ Margin = new Thickness(10)},
                new CtrlFSMState(){ Margin = new Thickness(10)},
                new CtrlFSMState(){ Margin = new Thickness(10)},
            };

            int noOfCol = (int)Math.Ceiling(states.Count / 4f);

            for (int i = 0; i < noOfCol; i++)
            {
                _grd_Screen.ColumnDefinitions.Add(new ColumnDefinition() { MaxWidth = 245, MinWidth = 245});
            }

            for (int i = 0; i < noOfCol; i++)
            {
                ScrollViewer slv = new ScrollViewer();
                StackPanel sp = new StackPanel();
                slv.Content = sp;

                slv.HorizontalAlignment = HorizontalAlignment.Stretch;
                slv.VerticalAlignment = VerticalAlignment.Stretch;
             

                Grid.SetRow(slv, 0);
                Grid.SetColumn(slv, i);

                _grd_Screen.Children.Add(slv);

                List<CtrlFSMState> templist = GetList(states, i, 4).ToList();

                for (int j = 0; j < templist.Count; j++)
                {
                    sp.Children.Add(templist[j]);
                }
            }
        }

        IEnumerable<T>GetList<T>(IEnumerable<T> tempList, int i, int count)
        {
            int tolCount = tempList.ToList().Count;
            bool isOverFlow = tolCount < (i + 1) * count;

            int finalCount = isOverFlow ? count - ((i + 1) * count - tolCount) : count;

            IEnumerable<T> newList = tempList.ToList().GetRange((i) * count, finalCount);
            return newList;
        }
        
       
    }
}
