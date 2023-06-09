﻿using System.IO;

namespace Terminal.Commands
{
    /// <summary>
    /// Class <c>MakeDirectory</c> create new directory.
    /// </summary>
    internal class MakeDirectory : Command
    {
        public MakeDirectory(string name) : base(name)
        {
            PossibleKeys.Add("-p");
            PossibleKeys.Add("-v");
            _command = Start;
        }

        private string Start()
        {
            string output = string.Empty;
            if (_values.Count <= 0)
                return "mkdir: missing operand";
            string current = string.Empty;
            foreach (var value in _values)
            {
                if (Path.IsPathRooted(value) == false)
                    current = Directory.GetCurrentDirectory() + "\\";
                if (_keys.Contains("-p") == false &&
                    Directory.Exists(Directory.GetParent(current + value).FullName) == false)
                    return "mkdir: cannot create directory " + value.Replace("\\", "/") + ": No such file or directory";
                Directory.CreateDirectory(current + value);
                if (_keys.Contains("-v"))
                    output += "\nmkdir: created directory " + value.Replace("\\", "/");
            }
            return output;
        }
    }
}