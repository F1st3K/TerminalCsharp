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
                    "─[" + Directory.GetCurrentDirectory() + "]\n└──$";
            Console.Write(view);
            return Console.ReadLine();
        }

        public void Output(string value)
        {
            Console.WriteLine(value);
        }

        public void CommandNotFound()
        {
            Console.WriteLine("command not found");
        }

        public void Stop()
        {
            Console.WriteLine("Terminal is stoped!");
            Console.ReadKey();
        }
    }
}
