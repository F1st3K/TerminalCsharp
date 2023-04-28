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
            PossibleKeys.Add("-A");
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
                list[i] = list[i].Replace(current, string.Empty).Substring(1);                
            }
            if (_keys.Contains("-a") == false && _keys.Contains("-A") == false)
                for (int i = 0; i<list.Count;i++)
                    if (list[i].ToCharArray()[0] == '.')
                        list.RemoveAt(i);

            if (_keys.Contains("-a") == true && _keys.Contains("-A") == false ||
                _keys.Contains("-a") == true && _keys.Contains("-A") == true && 
                _keys.IndexOf("-a") > _keys.IndexOf("-A"))
            {
                list.Insert(0, "../");
                list.Insert(0, "./");
            }
            if (_keys.Contains("-r"))
                list.Reverse();

            foreach (var file in list)
            {
                output += "\n\t" + file;
            }
            return output + "\n";
        }
    }
}
