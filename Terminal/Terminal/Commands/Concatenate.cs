﻿using System;
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
            else
            {
                if (_values.Count <= 0)
                    return OnlineMode();
                return ReadFile();
            }
        }

        private string OnlineMode()
        {
            while (true)
            {
                var line = Console.ReadLine();
                if (line == "^exit")
                    break;
                line = ChangeView(new string[] { line })[0];
                Console.WriteLine(line);
            }
            return string.Empty;
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
            if (_values.Count <= 0)
                return "syntax error near unexpected token `newline'";
            var listLines = new List<string>();
            while (true)
            {
                var line = Console.ReadLine();
                if (line == "^exit")
                    break;
                listLines.Add(line);
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
