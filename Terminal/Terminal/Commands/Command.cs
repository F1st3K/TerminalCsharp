using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Terminal.Commands
{
    internal class Command
    {
        private protected List<string> _keys;
        private protected List<string> _values;
        private protected Func<string> _command;
        private protected string _name;

        public List<string> PossibleKeys { get; private protected set; }

        public Command(string name)
        {
            _keys = new List<string>();
            _values = new List<string>();
            PossibleKeys = new List<string>();
            _name = name;
            PossibleKeys.Add("--help");
        }

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
