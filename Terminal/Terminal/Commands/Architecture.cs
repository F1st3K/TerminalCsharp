using System;

namespace Terminal.Commands
{
    internal class Architecture : Command
    {
        public Architecture(string name) : base(name)
        {
            _command = Start;
        }

        private string Start()
        {
            string output = string.Empty;
            output += Environment.Is64BitOperatingSystem ? "x86_64" : "x86";
            return output;
        }
    }
}  
