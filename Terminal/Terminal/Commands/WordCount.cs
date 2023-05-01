using System;
using System.IO;

namespace Terminal.Commands
{
    internal class WordCount : Command
    {
        public WordCount() : base()
        {
            PossibleKeys.Add("-m");
            PossibleKeys.Add("-c");
            PossibleKeys.Add("-l");
            _command += Start;
        }

        private string Start()
        {
            string output = string.Empty;
            if (_keys.Count <= 0)
            {
                _keys.Add("-m");
                _keys.Add("-c");
                _keys.Add("-l");
            }
            int totalLines = 0;
            int totalBytes = 0;
            int totalChars = 0;
            foreach (var file in _values)
            {
                try
                {
                    string current = string.Empty;
                    if (Path.IsPathRooted(file) == false)
                        current = Directory.GetCurrentDirectory() + "\\";
                    if (File.Exists(current + file) == false)
                        throw new ArgumentException("No such file or directory ");

                    if (_keys.Contains("-l"))
                    {
                        var lines = File.ReadAllLines(current + file).Length;
                        output += $"{lines, 6} ";
                        totalLines += lines;
                    }
                    if (_keys.Contains("-c"))
                    {
                        var bytes = File.ReadAllBytes(current + file).Length;
                        output += $"{bytes,6} ";
                        totalBytes += bytes;
                    }
                    if (_keys.Contains("-m"))
                    {
                        var chars = File.ReadAllText(current + file).Length;
                        output += $"{chars,6} ";
                        totalChars += chars;
                    }
                    output += file + "\n";
                }
                catch (Exception ex)
                {
                    output += "cat: " + file.Replace("\\", "/") + ": " +ex.Message + "\n";
                    continue;
                }
            }
            if (_values.Count > 1)
                output += $"{totalLines,6} {totalBytes,6} {totalChars,6} Total";
            return output;
        }
    }
}
