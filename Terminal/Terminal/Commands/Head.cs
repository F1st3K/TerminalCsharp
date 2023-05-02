using System;

namespace Terminal.Commands
{
    /// <summary>
    /// Class <c>Head</c> show 10 start lines for file.
    /// </summary>
    internal class Head : Command
    {
        public Head(string name) : base(name)
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
                    for (int i = 0; i < (count > lines.Length ? lines.Length : count); i++)
                    {
                        output += "\n" + lines[i];
                    }
                }
                catch (Exception ex)
                {
                    return "head: " + filename.Replace("\\", "/") + ": " + ex.Message;
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
