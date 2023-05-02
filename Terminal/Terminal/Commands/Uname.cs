using System;

namespace Terminal.Commands
{
    /// <summary>
    /// Class <c>Uname</c> show information from pc.
    /// </summary>
    internal class Uname : Command
    {
        public Uname(string name) : base(name)
        {
            PossibleKeys.Add("-p");
            PossibleKeys.Add("-s");
            PossibleKeys.Add("-n");
            _command = Start;
        }

        private string Start()
        {
            string output = string.Empty;
            if (_keys.Count <= 0)
                _keys.Add("-s");
            if (_keys.Contains("-p"))
                output += "\t"+(Environment.Is64BitOperatingSystem ? "x64" : "x32");
            if (_keys.Contains("-n"))
                output += "\t" + Environment.UserDomainName;
            if (_keys.Contains("-s"))
                output += "\t" + Environment.OSVersion.VersionString;
            return output;
        }
    }
}
