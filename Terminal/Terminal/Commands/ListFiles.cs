using System.Collections.Generic;
using System.IO;

namespace Terminal.Commands
{
    internal class ListFiles : Command
    {
        public ListFiles() : base()
        {
            PossibleKeys.Add("-a");
            PossibleKeys.Add("-r");
            PossibleKeys.Add("-t");
            _command = Start;
        }

        private string Start()
        {
            string output = string.Empty;
            var current = Directory.GetCurrentDirectory();
            var list = new List<string>();
            list.AddRange(Directory.GetFileSystemEntries(current));
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = list[i].Replace(current + "\\", string.Empty);
            }
            foreach (var file in list)
            {
                output += "\t"+file;
            }
            return output;
        }
    }
}
