using System.IO;

namespace Terminal.Commands
{
    /// <summary>
    /// Class <c>PrintWorkingDirectory</c> show current directory.
    /// </summary>
    internal class PrintWorkingDirectory : Command
    {
        public PrintWorkingDirectory(string name) : base(name)
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
