using System;
using System.IO;

namespace Terminal
{
    /// <summary>
    /// Class <c>View</c> view console.
    /// </summary>
    internal sealed class View
    {
        //Start message
        public void Start()
        {
            WriteColor("Welcome to the terminal emulator made -F1st3K -> ", ConsoleColor.Yellow);
            WriteColor("https://github.com/F1st3K/\n", ConsoleColor.Cyan);
        }
        //View information
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
        //Output console
        public void Output(string value)
        {
            WriteLineColor(value, ConsoleColor.Yellow);
        }
        //Clear console
        public void Clear()
        {
            Console.Clear();
        }
        //Error message
        public void CommandNotFound(string command)
        {
            WriteLineColor(command + ": command not found", ConsoleColor.Red);
        }
        //Stop message
        public void Stop()
        {
            WriteLineColor("Bye!)", ConsoleColor.Cyan);
        }
        //Write color console
        private void WriteColor(string value, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(value);
            Console.ResetColor();
        }
        //write line color console
        private void WriteLineColor(string value, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ResetColor();
        }
    }
}
