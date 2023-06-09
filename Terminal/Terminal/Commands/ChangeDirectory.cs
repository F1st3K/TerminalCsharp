﻿using System.IO;

namespace Terminal.Commands
{
    internal class ChangeDirectory : Command
    {
        /// <summary>
        /// Class <c>ChangeDirectory</c> change current dirrectory.
        /// </summary>
        public ChangeDirectory(string name) : base(name)
        {
            _command = Start;
        }

        private string Start()
        {
            string output = string.Empty;
            string current = Directory.GetCurrentDirectory();
            if (_values.Count > 1)
                return "cd: too many arguments";
            if (_values.Count <= 0)
            {
                Directory.SetCurrentDirectory("\\");
                return output;
            }
            
            try
            {
                if (Path.IsPathRooted(_values[0]))
                    Directory.SetCurrentDirectory(_values[0]);
                else
                    Directory.SetCurrentDirectory(current + "\\" + _values[0]);
            }
            catch
            {
                return "cd:\t" + _values[0] + ": No such file or directory";
            }
            return output;
        }
    }
}
