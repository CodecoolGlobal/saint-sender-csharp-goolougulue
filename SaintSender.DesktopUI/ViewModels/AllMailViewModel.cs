﻿using Google.Apis.Gmail.v1.Data;
using SaintSender.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.DesktopUI.ViewModels
{
    public class AllMailViewModel
    {
        public ObservableCollection<Message> Mails { get; private set; }
        public IList<Label> Folders { get; private set; }
        public GreetService Service { get; private set; }

        public AllMailViewModel()
        {
            Service = new GreetService();
            Folders = ShowFolders();
            //Task getMails = Task.Factory.StartNew(() => ShowMails());
            GetEMails();
        }    

        private async Task GetEMails()
        {
            await ShowMails();
        }

        public IList<Label> ShowFolders()
        {
            return Service.GetMailFolder();
        }

        public Task ShowMails()
        {
            Service.GetMails("INBOX", Mails);
            foreach (var item in Mails)
            {
                Console.WriteLine(item.Id);
            }
            return null;
        } 
    }
}
