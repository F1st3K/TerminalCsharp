using System;
using System.IO;

namespace Terminal
{
    internal sealed class View
    {
        public void Start()
        {
            Console.WriteLine("Terminal is started:");
        }

        public string Run()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("┌──[");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(Environment.UserName);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("@");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(Environment.UserDomainName);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("]─[");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(Directory.GetCurrentDirectory().Replace("\\", "/"));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("]\n└──");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("$");
            Console.ResetColor();
            return Console.ReadLine();
        }

        public void Output(string value)
        {
            Console.WriteLine(value);
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void CommandNotFound(string command)
        {
            Console.WriteLine(command + ": command not found");
        }

        public void Stop()
        {
            Console.WriteLine("Terminal is stoped!");
            Console.ReadKey();
        }
    }
}
