using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.Core.Entities
{
    public class CostumMail
    {
        public CostumMail()
        {

        }
        public CostumMail(string from, string to)
        {

        }
        public CostumMail(string from, string to, string subject, string message)
        {
            Body = message;
            FromAddress = from;
            ToRecipients = to;
            Subject = subject;
            IsHtml = false;
            Attachments = new List<Attachment>();
        }


        public IEnumerable<Attachment> Attachments { get; internal set; }
        public bool IsHtml { get; internal set; }
        public string Body { get; internal set; }
        public string Subject { get; internal set; }
        public string FromAddress { get; internal set; }
        public string ToRecipients { get; internal set; }
    }
}
