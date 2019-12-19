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
    class MessageConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var message = (MessagePart)value;
            var messageBody = message.Body.Data;
            string decodedString = "";
            IList<MessagePart> messageBodyParts;

            
            if (messageBody != null)
            {
                decodedString = Decode(messageBody);


                return decodedString;
            }

            messageBodyParts = message.Parts;
            foreach (var messagePart in messageBodyParts)
            {
                if (messagePart.Body.Data != null)
                {
                    decodedString += Decode(messagePart.Body.Data);
                }
                decodedString.Append('\n');
            }
            Console.WriteLine(decodedString);

            return decodedString;



        }

        private string Decode(string code)
        {
            byte[] bytes = System.Convert.FromBase64String(code.Replace('-', '+').Replace('_', '/').PadRight(4 * ((code.Length + 3) / 4), '='));
            return Encoding.UTF8.GetString(bytes);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
