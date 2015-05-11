using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace servertest
{
	class ServerCommand
	{
		public static void dada()
		{
			while (true)
			{
					Console.BackgroundColor = ConsoleColor.Blue;
					Console.WriteLine("");
 					Console.BackgroundColor = ConsoleColor.Green;
					Console.WriteLine("");
					Console.BackgroundColor = ConsoleColor.Red;
					Console.WriteLine("");
					Console.BackgroundColor = ConsoleColor.Magenta;
					Console.WriteLine("");
					Console.BackgroundColor = ConsoleColor.Cyan;
					Console.WriteLine("");
					Console.BackgroundColor = ConsoleColor.Yellow;
					Console.WriteLine("");
			}
		}

		public static void CommandList()
		{
			Console.WriteLine("");
			Console.WriteLine("PrintOnlineUsers - prints all currently online users.");
			Console.WriteLine("RegisterUser     - register a new user in the database.");
			Console.WriteLine("KickUser         - terminates the user's connection.");
		}
	
		public static void PrintOnlineUsers()
		{
			Console.WriteLine("");
			try
			{
				foreach (User user in User.UserList)
				{
					Console.WriteLine(Encoding.ASCII.GetString(user.userName));
				}
			}
			catch { PrintOnlineUsers(); }
		}

		public static void RegisterUser() 
		{
			Console.WriteLine("");
			string userName;
			string passWord;
			Console.Write("Username: ");
			userName = Console.ReadLine();
			Console.Write("Password: ");
			passWord = Console.ReadLine();
			
			string content;
			StreamReader sr = new StreamReader("Information.txt");
			content = sr.ReadToEnd();
			sr.Close();
			StreamWriter sw = new StreamWriter("Information.txt");
			sw.WriteLine(userName+":"+passWord);
			sw.Write(content);
			sw.Close();

			Login.LoginInformationList.Add(new Login(userName, passWord));
			Console.WriteLine("User {0} created!", userName);
			
		}

		public static void KickUser()
		{
			string input = null;
			bool finished = false;
			Console.WriteLine("Enter the username to kick. To abort enter #abort.");
			while (finished == false)
			{
				Console.Write("Username: ");
				input = Console.ReadLine();
				if (input == "#abort") { finished = true; return; }
				try
				{
					var UserToKick = User.UserList.SingleOrDefault(u => Encoding.ASCII.GetString(u.userName) == input);
					if (UserToKick != null)
					{
						Console.WriteLine("User {0} kicked!", input);
						User.RemoveUser(UserToKick.soc);
						return;
					}
					else { Console.WriteLine("No such user is currently online!"); }
				}
				catch { }
			}

		}

	}
}
