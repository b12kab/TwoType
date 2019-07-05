using System;
using System.Globalization;
using Xamarin.Forms;

namespace TwoTypeExample.Converter
{
    public sealed class ConverterDateToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string returnDateText;

            // This will only be used to change a date over to a string value of that date
            if (value is DateTime)
            {
                DateTime myDate = (DateTime)value;
                if (value == null)
                {
                    returnDateText = "NA";
                }
                else // There is a value to check now
                {
                    if (myDate == DateTime.MinValue ||
                        myDate == DateTime.MaxValue)
                    {
                        returnDateText = "Unset";
                    }
                    else
                    {
                        returnDateText = myDate.ToLocalTime().ToString(culture.DateTimeFormat.ShortDatePattern) + " " +
                                         myDate.ToLocalTime().ToString(culture.DateTimeFormat.ShortTimePattern);
                    }

                }
            }
            else
            {
                returnDateText = "Not a date";
            }

            return returnDateText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }

}
