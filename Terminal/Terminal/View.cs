using System;
using System.IO;

namespace Terminal
{
    internal sealed class View
    {
        public void Start()
        {
            WriteColor("Welcome to the terminal emulator made -F1st3K -> ", ConsoleColor.Yellow);
            WriteColor("https://github.com/F1st3K/\n", ConsoleColor.Cyan);
        }

        public string Run()
        {

            WriteColor("┌──[", ConsoleColor.Red);
            WriteColor(Environment.UserName, ConsoleColor.White);
            WriteColor("@", ConsoleColor.Yellow);
            WriteColor(Environment.UserDomainName, ConsoleColor.Cyan);
            WriteColor("]─[", ConsoleColor.Red);
            WriteColor(Directory.GetCurrentDirectory().Replace("\\", "/"), ConsoleColor.Green);
            WriteColor("]\n└──", ConsoleColor.Red);
            WriteColor("$", ConsoleColor.Yellow);
            return Console.ReadLine();
        }

        public void Output(string value)
        {
            WriteLineColor(value, ConsoleColor.Yellow);
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void CommandNotFound(string command)
        {
            WriteLineColor(command + ": command not found", ConsoleColor.Red);
        }

        public void Stop()
        {
            WriteLineColor("Bye!)", ConsoleColor.Cyan);
        }

        private void WriteColor(string value, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(value);
            Console.ResetColor();
        }
        private void WriteLineColor(string value, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ResetColor();
        }
    }
}
