using System;
using System.Collections.Generic;
using System.IO;

namespace Terminal.Commands
{
    internal class Concatenate : Command
    {
        public Concatenate() : base()
        {
            PossibleKeys.Add("->");
            PossibleKeys.Add("-b");
            PossibleKeys.Add("-T");
            _command = Start;
        }

        private string Start()
        {
            if (_keys.Contains("->"))
                return WriteFile();
            else return ReadFile();
        }

        private string ReadFile()
        {
            var output = string.Empty;
            foreach (var filename in _values)
            {
                string current = string.Empty;
                if (Path.IsPathRooted(filename) == false)
                    current = Directory.GetCurrentDirectory() + "\\";
                if (File.Exists(current + filename) == false)
                    return "cat: " + filename.Replace("\\", "/") + ": No such file or directory ";
                var lines = File.ReadAllLines(current + filename);
                lines = ChangeView(lines);
                foreach (var line in lines)
                    output += "\n" + line;
            }
            return output;
        }

        private string[] ChangeView(string[] lines)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (_keys.Contains("-T"))
                    lines[i] = lines[i].Replace("   ", "^I");
                if (_keys.Contains("-b"))
                    lines[i] = $"{(i+1), 4} |" + lines[i];
            }
            return lines;
        }

        private string WriteFile()
        {
            var listLines = new List<string>();
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Modifiers == ConsoleModifiers.Control && key.Key == ConsoleKey.O)
                    break;
                Console.Write(key.KeyChar);
                listLines.Add(Console.ReadLine());
            }
            var lines = ChangeView(listLines.ToArray());
            string current = string.Empty;
            foreach (var filename in _values)
            {
                if (Path.IsPathRooted(filename) == false)
                    current = Directory.GetCurrentDirectory() + "\\";
                File.WriteAllLines(current + filename, lines);
            }

            return string.Empty;
        }
    }
}
