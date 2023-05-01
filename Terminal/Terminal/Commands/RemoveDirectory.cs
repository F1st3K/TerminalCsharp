using System.IO;

namespace Terminal.Commands
{
    internal class RemoveDirectory : Command
    {
        public RemoveDirectory(string name) : base(name)
        {
            PossibleKeys.Add("-p");
            PossibleKeys.Add("-v");
            _command = Start;
        }

        private string Start()
        {
            string output = string.Empty;
            if (_values.Count <= 0)
                return "rmdir: missing operand";
            string current = string.Empty;
            foreach (var value in _values)
            {
                if (Path.IsPathRooted(value) == false)
                    current = Directory.GetCurrentDirectory() + "\\";
                if (Directory.Exists(current + value) == false)
                    return "rmdir: failed to remove " + value.Replace("\\", "/") + ": No such file or directory ";
                try
                {
                    Directory.Delete(current + value);
                    if (_keys.Contains("-v"))
                        output += "\nrmdir: removing directory " + value.Replace("\\", "/");
                    if (_keys.Contains("-p"))
                    {
                        var dir = Directory.GetParent(current + value).FullName;
                        while (dir + "\\" != current)
                        {
                            Directory.Delete(dir);
                            if (_keys.Contains("-v"))
                                output += "\nrmdir: removing directory " + dir.Replace(current, string.Empty).Replace("\\", "/");
                            dir = Directory.GetParent(dir).FullName;
                        }
                    }

                }
                catch
                {
                    output += "\nrmdir: failed to remove " + value.Replace("\\", "/") + ": Directory not empty";
                }
            }
            return output;
        }
    }
}