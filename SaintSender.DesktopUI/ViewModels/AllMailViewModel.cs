using Google.Apis.Gmail.v1.Data;
using SaintSender.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.DesktopUI.ViewModels
{
    public class AllMailViewModel
    {
        public IList<Label> Folders { get; private set; }
        public GreetService Service { get; private set; }

        public AllMailViewModel()
        {
            Service = new GreetService();
            Folders = ShowFolders();
        }

        public IList<Label> ShowFolders()
        {
            return Service.GetMailFolder();
        }
    }
}
