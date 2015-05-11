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
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace Chat_Client_Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        
        static public TcpClient user = new TcpClient();
        
        static public NetworkStream Connection()
        {

            user = new TcpClient();
            user.Connect("192.168.100.10", 8080);

            NetworkStream userConnection = user.GetStream();
           
            return userConnection;
        }

        public static byte[] userBytes;

        public static string MyString = null;

        public Chat chatWindow = new Chat();
        
        public MainWindow()
        {
            InitializeComponent();
           
            

            if (File.Exists("info.txt") == false)
            {
                FileStream fs = File.Create("info.txt");
                fs.Close();
                StreamWriter writer = new StreamWriter("info.txt");
                writer.WriteLine("");
                writer.WriteLine("off");
                writer.Close();
            }

                StreamReader options = new StreamReader("info.txt");

                string rememberCheck = File.ReadLines("info.txt").Skip(1).Take(1).First();

           
                if (rememberCheck == "on")
                {
                    remember.IsChecked = true;
                    string userPass = options.ReadLine();
                    string[] info = userPass.Split(':');
                    username.Text = info[0];
                    password.Password = info[1];
                    options.Close();
                                    
                }
                else options.Close();
        }

        
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {

            

            
        }

       

        
        private void Log_in_Click(object sender, RoutedEventArgs e)
        {
            ChatConnection();
        }

        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                ChatConnection();
            }
          }
        
        private void username_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void remember_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
          
            
        }

        public static void shutdown()
        {
            Application.Current.Shutdown();
            Process main = Process.GetCurrentProcess();
            main.Kill();
        }
         
        
        public void ChatConnection()
        {
            NetworkStream userConnection = Connection();


            byte[] usernamePassBytes = Encoding.ASCII.GetBytes("fl" + username.Text + ":" + password.Password);
            //byte[] usernameBytes = Encoding.ASCII.GetBytes(username.Text);
            Chat.usergetter(username.Text);


            byte[] respons = new byte[9];

            userConnection.Write(usernamePassBytes, 0, usernamePassBytes.Length);
            //userConnection.Write(passwordBytes, 0, passwordBytes.Length);

            userConnection.Read(respons, 0, respons.Length);
            string isValid = Encoding.ASCII.GetString(respons, 0, respons.Length);

            //byte[] function = new byte[2];
            //userConnection.Read(users, 0, users.Length);
            
                           

            if (isValid == "Connected")
            {

                if (remember.IsChecked == true)
                {
                    if (File.Exists("info.txt") == false)
                    {
                        FileStream fs = File.Create("info.txt");
                        fs.Close();
                    }
                    StreamWriter data = new StreamWriter("info.txt");
                    data.WriteLine(username.Text + ":" + password.Password);
                    data.WriteLine("on");
                    data.Close();

                }
                else if (remember.IsChecked == false)
                {

                    StreamWriter data = new StreamWriter("info.txt");
                    data.WriteLine();
                    data.WriteLine("off");
                    data.Close();

                }
                //Chat something = new Chat();
                
                chatWindow.Show();
                chatWindow.streamRead();
                //populateUsers();

                this.Close();

            }
            else
            {

                Window1 error = new Window1();
                error.Show();
                userConnection.Close();
                user.Close();

            }

        }
        private void register_Click(object sender, RoutedEventArgs e)
        {
            NetworkStream userConnection2 = Connection();
            byte[] usernamePassBytes = Encoding.ASCII.GetBytes("fr"+username.Text + ":" + password.Password);
            byte[] respons = new byte[10];
            string test = Encoding.ASCII.GetString(usernamePassBytes);
            userConnection2.Write(usernamePassBytes, 0, usernamePassBytes.Length);
            userConnection2.Read(respons, 0, respons.Length);
            string isValid = Encoding.ASCII.GetString(respons, 0, respons.Length);
            if (isValid == "Registered")
            {
                registerMsg.Text = "Registered Successfully!";
                userConnection2.Close();
            }
            else
            {
                registerMsg.Text = "Registration failed!";
                userConnection2.Close();
            }
       }

        static string usersS;
        static string[] formatUsers;
        static List<string> userList;
        
        
    
    }
}
