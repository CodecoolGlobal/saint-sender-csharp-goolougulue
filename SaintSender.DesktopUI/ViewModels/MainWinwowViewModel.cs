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
    class MainWinwowViewModel
    {
            
        private GreetService service = new GreetService();

        public ICommand GreetCommand { get; private set; }
        public ICommand SampleCommand { get; private set; }


        public MainWinwowViewModel()
        {
            GreetCommand = new Commands((obj) => { return true; }, (obj) => { Greet();  });
            SampleCommand = new Commands((obj) => { return File.Exists("/token.json/Google.Apis.Auth.OAuth2.Responses.TokenResponse-user"); }, (obj) => { /*service.Sample();*/ });
        }

        public void Greet()
        {
            Task task = Task.Factory.StartNew(() => service.Greet("Janos"));
        }
    }
}
