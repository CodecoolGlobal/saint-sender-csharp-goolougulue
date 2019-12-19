using Google.Apis.Gmail.v1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.DesktopUI.ViewModels
{
    public class OneMailViewModel
    {
        public MessagePart From { get; set; }
        public long When { get; set; }
        
        public MessagePart Subject { get; set; }
        public MessagePart Message { get; set; }

        public OneMailViewModel(Message message)
        {
            Message = message.Payload;
            Subject = message.Payload;
            When = (long) message.InternalDate;
            From = message.Payload;

        }
    }
}
