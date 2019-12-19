using SaintSender.Core.Entities;
using SaintSender.Core.Services;
using SaintSender.DesktopUI.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SaintSender.DesktopUI.ViewModels
{
    class MailSenderViewModel : INotifyPropertyChanged
    {
        private GreetService _service = new GreetService();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string text)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(text));
            }
        }

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
                    SendMail();
                    CloseWindow((Window)obj);
                });
        }

        private void CloseWindow(Window mainWindow)
        {
            mainWindow.Close();
        }

        public void SendMail()
        {
            GreetService service = new GreetService();
            var from = service.UserMailAddress();
            service.SendEmail(new CostumMail(from, To, Subject, Message));
        }
    }
}
