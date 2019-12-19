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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SaintSender.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for AllMailList.xaml
    /// </summary>
    public partial class AllMailList : UserControl
    {


        public AllMailList()
        {
            InitializeComponent();


        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var mails = (ListView)sender;
            OneMail selectedMailWindow = new OneMail((Message)mails.SelectedItem);
            selectedMailWindow.Show();
        }
    }
}
