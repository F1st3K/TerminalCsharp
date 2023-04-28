﻿using System;
using System.Collections.Generic;
using Terminal.Commands;

namespace Terminal
{
    internal sealed class Executor
    {
        private Dictionary<string, Func<string[], string>> _listCommands = new Dictionary<string, Func<string[], string>>()
        {
            { "ls", new ListFiles().Run},
            { "cd", new ChangeDirectory().Run},
            { "pwd", new PrintWorkingDirectory().Run},
            { "arch", new Architecture().Run},
            { "mkdir", new MakeDirectory().Run},
            { "rmdir", new RemoveDirectory().Run},
            { "cat", new Concatenate().Run},
        };
        public bool CommandIsExist(string value)
        {
            return _listCommands.ContainsKey(value);
        }

        public string RunCommand(string[] values)
        {
            if (values.Length < 1)
                throw new ArgumentException("values is no null");
            _listCommands.TryGetValue(values[0], out var command);
            string[] arguments = new string[values.Length - 1]; ;
            for (int i = 1, j = 0; i < values.Length; i++, j++)
                arguments[j] = values[i];
            return command.Invoke(arguments);
        }
    }
}
