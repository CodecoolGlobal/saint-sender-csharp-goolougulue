using SaintSender.Core.Services;
using SaintSender.DesktopUI.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SaintSender.DesktopUI.ViewModels
{
    class MailSenderViewModel
    {
        private GreetService _service = new GreetService(); 

        public ICommand SendCommand { get; private set; }

        public string To { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }


        public MailSenderViewModel()
        {
            To = Subject = Message = "";
            SendCommand = new Commands(
                (obj) =>
                {
                    if (To != "" && Subject != "" && Message != "")
                    {
                        return true;
                    }
                    return false;
                },
                (obj) =>
                {
                    Console.WriteLine("objects:");
                    Console.WriteLine(obj);
                });
        }
    }
}
