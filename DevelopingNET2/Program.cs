using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DevelopingNET2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Server.AcceptMsq();
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    new Thread(() =>
                    {
                        Client.SendMsg($"{args[0]} {i}");
                    }).Start();
                }
                string input;
                while (true)
                {
                    Console.Write("Введите сообщение (или 'Exit' для выхода): ");
                    input = Console.ReadLine();

                    if (input?.Trim().ToLower() == "exit")
                    {
                        Console.WriteLine("Клиент завершает работу.");
                        break;
                    }
                }

            }
        }
    }
}
