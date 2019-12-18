using Google.Apis.Gmail.v1.Data;
using SaintSender.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SaintSender.DesktopUI.ViewModels
{
    public class AllMailViewModel
    {
        public ObservableCollection<Message> Mails { get; private set; }
        public IList<Label> Folders { get; private set; }
        public GreetService Service { get; private set; }
        public object _itemsLock = new object ();

        public AllMailViewModel()
        {
            Service = new GreetService();
            Folders = ShowFolders();
            Mails = new ObservableCollection<Message>();
            Task getMails = Task.Factory.StartNew(()=>ShowMails());

            BindingOperations.EnableCollectionSynchronization(Mails, _itemsLock);

        }    


        public IList<Label> ShowFolders()
        {
            return Service.GetMailFolder();
        }

        public void ShowMails()
        {
            Service.GetMails("INBOX", Mails, _itemsLock);
            foreach (var item in Mails)
            {
                Console.WriteLine(item.Id);
            }

        } 
    }
}
