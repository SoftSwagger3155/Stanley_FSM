using System;
using System.ComponentModel;
using System.Globalization;
namespace Stanley_Utility
{
    public class YesNoPropertyType : BooleanConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
        {
            return ((bool)value) ? "Yes" : "No";
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return (string)value == "Yes";
        }
    }
}
