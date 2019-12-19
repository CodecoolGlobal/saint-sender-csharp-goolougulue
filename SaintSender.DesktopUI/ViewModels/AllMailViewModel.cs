using Google.Apis.Gmail.v1.Data;
using SaintSender.Core.Entities;
using SaintSender.Core.Services;
using SaintSender.DesktopUI.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
        public object _itemsLock = new object ();

        public AllMailViewModel()
        {
            Service = new GreetService();
            Folders = ShowFolders();
            Mails = new ObservableCollection<Message>();
            Task getMails = Task.Factory.StartNew(() => ShowMails());
            BindingOperations.EnableCollectionSynchronization(Mails, _itemsLock);
            SaveEmails = new Commands(
                (obj) => { return true; },
                (obj) => Mails.ToList().ForEach((item) => SaveMails(item))
                );
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
            await Task.Run(()=>MessageSerializer.SerializeMessage(item));
        }
        
        }
    }
