using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Terminal.Commands
{
    /// <summary>
    /// Class <c>Command</c> base class for commands.
    /// </summary>
    internal class Command
    {
        //Current key on run command
        private protected List<string> _keys;
        //Current values on run command
        private protected List<string> _values;
        //Method start command
        private protected Func<string> _command;
        //Name in man
        private protected string _name;
        //Possible keys
        public List<string> PossibleKeys { get; private protected set; }

        public Command(string name)
        {
            _keys = new List<string>();
            _values = new List<string>();
            PossibleKeys = new List<string>();
            _name = name;
            PossibleKeys.Add("--help");
        }
        //Start command
        public string Run(string[] arguments)
        {
            SeparateArguments(arguments);
            var check = CheckKeys();
            if (check != String.Empty)
                return check;
            if (_keys.Contains("--help"))
                try
                {
                    var start = Directory.GetParent(Assembly.GetExecutingAssembly().Location);
                    return File.ReadAllText(start + "\\help\\" + _name + ".help");
                }
                catch
                {
                    return "No such help information";
                }
            return _command.Invoke();
        }
        //Separate value without keys
        private protected void SeparateArguments(string[] args)
        {
            _keys.Clear();
            _values.Clear();
            foreach (var arg in args)
            {
                if (arg.ToCharArray()[0] == '-')
                    _keys.Add(arg);
                else _values.Add(arg);
            }
        }
        //Check valid keys
        private protected string CheckKeys()
        {
            foreach (var key in _keys)
            {
                if (PossibleKeys.Contains(key) == false)
                    return "Invalid key " + key;
            }
            return String.Empty;
        }
    }
}
