using System.Collections.Generic;
using System.IO;

namespace Terminal.Commands
{
    internal class ListFiles : Command
    {
        public ListFiles() : base()
        {
            PossibleKeys.Add("-a");
            PossibleKeys.Add("-r");
            PossibleKeys.Add("-t");
            _command = Start;
        }

        private string Start()
        {
            string output = string.Empty;
            if (_values.Count <= 0)
            {
                output = List(Directory.GetCurrentDirectory());
            }
            else
            {
                foreach (var value in _values)
                {
                    output += List(value);
                }
            }
            return output;
        }

        private string List(string path)
        {
            string current = Path.GetFullPath(path).Replace("\\", "/");
            var names = current.Split(new string[] {"/"}, System.StringSplitOptions.RemoveEmptyEntries);
            string output = names[names.Length-1] + ": \n";
            var list = new List<string>();
            try
            {
                if (Path.IsPathRooted(path))
                    list.AddRange(Directory.GetFileSystemEntries(path));
                else
                    list.AddRange(Directory.GetFileSystemEntries(Directory.GetCurrentDirectory()+ "\\" + path));
                }
            catch
            {
                return "cd:\t" + _values[0] + ": No such file or directory";
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (Directory.Exists(list[i]))
                    list[i] += "\\";
                list[i] = list[i].Replace("\\", "/");
                list[i] = "\t" + list[i].Replace(current, string.Empty);
            }
            foreach (var file in list)
            {
                output += "\n\t" + file;
            }
            return output + "\n";
        }
    }
}
