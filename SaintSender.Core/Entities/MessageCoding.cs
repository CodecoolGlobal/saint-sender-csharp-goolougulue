using Google.Apis.Gmail.v1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.Core.Entities
{
    public class MessageCoding
    {


        public string DecodeMessageBody(Message message)
        {
            var messageBody = message.Payload.Body.Data;
            string decodedString = "";
            IList<MessagePart> messageBodyParts;

            if (messageBody != null)
            {
                decodedString = Decode(messageBody);


                return decodedString;
            }

            messageBodyParts = message.Payload.Parts;
            foreach (var messagePart in messageBodyParts)
            {
                if (messagePart.Body.Data != null)
                {
                    decodedString += Decode(messagePart.Body.Data);
                }
                decodedString.Append('\n');
            }

            return decodedString;

        }

        private string Decode(string text)
        {
            byte[] bytes = Convert.FromBase64String(
                text.Replace('-', '+')
                    .Replace('_', '/')
                    .PadRight(4 * ((text.Length + 3) / 4), '=')
                    );
            return Encoding.UTF8.GetString(bytes);
        }

        public string Encode(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);

            return Convert.ToBase64String(bytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");
        }
    }
}
