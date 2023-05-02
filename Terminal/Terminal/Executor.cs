using System;
using System.Collections.Generic;
using Terminal.Commands;

namespace Terminal
{
    /// <summary>
    /// Class <c>Executor</c> execute commands.
    /// </summary>
    internal sealed class Executor
    {
        //Diction containe all commands
        private Dictionary<string, Func<string[], string>> _listCommands = new Dictionary<string, Func<string[], string>>()
        {
            { "ls", new ListFiles("ls").Run},
            { "cd", new ChangeDirectory("cd").Run},
            { "pwd", new PrintWorkingDirectory("pwd").Run},
            { "arch", new Architecture("arch").Run},
            { "mkdir", new MakeDirectory("mkdir").Run},
            { "rmdir", new RemoveDirectory("rmdir").Run},
            { "cat", new Concatenate("cat").Run},
            { "head", new Head("head").Run},
            { "tail", new Tail("tail").Run},
            { "date", new Date("date").Run},
            { "rm", new Remove("rm").Run},
            { "touch", new Touch("touch").Run},
            { "ps", new Processes("ps").Run},
            { "kill", new Kill("kill").Run},
            { "wc", new WordCount("wc").Run},
            { "man", new Manual("man").Run},
            { "cp", new Copy("cp").Run},
            { "uname", new Uname("uname").Run},
            { "du", new DiskUsage("du").Run},
            { "df", new DiskFree("df").Run},
        };
        public bool CommandIsExist(string value)
        {
            return _listCommands.ContainsKey(value);
        }
        //Run command in dictionary
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
