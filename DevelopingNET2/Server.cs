using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace DevelopingNET2
{
    internal class Server
    {
        public static void AcceptMsq()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            UdpClient udpClient = new UdpClient(16874);
            Console.WriteLine("Сервер ожидает сообщения");
            Thread listenerThread = new Thread(() =>
            {
                while (true)
                {
                    byte[] buffer = udpClient.Receive(ref ep);
                    string data = Encoding.UTF8.GetString(buffer);
                    Thread tr = new Thread(() =>
                    {
                        Message msg = Message.FromJson(data);
                        Console.WriteLine(msg.ToString());
                        Message responseMsq = new Message("Server", "Message accept on serv!");
                        string responseMsqjs = responseMsq.ToJson();
                        byte[] responseDate = Encoding.UTF8.GetBytes(responseMsqjs);
                        udpClient.Send(responseDate, ep);
                    });
                    tr.Start();
                }
            });
                    listenerThread.Start();

                    // Ожидание нажатия любой клавиши для завершения
                    Console.ReadKey();
                    Console.WriteLine("Сервер завершает работу.");
                    udpClient.Close();

                
        }
    }
}
