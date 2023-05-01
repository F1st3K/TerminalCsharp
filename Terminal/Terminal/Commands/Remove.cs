using System;
using System.IO;

namespace Terminal.Commands
{
    internal class Remove : Command
    {
        public Remove(string name) : base(name)
        {
            PossibleKeys.Add("-f");
            PossibleKeys.Add("-r");
            PossibleKeys.Add("-v");
            _command = Start;
        }

        private string Start()
        {
            string output = string.Empty;
            if (_values.Count <= 0)
                return "rm: missing operand";
            foreach (var file in _values)
            {
                string current = Directory.GetCurrentDirectory() + "\\";
                try
                {
                    if (Path.IsPathRooted(_values[0]))
                        current = string.Empty;

                    if (Directory.Exists(current + file))
                    {
                        if (_keys.Contains("-r"))
                            Directory.Delete(current + file, true);
                        else Directory.Delete(current + file);
                    }
                    else if (File.Exists(current + file))
                    {
                        File.Delete(current + file);
                    }
                    else throw new Exception("No such file or directory");

                    if (_keys.Contains("-v"))
                        output += "\nremoved " + file;
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
