using System;
using System.IO;

namespace Terminal.Commands
{
    /// <summary>
    /// Class <c>DiskUsage</c> show usage memory directoryes and files.
    /// </summary>
    internal class DiskUsage : Command
    {
        public DiskUsage(string name) : base(name)
        {
            PossibleKeys.Add("-a");
            PossibleKeys.Add("-c");
            PossibleKeys.Add("-h");
            _command = Start;
        }

        private string Start()
        {
            string output = string.Empty;
            if (_values.Count <= 0)
                _values.Add(Directory.GetCurrentDirectory());
            long total = 0;
            foreach (var file in _values)
                try
                {
                    string current = string.Empty;
                    if (Path.IsPathRooted(_values[_values.Count - 1]) == false)
                        current = Directory.GetCurrentDirectory() + "\\";
                    output += RecursiveEntry(current + file);
                    total += DirSize(new DirectoryInfo(current + file));
                }
                catch (Exception ex)
                {
                    output += "\n du: "+ file + ": " + ex.Message;
                    continue;
                }
            if (_keys.Contains("-c"))
            {
                var size = _keys.Contains("-h") ? (total / 1024).ToString() + "KB" : total.ToString();
                output += $"\n {size,6}\ttotal";
            }
            return output;
        }

        private string RecursiveEntry(string path)
        {
            string output = string.Empty;
            if (Directory.Exists(path))
            {
                var d = new DirectoryInfo(path);
                string size = _keys.Contains("-h") ? (DirSize(d) / 1024).ToString() + "KB" : DirSize(d).ToString();
                output += $"\n {size,6}\t" +
                    $"{(path).Replace(Directory.GetCurrentDirectory() + "\\", string.Empty).Replace("\\", "/")}";
                foreach (var directory in d.GetDirectories())
                {
                    output += RecursiveEntry(directory.FullName);
                }
                if (_keys.Contains("-a"))
                    foreach (var directory in d.GetFiles())
                    {
                        output += RecursiveEntry(directory.FullName);
                    }
            }
            else if (File.Exists(path))
            {
                string size = _keys.Contains("-h") ? (File.ReadAllBytes(path).Length / 1024).ToString() + "KB" : File.ReadAllBytes(path).Length.ToString();
                output += $"\n {size,6}\t" +
                    $"{(path).Replace(Directory.GetCurrentDirectory() + "\\", string.Empty).Replace("\\", "/")}";
            }
            return output;
        }

        public static long DirSize(DirectoryInfo d)
        {
            long Size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                try
                {
                    Size += fi.Length;
                }
                //Данное исключение делается для пропуска папок к которым нет доступа
                catch (UnauthorizedAccessException)
                {
                    ;
                }
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                try
                {
                    Size += DirSize(di);
                }
                //Данное исключение делается для пропуска папок к которым нет доступа
                catch (UnauthorizedAccessException)
                {
                    ;
                }
            }
            return (Size);
        }
    }
}
