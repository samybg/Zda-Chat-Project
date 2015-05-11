using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;

namespace servertest
{
	class Login
	{
		private string username;
		private string password;
		public static List<Login> LoginInformationList = new List<Login>();
		
		public string Username
		{
			get { return username; }
			set { username = value; }
		}

		public string Password
		{
			get { return password; }
			set { password = value; }
		}

		public Login(string Uname,string Pass)
		{
			Username = Uname;
			Password = Pass;
		}

		static StreamReader reader = new StreamReader("Information.txt", Encoding.ASCII);
		public static void FillLoginInformationList()
		{
			try
			{
				string Line = null;
				
				do
				{
					Line = reader.ReadLine();
					if (Line == null) { break; };
					string[] Information = Line.Split(':');
					LoginInformationList.Add(new Login(Information[0], Information[1]));
				} while (Line != null);
				reader.Close();
			}
			catch {};
			reader.Close();
		}



		public static bool addingUser = false;
        public static bool iscorrect=false;
		public static void LoginCheck (User UserToCheck)
		{
			addingUser = true;
            string LoginInfoString = "";
            byte[] LoginInfoByte = new byte[1024];
			System.Buffer.BlockCopy(UserToCheck.streamCatcher,2, LoginInfoByte, 0, 100);
			LoginInfoString = Encoding.ASCII.GetString(LoginInfoByte);
            string[] Information = new string[2];
            Information = LoginInfoString.Split(':');

            string userName = Information[0]; string passWord = Information[1].Trim('\0');

            
            try
            {
                Login LoginToCheck = LoginInformationList.Single(l => l.username == userName);
                iscorrect = LoginInformationList.Contains(LoginToCheck) && passWord == LoginToCheck.Password;
            }
            catch { }
            bool iscorrect1 = iscorrect;
            if (iscorrect == false)
            {
                byte[] RejectMessage = Encoding.ASCII.GetBytes("Wrong Info");
                UserToCheck.stream.Write(RejectMessage, 0, RejectMessage.Length);
				Console.WriteLine("User disconnected due to wrong login info!");
                User.UserList.Remove(UserToCheck);
                UserToCheck.stream.Close();
                UserToCheck.soc.Close();
            }
            else
            {
				Console.WriteLine("User {0} connected!",userName);
                UserToCheck.userName = Encoding.ASCII.GetBytes(userName);
				UserToCheck.userNameString = userName;
                byte[] copy = new byte[1024];
                copy.CopyTo(UserToCheck.streamCatcher, 0);
            }

            iscorrect = false;
            LoginInfoString = "";
            LoginInfoByte = new byte[1024];
			addingUser = false;
		}


		public static void RegisterUser(User UserToRegister) 
		{
			string RegisterInfoString = Encoding.ASCII.GetString(UserToRegister.streamCatcher, 2, 100);

			RegisterInfoString = RegisterInfoString.Trim('\0');
			string[] Information = new string[2];
			Information = RegisterInfoString.Split(':');
			string userName = Information[0];
			string passWord = Information[1];
			
			Login NameTaken =null;
			try
			{
				NameTaken = LoginInformationList.SingleOrDefault(u => u.Username == userName);
			}
			catch { }
			if (NameTaken == null)
			{
				string content;
				StreamReader sr = new StreamReader("Information.txt");
				content = sr.ReadToEnd();
				sr.Close();
				StreamWriter sw = new StreamWriter("Information.txt");
				sw.WriteLine(userName + ":" + passWord);
				sw.Write(content);
				sw.Close();

				Login.LoginInformationList.Add(new Login(userName, passWord));
				Console.WriteLine("User {0} created!", userName);
				UserToRegister.stream.Write(Encoding.ASCII.GetBytes("Registered"), 0, 10);
				User.RemoveUser(UserToRegister.soc);
			}
			else 
			{
				UserToRegister.stream.Write(Encoding.ASCII.GetBytes("Failed"), 0, 7);
				User.RemoveUser(UserToRegister.soc);
			}


		}


		/*public static void TestInformationList(List<Login> InfoList)
		{
			foreach (Login login in InfoList)
			{
				Console.WriteLine(login.Username);
				Console.WriteLine(login.Password);
			}
		}*/




	}
}
