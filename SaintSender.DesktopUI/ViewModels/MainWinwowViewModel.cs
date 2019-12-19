using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SaintSender.Core.Services;
using SaintSender.DesktopUI.Command;

namespace SaintSender.DesktopUI.ViewModels
{
    public class MainWinwowViewModel
    {
        public ICommand GreetCommand { get; private set; }

        public MainWinwowViewModel()
        {
            GreetCommand = new Commands((obj) => { return true; }, (obj) => { new GreetService();  });
        }
    }
}
