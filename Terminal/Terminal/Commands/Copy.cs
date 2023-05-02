using System;
using System.IO;

namespace Terminal.Commands
{
    /// <summary>
    /// Class <c>Copy</c> copy files and directoryes.
    /// </summary>
    internal class Copy : Command
    {
        public Copy(string name) : base(name)
        {
            PossibleKeys.Add("-f");
            PossibleKeys.Add("-v");
            PossibleKeys.Add("-i");
            PossibleKeys.Add("-n");
            _command = Start;
        }

        private string Start()
        {
            string output = string.Empty;
            try
            {
                string current = string.Empty;
                if (Path.IsPathRooted(_values[_values.Count-1]) == false)
                    current = Directory.GetCurrentDirectory() + "\\";
                string destFile = current + _values[_values.Count - 1];
                if (_values.Count == 2 && File.Exists(current + _values[0]))
                {
                    if (File.Exists(destFile))
                    {
                        if (_keys.Contains("-n"))
                            return output;
                        if (_keys.Contains("-i") && Confirmation("cp: overwrite " + destFile + "?") == false)
                            return output;
                    }
                    File.Copy(current + _values[0], destFile, true);
                    if (_keys.Contains("-v"))
                        output += "\n" + _values[0] + " -> " + destFile;
                    return output;
                }
                if (Directory.Exists(destFile) ||
                    Directory.Exists(Directory.GetParent(destFile).FullName))
                {
                    for (int i = 0; i < _values.Count - 1; i++)
                    {
                        current = string.Empty;
                        if (Path.IsPathRooted(_values[i]) == false)
                            current = Directory.GetCurrentDirectory() + "\\";
                        if (File.Exists(current + _values[i]))
                        {
                            CopyFile(current + _values[i], destFile);
                            if (_keys.Contains("-v"))
                                output += "\n" + _values[i] + " -> " + destFile;
                            }
                        else if (Directory.Exists(current + _values[i]))
                        {
                            CopyDirectory(current + _values[i], destFile);
                            if (_keys.Contains("-v"))
                                output += "\n" + _values[i] + " -> " + destFile;
                            }
                        else
                        {
                            throw new ArgumentException(_values[i] + ": No such file or directory ");
                        }
                    }
                }
                else
                    throw new ArgumentException(_values[_values.Count - 1] + ": No such directory ");
            }
            catch (Exception ex)
            {
                return "cp: " + ex.Message;
            }
            return output;
        }

        private void CopyDirectory(string path, string dest)
        {
            var dir = (path).Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            var nameDir = dir[dir.Length - 1];
            Directory.CreateDirectory(dest +"\\" + nameDir);
            var files = Directory.GetFileSystemEntries(path);
            for (int i = 0; i < files.Length; i++)
            {
                if (File.Exists(files[i]))
                    CopyFile(files[i], dest + "\\" + nameDir);
                else if (Directory.Exists(files[i]))
                    CopyDirectory(files[i], dest + "\\" + nameDir);
                else continue;
            }
        }

        private void CopyFile(string path, string dest)
        {
            var f = (path).Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            var fileName = f[f.Length - 1];
            if (dest.Substring(dest.Length - 1) != "\\")
                dest += "\\";
            if (File.Exists(dest + fileName))
            {
                if (_keys.Contains("-n"))
                    return;
                if (_keys.Contains("-i") && Confirmation("cp: overwrite " + fileName + "?") == false)
                    return;
            }
            File.Copy(path, dest + fileName, true);
        }

        private bool Confirmation(string value)
        {
            while (true)
            {
                Console.Write(value);
                var key = Console.ReadLine();
                if (key == "y")
                    return true;
                if (key == "n")
                    return false;
            }
        }
    }
}
