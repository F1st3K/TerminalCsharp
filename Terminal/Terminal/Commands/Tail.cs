using System;

namespace Terminal.Commands
{
    /// <summary>
    /// Class <c>Tail</c> show last 10 lines for file.
    /// </summary>
    internal class Tail : Command
    {
        public Tail(string name) : base(name)
        {
            PossibleKeys.Add("-n");
            PossibleKeys.Add("-v");
            _command = Start;
        }

        private string Start()
        {
            if (_values.Count <= 0)
                return OnlineMode();
            var output = string.Empty;
            var count = 10;
            if (_keys.Contains("-n"))
            {
                if (int.TryParse(_values[_values.Count - 1], out count) == false)
                    return "head: invalid number of lines: " + _values[_values.Count - 1];
                _values.RemoveAt(_values.Count - 1);
            }
            foreach (var filename in _values)
            {
                try
                {
                    if (_keys.Contains("-v"))
                        output += "\n" + "==>\t" + filename + "\t<==";

                    var lines = Concatenate.Read(filename);
                    for (int i = lines.Length - 1; i >= (lines.Length - count >= 0 ? lines.Length - count : 0); i--)
                    {
                        output += "\n" + lines[i];
                    }
                }
                catch (Exception ex)
                {
                    return "tail: " + filename.Replace("\\", "/") + ": " + ex.Message;
                }
            }
            return output;
        }
        public static string OnlineMode()
        {
            while (true)
            {
                var line = Console.ReadLine();
                if (line == "^exit")
                    break;
            }
            return string.Empty;
        }
    }
}
