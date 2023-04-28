using System;
using System.IO;

namespace Terminal.Commands
{
    class Remove : Command
    {
        public Remove() : base()
        {
            PossibleKeys.Add("-f");
            PossibleKeys.Add("-r");
            PossibleKeys.Add("-v");
            _command = Start;
        }

        private string Start()
        {
            string output = string.Empty;
            foreach (var file in _values)
            {
                string current = Directory.GetCurrentDirectory() + "\\";
                if (_values.Count <= 0)
                    return "rm: missing operand";
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
                    else
                    {
                        File.Delete(current + file);
                    }

                    if (_keys.Contains("-v"))
                        output += "\nremoved " + file;
                }
                catch
                {
                    if (_keys.Contains("-f"))
                        continue;
                    output += "\nrm: cannot remove " + file + ": No such file or directory";
                    break;
                }
            }
            return output;
        }
    }
}
