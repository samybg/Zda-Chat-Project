using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace servertest
{
	class ChatFunctions
	{
		public static void PrivateChatMessager(byte[] buffer)
		{
			Console.WriteLine(Encoding.ASCII.GetString(buffer).Trim('\0'));
			string bufferString = Encoding.ASCII.GetString(buffer);
			int index = bufferString.IndexOf(';');
			string UsersTogether = bufferString.Substring(2, (index-2));
			string[] UserNames = UsersTogether.Split(':');
			
            try
			{
				foreach (string userName in UserNames)
				{
					try
					{
						User privateUser;
						privateUser = User.UserList.SingleOrDefault(u => u.userNameString == userName);
						privateUser.stream.Write(buffer, 0, buffer.Length);
					}
					catch { };
				}
			}
			catch { };



		}



		public static void PrivateChatRequestDeclined(byte[] buffer, string userNameString)
		{
			byte[] UserNameToRespondTo = new byte[50];
			System.Buffer.BlockCopy(buffer, 2, UserNameToRespondTo, 0, 50);
			string UserNameToRespondToString = Encoding.ASCII.GetString(UserNameToRespondTo);
			UserNameToRespondToString = UserNameToRespondToString.TrimEnd('\0');

			try
			{
				User UserToRespondTo = User.UserList.Single(u => u.userNameString == UserNameToRespondToString);
				string ResponseToPrivateChat = null;
				ResponseToPrivateChat = "fd" + userNameString;
				byte[] Response = new byte[ResponseToPrivateChat.Length];
				Response = Encoding.ASCII.GetBytes(ResponseToPrivateChat);
				UserToRespondTo.stream.Write(Response, 0, Response.Length);
				Console.WriteLine("Somebody just declined a request");
			}
			catch { };


		}


		public static void PrivateChatRequestAccepted(byte[] buffer, string userNameString)
		{
			byte[] UserNameToRespondTo = new byte[50];
			System.Buffer.BlockCopy(buffer,2,UserNameToRespondTo,0,50);
			string UserNameToRespondToString  = Encoding.ASCII.GetString(UserNameToRespondTo);
			UserNameToRespondToString = UserNameToRespondToString.TrimEnd('\0');
			
			try 
			{ 
				User UserToRespondTo = User.UserList.Single(u => u.userNameString == UserNameToRespondToString);
				string ResponseToPrivateChat = null;
				ResponseToPrivateChat = "fa" + userNameString;
				Console.WriteLine(ResponseToPrivateChat);
				byte[] Response = new byte[ResponseToPrivateChat.Length];
				Response = Encoding.ASCII.GetBytes(ResponseToPrivateChat);
				UserToRespondTo.stream.Write(Response, 0, Response.Length);
				UserToRespondTo.stream.Flush();
				Console.WriteLine("Somebody just accepted a request");
			}
			catch { };


		}


		public static void PrivateChatRequestSender(byte[] buffer, string userNameString)
		{
			byte[] UserNameToRequest = new byte[50];
			System.Buffer.BlockCopy(buffer,2,UserNameToRequest,0,50);
			string UserNameToRequestString = Encoding.ASCII.GetString(UserNameToRequest);
			UserNameToRequestString = UserNameToRequestString.TrimEnd('\0');
			
			//try { User UserRequesting = User.UserList.Single(u => u.userNameString == userNameString); }
			//catch { };
			
			try
			{
			User UserToRequest = User.UserList.Single(u => u.userNameString == UserNameToRequestString);
			string RequestForPrivateChat = null;
			RequestForPrivateChat = "fp" + userNameString;
			Console.WriteLine(RequestForPrivateChat);
			byte[] Request = new byte[100];
			Request = Encoding.ASCII.GetBytes(RequestForPrivateChat);
			UserToRequest.stream.Write(Request,0,Request.Length);
			UserToRequest.stream.Flush();
			Console.WriteLine("{0} just sent a private chat request to {1}",userNameString,UserNameToRequestString);
			}
			catch { }
		}



		

		public static void WriteToAll(byte[] buffer)
		{
			foreach (User user in User.UserList)
			{
				user.stream.Write(buffer, 0, buffer.Length);
			}
		}


		public static void FunctionChooser(byte[] buffer,string userNameString)
		{
			string identifier1 = Encoding.ASCII.GetString(buffer, 0, 1); //function or message c/f
			string identifier2 = Encoding.ASCII.GetString(buffer, 1, 1); //type

			if (identifier1 == "c")
			{
				if (identifier2 == "a") { WriteToAll(buffer); }
				if (identifier2 == "p") { PrivateChatMessager(buffer); }
			}


			if (identifier1 == "f")
			{
				if (identifier2 == "p") { PrivateChatRequestSender(buffer, userNameString); } //request to user for private chat
				if (identifier2 == "a") { PrivateChatRequestAccepted(buffer, userNameString);}
				if (identifier2 == "d") { PrivateChatRequestDeclined(buffer, userNameString); }
			}
		}




		public static void Reader(IAsyncResult soc)
		{
			try
			{
				int read;
				User currentUser = User.UserList.Single(u => u.soc == (Socket)soc.AsyncState);
				read = currentUser.stream.EndRead(soc);
				if (read > 0)
				{
					byte[] buffer = new byte[1024];
					System.Buffer.BlockCopy(currentUser.streamCatcher, 0, buffer, 0, currentUser.streamCatcher.Length);
					FunctionChooser(buffer, currentUser.userNameString);
					currentUser.streamCatcher = new byte[1024];
					buffer = new byte[1024];
				}
				currentUser.stream.BeginRead(currentUser.streamCatcher, 0, currentUser.streamCatcher.Length, Reader, currentUser.soc);
			}
			catch { }
		}








	}
}
