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
    class Program
    {


        
        public static void OnConnectRequest(IAsyncResult UserConnects)
        {
            Socket current = (Socket)UserConnects.AsyncState;
            User.AddUser(current.EndAccept(UserConnects));
            current.BeginAccept(new AsyncCallback(OnConnectRequest), current);
        }


        
        static void Main(string[] args)
        {
            Login.FillLoginInformationList();
            Thread listKeeper = new Thread(User.ListKeeper);
            listKeeper.Start();

            //IPAddress[] LocalAdrr = null;                                                   // get
            string strHostName = "";                                                        // information
            strHostName = Dns.GetHostName();                                                //for
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);                           //listening 
            IPAddress localHost = IPAddress.Parse("127.0.0.1");
            //LocalAdrr = ipEntry.AddressList;                                                //socket
            Console.WriteLine("Sever listening on " + localHost.ToString() + ":8080");       //Print IP and Port the server is listening on
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //creates the listnening socket

            listener.Bind(new IPEndPoint(localHost, 8080));                              //Binds socket to ip and port
            listener.Listen(10);                                                            //sets timeout
            listener.BeginAccept(new AsyncCallback(OnConnectRequest), listener);            //socket begins listening
			Console.WriteLine("");
			Console.WriteLine("Type CommandList to see available commands.");
			while (true)
			{
				string serverCommand = Console.ReadLine();
				switch (serverCommand)
				{
					case "PrintOnlineUsers":
						ServerCommand.PrintOnlineUsers();
						break;
					case "RegisterUser":
						ServerCommand.RegisterUser();
						break;
					case "KickUser":
						ServerCommand.KickUser();
						break;

					case"CommandList":
						ServerCommand.CommandList();
						break;
					case"Tova ti e igrata":
						ServerCommand.dada();
						break;
					default:
						Console.WriteLine("No such command!");
						break;
				}
 				
			}
           

           

        }
    }
}
