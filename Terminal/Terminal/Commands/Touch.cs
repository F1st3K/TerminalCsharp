using System;
using System.IO;

namespace Terminal.Commands
{
    class Touch : Command
    {
        public Touch() : base()
        {
            _command = Start;
        }

        private string Start()
        {
            string output = string.Empty;
            if (_values.Count <= 0)
                return "touch: missing operand";
            foreach (var file in _values)
            {
                string current = Directory.GetCurrentDirectory() + "\\";
                try
                {
                    if (Path.IsPathRooted(_values[0]))
                        current = string.Empty;
                    var f = File.Open(current + file, FileMode.OpenOrCreate);
                    f.Close();
                    File.SetLastWriteTime(current + file, DateTime.Now);
                }
                catch (Exception ex)
                {
                    if (_keys.Contains("-f"))
                        continue;
                    output += "\nrm: cannot remove " + file + ": " + ex.Message;
                    break;
                }
            }
            return output;
        }
    }
}
