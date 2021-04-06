using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Stanley_FSM
{
    [ValueConversion(typeof(FSMStateStatus), typeof(SolidColorBrush))]
    public class FSMStateStatusToBrushConveter : IValueConverter
    {
        static SolidColorBrush IdleBrush = Brushes.LightGray;
        static SolidColorBrush DoneBrush = Brushes.Green;
        static SolidColorBrush OnRunBrush = Brushes.Orange;
        static SolidColorBrush ErrorBrush = Brushes.Red;

        public object Convert(object value, Type targetType,
        object parameter, System.Globalization.CultureInfo culture)
        {
            FSMStateStatus b = (FSMStateStatus)value;
            SolidColorBrush rgb = IdleBrush;
            switch (b)
            {
                case FSMStateStatus.Idle:
                    rgb = IdleBrush;
                    break;
                case FSMStateStatus.Running:
                    rgb = OnRunBrush;
                    break;
                case FSMStateStatus.Error:
                    rgb = ErrorBrush;
                    break;
                case FSMStateStatus.Done:
                    rgb = DoneBrush;
                    break;
                default:
                    break;
            }
            return rgb;
        }
        public object ConvertBack(object value, Type targetType,
        object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
