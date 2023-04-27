using System.IO;

namespace Terminal.Commands
{
    internal class PrintWorkingDirectory : Command
    {
        public PrintWorkingDirectory() : base()
        {
            PossibleKeys.Add("-L");
            PossibleKeys.Add("-P");
            _command = Start;
        }

        private string Start()
        {
            string output = string.Empty;
            output += Directory.GetCurrentDirectory();
            return output;
        }
    }
}
