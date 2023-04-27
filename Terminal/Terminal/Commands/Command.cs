using System;
using System.Collections.Generic;

namespace Terminal.Commands
{
    internal class Command
    {
        private protected List<string> _keys;
        private protected List<string> _values;
        private protected Func<string> _command;

        public List<string> PossibleKeys { get; private protected set; }

        public Command()
        {
            _keys = new List<string>();
            _values = new List<string>();
            PossibleKeys = new List<string>();
        }

        public string Run(string[] arguments)
        {
            SeparateArguments(arguments);
            var check = CheckKeys();
            if (check != String.Empty)
                return check;
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
