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
using System.Net.Sockets;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

namespace Chat_Client_Interface
{
    /// <summary>
    /// Interaction logic for privateChatWindow.xaml
    /// </summary>
    public partial class privateChatWindow : Window
    {
        public privateChatWindow()
        {
            InitializeComponent();
            chatWindow.Text = chatWindow.Text+"\n";
        }

        private string firstUser = null;
        private string secondUser = null;
        private string chatName = null;
        public string FirstUser
        {
            get { return firstUser; }
            set { firstUser = value; }

        }
        public string SecondUser
        {
            get { return secondUser; }
            set { secondUser = value; }

        }

        public string ChatName
        {
            get { return firstUser + secondUser; }
            set { chatName = value; }
        }

        private void chatWindow_KeyDown(object sender, KeyEventArgs e)
        {
        }
        public string readData = null;
        public void getMessage(string messege)
        {

            string returndata = messege;
            readData = "" + returndata;
            msg();

        }

        private void msg()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
             
                MainWindow main = new MainWindow();
                chatWindow.AppendText(readData + "\n");
                chatWindow.SelectionStart = chatWindow.Text.Length;
                chatWindow.ScrollToEnd();
            }));



        }

        private void chatbox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Return)
            {
                NetworkStream chatStream = MainWindow.user.GetStream();
                byte[] username = Encoding.ASCII.GetBytes("cp" + firstUser + ":" + secondUser+"; "+FirstUser+":");
               
                byte[] chatMsg = Encoding.ASCII.GetBytes(chatbox.Text);
                byte[] copy = new byte[chatMsg.Length + 2 + username.Length];

                System.Buffer.BlockCopy(username, 0, copy, 0, username.Length);
                
                System.Buffer.BlockCopy(chatMsg, 0, copy, username.Length, chatMsg.Length);

                chatStream.Write(copy, 0, copy.Length);

                chatbox.Text = "";
                chatStream.Flush();
            }
        }

        private void chatbox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (chatbox.Text == "Type here...") chatbox.Text = "";
        }

        private void chatbox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (chatbox.Text == "") chatbox.Text = "Type here...";
        }


    }
}
