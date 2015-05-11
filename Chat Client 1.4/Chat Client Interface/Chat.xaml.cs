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
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class Chat : Window
    {




        static string privateChatUsername;
        
        public Chat()
        {
            InitializeComponent();
            chatWindow.IsReadOnly = true;
            chatWindow.Background = Brushes.White;
            
            chatWindow.Text = chatWindow.Text + "\n";
            //userName = MainWindow.userName;
            
        }
        
        
        //NetworkStream chatStream = MainWindow.user.GetStream();
        string readData = null;


       static byte[] buffer = new byte[1024];
        public void streamRead()
        {
            NetworkStream chatStream = MainWindow.user.GetStream();
            //buffer = null;
            chatStream.BeginRead(buffer, 0, buffer.Length, functionDecode, chatStream);
            
        }
             
             public void functionDecode(IAsyncResult chatStreamAR)
             {
                 NetworkStream chatStream = (NetworkStream)chatStreamAR.AsyncState;
                 chatStream.EndRead(chatStreamAR);
                 
                   
                 
                 byte[] function = new byte[2];
                 byte[] messege = new byte[1024];
                 //chatStream.Read(buffer, 0, buffer.Length);
                 System.Buffer.BlockCopy(buffer, 0, messege, 0, buffer.Length);


                //chatStream.Read(messege, 0, messege.Length);
                 function[0] = messege[0];
                function[1] = messege[1];
                messege[0] = 0;
                messege[1] = 0;
                string realMessege = Encoding.ASCII.GetString(messege);
                realMessege = realMessege.Trim('\0');
                string functionCall = Encoding.ASCII.GetString(function);
                string firstFunctionCall = functionCall.Substring(0, 1);
                string secondFunctionCall = functionCall.Substring(1, 1);
                chatStream.Flush();
                functionDirect(firstFunctionCall, secondFunctionCall, realMessege);
                realMessege = null;
                buffer = new byte[1024];
                chatStream.BeginRead(buffer, 0, buffer.Length, functionDecode, chatStream);
             }



             public void functionDirect(string firstFunctionCall , string secondFunctionCall,string realMessege)
             {
                 
                 this.Dispatcher.Invoke((Action)(() =>
                {
                 
                 switch (firstFunctionCall)
                 {
                     case "c":
                         switch (secondFunctionCall)
                         {
                             case "a":

                                 getMessage(realMessege);
                                 
                                 break;
                             case "p":
                                 string[] messegeSplit = realMessege.Split(';');
                                 string[] userSplit = messegeSplit[0].Split(':');
                                 string chatName = userSplit[0] + userSplit[1];
                                 string reversChatName = userSplit[1] + userSplit[0];
                                 string messege = messegeSplit[1];
                                 foreach (var obj in privateWindows)
                                 {
                                     if (obj.ChatName == chatName || obj.ChatName == reversChatName)
                                     {
                                         obj.getMessage(messegeSplit[1]);
                                         break;
                                     }
                                         

                                 }
                                 
                                 break;
                         }
                         break;
                     case "f":
                         switch (secondFunctionCall)
                         {
                             case "p":
                                 PrivateChatRespons(realMessege);
                                 
                                 break;
                             case "a":
                                    
                                    chatWindow.AppendText("Accepted request" + "\n");
                                    Thread openPrivateChatWindow = new Thread(() => privateChatWindowOpen(realMessege));
                                    openPrivateChatWindow.SetApartmentState(ApartmentState.STA);
                                    openPrivateChatWindow.Start();
                                 
                                 break;
                             case "z":
                                 removeUserFromList(realMessege);
                                 break;
                             case "x":
                                 addUserToList(realMessege);
                                 break;
                             case "l":
                                 populateUsers(realMessege);                     
                                 break;
                         }
                         break;
                 
                 }
                }));
             }
        
        
       public static string userString = null;

        
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
               if (encription.IsChecked == true)
               {
                   Aes cryptator = Aes.Create();

                   byte[] key = Encoding.Unicode.GetBytes(eKey.Text);
                   cryptator.Key = key;
                   cryptator.IV = key;
                   try
                   {
                       byte[] cipherBytes = Convert.FromBase64String(readData);//compensate
                       

                       MemoryStream ms1 = new MemoryStream();
                       CryptoStream cs1 = new CryptoStream(ms1, cryptator.CreateDecryptor(), CryptoStreamMode.Write);
                       cs1.Write(cipherBytes, 0, cipherBytes.Length);
                       cs1.Close();
                       string cipherText = Encoding.ASCII.GetString(ms1.ToArray());

                       readData = cipherText;
                       
                   }
                   catch { }
               }
               
                   MainWindow main = new MainWindow();
                   chatWindow.AppendText(readData + "\n");
                   chatWindow.SelectionStart = chatWindow.Text.Length;
                   chatWindow.ScrollToEnd();
                }));
               
           
          
        }


        public static string respons = null;

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            
        }

        private void chatbox_MouseEnter(object sender, MouseEventArgs e)
        {
            if(chatbox.Text == "Type here..." || chatbox.Text == "Type Here...")chatbox.Text = "";
        }

        private void chatbox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (chatbox.Text == "") chatbox.Text = "Type here...";
        }


        

        private void chatbox_KeyDown(object sender, KeyEventArgs e)
        {
            
            if(e.Key == Key.Return)
            {
                if (encription.IsChecked == true)
                {
                    NetworkStream chatStream = MainWindow.user.GetStream();
                    byte[] eChatMsg = Encoding.ASCII.GetBytes(chatbox.Text);
                    MainWindow main = new MainWindow();
                         
                    byte[] username = Encoding.ASCII.GetBytes(proxyusername);
                    byte[] spacer = new byte[2]; spacer = Encoding.ASCII.GetBytes(": ");
                    
                    byte[] copy = new byte[eChatMsg.Length + 2 + username.Length];
                    System.Buffer.BlockCopy(username, 0, copy, 0, username.Length);
                    System.Buffer.BlockCopy(spacer, 0, copy, username.Length, spacer.Length);
                    System.Buffer.BlockCopy(eChatMsg, 0, copy, username.Length + spacer.Length, eChatMsg.Length);
                    Aes cryptator = Aes.Create();
                    
                    byte[] key = Encoding.Unicode.GetBytes(eKey.Text);
                    cryptator.Key = key;
                    cryptator.IV = key;
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, cryptator.CreateEncryptor(), CryptoStreamMode.Write);
                    cs.Write(copy, 0, copy.Length);
                    cs.Close();
                    string text = Convert.ToBase64String(ms.ToArray());
                    byte[] encodedMsg = Encoding.ASCII.GetBytes("ca"+text);
                    chatStream.Write(encodedMsg, 0, encodedMsg.Length);
                    chatbox.Text = "";
                    chatStream.Flush();
                }
                else
                {

                    NetworkStream chatStream = MainWindow.user.GetStream();
                    byte[] username = Encoding.ASCII.GetBytes("ca"+proxyusername);
                    byte[] spacer = new byte[2]; spacer = Encoding.ASCII.GetBytes(": ");
                    byte[] chatMsg = Encoding.ASCII.GetBytes(chatbox.Text);
                    byte[] copy = new byte[chatMsg.Length + 2 + username.Length];
                    
                    System.Buffer.BlockCopy(username, 0, copy, 0, username.Length);
                    System.Buffer.BlockCopy(spacer, 0, copy, username.Length, spacer.Length);
                    System.Buffer.BlockCopy(chatMsg, 0, copy, username.Length + spacer.Length, chatMsg.Length);
                    
                    chatStream.Write(copy, 0, copy.Length);

                    chatbox.Text = "";
                    chatStream.Flush();
                }
            }
        }

        private void Window_Closed_1(object sender, EventArgs e)
        {
            MainWindow main = new MainWindow();
            
            MainWindow.shutdown();
            Application.Current.Shutdown();
        }

        private void encription_Checked(object sender, RoutedEventArgs e)
        {
            if (eKey.Text.Length != 8)
            {
                keyError.Text = "Wrong Key!!!";
                encription.IsChecked = false;
            }
            else if (eKey.Text.Length == 8)
            {
                keyError.Text = "";
                encription.IsChecked = true;
            }
        }

        private void eKey_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    
        public static string proxyusername;
        public static string usergetter(string userName)     
        {
            proxyusername = userName;
            return userName;
        }

        public void SendPrivateChat(object sender, System.EventArgs e)
        {
            NetworkStream chatStream = MainWindow.user.GetStream();
            MenuItem privatechat = (MenuItem)sender;
            string userName = "fp"+(String)privatechat.Tag;
            byte[] userNameBytes = new byte[userName.Length];
            userNameBytes = Encoding.ASCII.GetBytes(userName);
            chatStream.Write(userNameBytes, 0, userNameBytes.Length);
            
        }
        static public void PrivateChatResponsHandleAccept()
        {
            NetworkStream chatStream = MainWindow.user.GetStream();
            string realRespons = "fa" + privateChatUsername;
            Thread openPrivateChatWindow = new Thread(() => privateChatWindowOpen(privateChatUsername));
            openPrivateChatWindow.SetApartmentState(ApartmentState.STA);
            openPrivateChatWindow.Start();
            byte[] responsToSend = Encoding.ASCII.GetBytes(realRespons);
            chatStream.Write(responsToSend,0,responsToSend.Length);
            respons = null;
        }
        static public void PrivateChatResponsHandleDecline()
        {
            NetworkStream chatStream = MainWindow.user.GetStream();
            string realRespons = "fd" + privateChatUsername;
            byte[] responsToSend = Encoding.ASCII.GetBytes(realRespons);
            chatStream.Write(responsToSend, 0, responsToSend.Length);
            respons = null;
        }

        public static List<privateChatWindow> privateWindows = new List<privateChatWindow>();
        public void PrivateChatRespons(string username)
        {
            NetworkStream chatStream = MainWindow.user.GetStream();

            
            Thread openPrivateChatWindow = new Thread(openPrivateChatRespons);
            openPrivateChatWindow.SetApartmentState(ApartmentState.STA);
            openPrivateChatWindow.Start();
            
            
            
            
            privateChatUsername = username;
        }
        public void openPrivateChatRespons()
        {
            privateChatRespons window = new privateChatRespons();
            
            window.ShowDialog();
        }

        static public void privateChatWindowOpen(string secondUser)
        {
            privateChatWindow obj = new privateChatWindow();
            obj.FirstUser = proxyusername;
            obj.SecondUser = secondUser;
            obj.ChatName = obj.FirstUser + obj.SecondUser;

            privateWindows.Add(obj);
            obj.ShowDialog();

        }

        public void removeUserFromList(string user)
        {
            for (int n = listbox.Items.Count - 1; n >= 0; --n)
            {
                 
                TextBlock userString = (TextBlock)listbox.Items[n];
                string removelistitem = user + "         ";
                if (userString.Text == removelistitem)
                {
                   listbox.Items.RemoveAt(n);
                }
            }    
        }
        public void addUserToList(string user)
        {
            MenuItem menuItem1 = new MenuItem();
            //ContextMenu cs = new ContextMenu();
            menuItem1.Header = "Private Chat";
            menuItem1.Click += new RoutedEventHandler(SendPrivateChat);
            ContextMenu cs = new ContextMenu();
            menuItem1.Tag = user;
            TextBlock newUser = new TextBlock();
            newUser.Name = user;
            newUser.Text = user + "         ";

            cs.Items.Add(menuItem1);
            newUser.ContextMenu = cs;
            listbox.Items.Add(newUser);
        }

        public void populateUsers(string user)
        {


            string usersS = user;
            usersS = usersS.Trim('\0');
            string[] formatUsers = usersS.Split(':');
            

            List<string> userList = new List<string>(formatUsers);





            foreach (var obj in userList)
            {
                MenuItem menuItem1 = new MenuItem();
                //ContextMenu cs = new ContextMenu();
                menuItem1.Header = "Private Chat";
                menuItem1.Click += new RoutedEventHandler(SendPrivateChat);
                ContextMenu cs = new ContextMenu();
                menuItem1.Tag = obj;
                TextBlock newUser = new TextBlock();
                newUser.Name = obj;
                newUser.Text = obj + "         ";

                cs.Items.Add(menuItem1);
                newUser.ContextMenu = cs;
                listbox.Items.Add(newUser);

            }
            int count = listbox.Items.Count;
            listbox.Items.RemoveAt(count - 1);
        }

    }
    
    
    }
