﻿using System;
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
            string view = "┌─[-]─[" + Environment.UserName + "@" + Environment.UserDomainName + "]" +
                    "─[" + Directory.GetCurrentDirectory().Replace("\\", "/") + "]\n└──$";
            Console.Write(view);
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
