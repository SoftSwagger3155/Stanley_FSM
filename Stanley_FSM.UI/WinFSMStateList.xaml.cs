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
    /// Interaction logic for WinFSMStateList.xaml
    /// </summary>
    public partial class WinFSMStateList : Window
    {
        public WinFSMStateList()
        {
            InitializeComponent();
        }

        int boxWidth = 245;
        int boxHeight = 150;
        int oneLineCount = 4;
        List<CtrlFSMState> states = null;
       public void Setup(List<CtrlFSMState> states)
        {
            this.states = states;
            UpdateUI(states);
        }

        private void UpdateUI(List<CtrlFSMState> states)
        {
            int noOfCol = (int)Math.Ceiling(states.Count / 4f);

            for (int i = 0; i < noOfCol; i++)
            {
                _grd_Screen.ColumnDefinitions.Add(new ColumnDefinition() { MaxWidth = boxWidth, MinWidth = boxWidth });
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

                List<CtrlFSMState> templist = GetList(states, i, oneLineCount).ToList();

                for (int j = 0; j < templist.Count; j++)
                {
                    sp.Children.Add(templist[j]);
                }
            }

            this.Width = noOfCol * boxWidth + 10;
            this.Height = boxHeight * oneLineCount + 10;
        }

        IEnumerable<T> GetList<T>(IEnumerable<T> tempList, int i, int count)
        {
            int tolCount = tempList.ToList().Count;
            bool isOverFlow = tolCount < (i + 1) * count;

            int finalCount = isOverFlow ? count - ((i + 1) * count - tolCount) : count;

            IEnumerable<T> newList = tempList.ToList().GetRange((i) * count, finalCount);
            return newList;
        }
    }
}
