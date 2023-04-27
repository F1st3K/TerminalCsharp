using System;
using System.Collections.Generic;
using Terminal.Commands;

namespace Terminal
{
    internal sealed class Executor
    {
        private Dictionary<string, Func<string[], string>> _listCommands = new Dictionary<string, Func<string[], string>>()
        {
            { "ls", ListFiles.Run},
        };
        public bool CommandIsExist(string value)
        {
            return _listCommands.ContainsKey(value);
        }

        public string RunCommand(string[] values)
        {
            _listCommands.TryGetValue(values[0], out var command);
            return command.Invoke(values);
        }
    }
}
