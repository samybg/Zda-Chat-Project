using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace servertest
{
    class User
    {
        public byte[] userName;
		public string userNameString;
        public  Socket soc;
        public NetworkStream stream;
        public byte[] streamCatcher;
        public static int userCount = 0;
        public static List<User> UserList = new List<User>();


        public User(Socket Soc)
        {
            soc = Soc;
            streamCatcher = new byte[1024];
            stream = new NetworkStream(Soc);
            userCount++;
        }


        static public void AddUser(Socket soc)
        {
                UserList.Add(new User (soc));
                try
                {
                    User UserToCheck = UserList.Single(u => u.soc == soc);
                    UserToCheck.stream.Read(UserToCheck.streamCatcher, 0,100);
					string identifier1 = Encoding.ASCII.GetString(UserToCheck.streamCatcher,0,1); //identifies wether is a function request
					string identifier2 = Encoding.ASCII.GetString(UserToCheck.streamCatcher,1,1); //identifies wether it is login or register
					if (identifier1 == "f")
					{
						if (identifier2 == "l") { Login.LoginCheck(UserToCheck); }
						if (identifier2 == "r") { Login.RegisterUser(UserToCheck); }
					}
					else {RemoveUser(UserToCheck.soc);}
                }
                catch { RemoveUser(soc); }

                try
                {
                    byte[] WelcomeMessage = Encoding.ASCII.GetBytes("Connected");
                    NetworkStream zda = new NetworkStream(soc);
                    zda.Write(WelcomeMessage,0,WelcomeMessage.Length);
					zda.Write(Encoding.ASCII.GetBytes(StringOfAllOnlineUsers()), 0, StringOfAllOnlineUsers().Length);
					zda.Flush();
					zda.Close();
                }
                catch { }

                try
                {
                    User currentUser = UserList.Single(u => u.soc == soc);
					//notify users
					byte[] connectMessage = Encoding.ASCII.GetBytes("fx" + Encoding.ASCII.GetString(currentUser.userName));
					ChatFunctions.WriteToAll(connectMessage);
					
					currentUser.stream.BeginRead(currentUser.streamCatcher, 0, 1024, ChatFunctions.Reader, currentUser.soc);
                }
                catch { }
        }

        static public void RemoveUser(Socket soc)
        {
            var UserToRemove = UserList.Single(u => u.soc == soc);
            UserToRemove.stream.Close();
            UserToRemove.soc.Close();
            UserList.Remove(UserToRemove);
        }

        static public void IsUserAlive(Socket socket)
        {
            bool bool1 = socket.Poll(1000, SelectMode.SelectRead);
            bool bool2 = (socket.Available == 0);
            if ((bool1 && bool2) )//|| !socket.Connected)
            {
				User UserToRemove = UserList.Single(u => u.soc == socket);
                Console.WriteLine("User {0} disconnected!",Encoding.ASCII.GetString(UserToRemove.userName));
                RemoveUser(socket);
				
				//notify users
				byte[] disconnectMessage = Encoding.ASCII.GetBytes("fz"+Encoding.ASCII.GetString(UserToRemove.userName));
				ChatFunctions.WriteToAll(disconnectMessage);
            }
        }
        

        static public void ListKeeper()
        {
            while (true)
            {
                try
                {
                    foreach (User user in UserList)
                    {
                        try
                        {
                            User.IsUserAlive(user.soc);
                        }
                        catch { }
                    }
                }
                catch { };
                Thread.Sleep(300);
            }
        }

		public static string StringOfAllOnlineUsers()
		{
			string result = "fl";
			foreach (User user in User.UserList)
			{
				result = result + Encoding.ASCII.GetString(user.userName) + ":";
			}
			Console.WriteLine(result);
			return result;
		}


    }
}
