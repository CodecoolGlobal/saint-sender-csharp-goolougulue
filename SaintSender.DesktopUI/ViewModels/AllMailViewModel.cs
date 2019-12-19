using Google.Apis.Gmail.v1.Data;
using SaintSender.Core.Entities;
using SaintSender.Core.Services;
using SaintSender.DesktopUI.Command;
using SaintSender.DesktopUI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace SaintSender.DesktopUI.ViewModels
{
    public class AllMailViewModel
    {
        public ICommand SaveEmails { get; private set; }
        public ObservableCollection<Message> Mails { get; private set; }
        public IList<Label> Folders { get; private set; }
        public GreetService Service { get; private set; }
        public ICommand GetMailsCommand { get; private set; }
        public ICommand OpenSend { get; private set; }

        public object _itemsLock = new object();
        System.Threading.Thread emailChecker;

        public AllMailViewModel()
        {
            Service = new GreetService();
            Folders = ShowFolders();
            Mails = new ObservableCollection<Message>();
            BindingOperations.EnableCollectionSynchronization(Mails, _itemsLock);
            Task initialize = Task.Factory.StartNew(() => initializer());

        }

        private void initializer()
        {
            Task getMails = Task.Factory.StartNew(() => ShowMails());

            SaveEmails = new Commands(
                (obj) => { return true; },
                (obj) => Mails.ToList().ForEach((item) => SaveMails(item))
                );
            GetMailsCommand = new Commands((obj) => { return true; }, (obj) => {
                new GreetService();
            });
            OpenSend = new Commands((obj) => { return true; }, (obj) =>
            {
                MailSender sender = new MailSender();
                sender.Show();
            });
            getMails.Wait();
            emailChecker = new System.Threading.Thread(new ThreadStart(RefreshEmails));
            emailChecker.Start();
        }

        private void RefreshEmails()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(2000);
                ObservableCollection<Message> msgs = new ObservableCollection<Message>();
                Service.GetMails("INBOX", msgs, _itemsLock);
                List<Message> difference = msgs.Except(Mails,new SizesComparer()).ToList();
                foreach (var item in difference)
                {
                    Mails.Insert(0, item);
                }
            }

        }

        public IList<Label> ShowFolders()
        {
            return Service.GetMailFolder();
        }

        public void ShowMails()
        {
            Service.GetMails("INBOX", Mails, _itemsLock);
        }

        public async void SaveMails(Message item)
        {
            await Task.Run(() => MessageSerializer.SerializeMessage(item));
        }


        public class SizesComparer : IEqualityComparer<Message>
        {
            bool IEqualityComparer<Message>.Equals(Message x, Message y)
            {
                return (x.Id.Equals(y.Id) && x.Id.Equals(y.Id));
            }

            int IEqualityComparer<Message>.GetHashCode(Message obj)
            {
                if (Object.ReferenceEquals(obj, null))
                    return 0;

                return -1;
            }
        }
    }
}
