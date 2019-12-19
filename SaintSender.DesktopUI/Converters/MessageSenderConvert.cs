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
    class MessageSenderConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var msgPart = (MessagePart)value;
            var fromHeader = msgPart.Headers;

            var item = fromHeader.FirstOrDefault(header => header.Name == "From" || header.Name == "from");

            return item.Value;


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
