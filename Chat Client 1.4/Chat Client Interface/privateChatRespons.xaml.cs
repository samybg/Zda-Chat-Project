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
using System.Threading;

namespace Chat_Client_Interface
{
    /// <summary>
    /// Interaction logic for privateChatRespons.xaml
    /// </summary>
    public partial class privateChatRespons : Window
    {
        public privateChatRespons()
        {
             InitializeComponent();
             
        }

       
        private void accept_Click(object sender, RoutedEventArgs e)
        {
            Chat.respons = "yes";
            Chat.PrivateChatResponsHandleAccept();
            Close();
        }

        private void no_Click(object sender, RoutedEventArgs e)
        {
            Chat.respons = "no";
            Chat.PrivateChatResponsHandleDecline();
            
            Close();
        }


    }
}
