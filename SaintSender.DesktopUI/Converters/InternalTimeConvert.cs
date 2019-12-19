using Google.Apis.Gmail.v1.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SaintSender.DesktopUI.Converters
{
    public class InternalTimeConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var dateTime = ((long)value);
            

            var convertTime = DateTimeOffset.FromUnixTimeMilliseconds(dateTime).DateTime;
            return TimeZoneInfo.ConvertTimeToUtc(convertTime);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
