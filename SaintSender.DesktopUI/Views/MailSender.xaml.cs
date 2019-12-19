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
    /// Interaction logic for MailSender.xaml
    /// </summary>
    public partial class MailSender : Window
    {
        private MailSenderViewModel _mailSender;

        public MailSender()
        {
            InitializeComponent();
            _mailSender = new MailSenderViewModel();

            this.DataContext = _mailSender;
        }

       
    }
}
