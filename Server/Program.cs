using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{

    internal class Program
    {

        static async Task Main()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint ep = new IPEndPoint(ip, 1024);

            s.Bind(ep);
            s.Listen(10);

            try
            {

                while (true)
                {
                    Socket sn = s.Accept();
                    await Task.Run(() => { UpdateTime(sn); });
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Connect Error");
            }
            finally
            {
                s.Close();
            }
            async void UpdateTime(Socket socket)
            {
                //если клиент разорвал подключение падает прога без try catch
                while (socket.Connected)
                {
                    try
                    {
                        socket.Send(Encoding.UTF8.GetBytes(DateTime.Now.ToLongTimeString()));
                        await Task.Delay(1000);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Client closed");
                    }
                }
            }
        }

    }
}

