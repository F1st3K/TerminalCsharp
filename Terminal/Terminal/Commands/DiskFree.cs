using System;
using System.Collections.Generic;
using System.IO;

namespace Terminal.Commands
{
    /// <summary>
    /// Class <c>DiskFree</c> show information for disks.
    /// </summary>
    internal class DiskFree : Command
    {
        public DiskFree(string name) : base(name)
        {
            PossibleKeys.Add("-a");
            PossibleKeys.Add("-k");
            PossibleKeys.Add("-H");
            _command = Start;
        }

        private string Start()
        {
            string output = string.Empty;
            var drives = new List<DriveInfo>();
            if (_keys.Contains("-a"))
            {
                drives.AddRange(DriveInfo.GetDrives());
            }
            else
            {
                if (_values.Count <= 0)
                    _values.Add(Directory.GetCurrentDirectory());
                foreach (var file in _values)
                    try
                    {
                        var directory = new DirectoryInfo(file);
                        drives.Add(new DriveInfo(directory.FullName.Substring(0, 1)));
                    }
                    catch (Exception ex)
                    {
                        output += "\n du: " + file + ": " + ex.Message;
                    }
            }
            output += $"{"Name",6} {"Total",12} {"Used",12} {"Avaliable",12} {"Use",3}% Mounted on";
            foreach (var d in drives)
            {
                var total = d.TotalSize;
                var used = d.TotalSize - d.AvailableFreeSpace;
                var free = d.AvailableFreeSpace;
                var percent = (float)used / (float)total * 100;
                string unit = "";
                if (_keys.Contains("-k"))
                {
                    total = total / 1024;
                    used = used / 1024;
                    free = free / 1024;
                    unit = "KB";
                }
                else if (_keys.Contains("-H"))
                {
                    total = total / (1024 * 1024 * 1024);
                    used = used / (1024 * 1024 * 1024);
                    free = free / (1024 * 1024 * 1024);
                    unit = "GB";
                }
                output += $"\n{d.Name, 6} {total + unit, 12} {used + unit,12} {free + unit,12} {Math.Truncate(percent),3}% {d.DriveFormat}";
            }
            return output;
        }
    }
}
