using Google.Apis.Gmail.v1.Data;
using SaintSender.DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SaintSender.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for OneMail.xaml
    /// </summary>
    public partial class OneMail : Window
    {
        private OneMailViewModel OneMailView;
        public OneMail(Message mailMessage) 
        {
            InitializeComponent();
            OneMailView = new OneMailViewModel(mailMessage);
            this.DataContext = OneMailView;


        }

        public OneMail()
        {
            InitializeComponent();

        }
    }
}
