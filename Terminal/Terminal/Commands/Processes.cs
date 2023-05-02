using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Terminal.Commands
{
    /// <summary>
    /// Class <c>Processes</c> show procesess by pid.
    /// </summary>
    internal class Processes : Command
    {
        public Processes(string name) : base(name)
        {
            PossibleKeys.Add("-A");
            PossibleKeys.Add("-e");
            PossibleKeys.Add("-p");
            _command = Start;
        }

        private string Start()
        {
            string output = string.Empty;
            try
            {
                Process[] ps;
                if (_keys.Contains("-p") == false && _values.Count > 0)
                    throw new Exception("missing operands");
                if (_keys.Count > 0)
                    ps = Process.GetProcesses();
                else
                    ps = new Process[] { Process.GetCurrentProcess() };
                if (_keys.Contains("-p"))
                    ps = SearchProcesses(ps, _values.ToArray());
                var view = ViewProcesses(ps);
                foreach (var p in view)
                {
                    output += "\n" + p;
                }
            }
            catch (Exception ex)
            {
                output += "ps: " + ex.Message;
            }

            return output;
        }

        private Process[] SearchProcesses(Process[] ps, string[] values)
        {
            var listProcesses = new List<Process>();
            foreach (var pid in values)
            {
                if (int.TryParse(pid, out var id) == false)
                    throw new Exception("process id syntax error");
                foreach (var p in ps)
                {
                    if (p.Id == id)
                        listProcesses.Add(p);
                }
            }
            return listProcesses.ToArray();
        }

        private string[] ViewProcesses(Process[] ps) 
        {
            var output = new List<string>();
            foreach (var p in ps)
                try 
                { 
                    output.Add($"{p.Id, 6} {"?", 6} {p.TotalProcessorTime.ToString(@"hh\:mm\:ss"), 9} {p.ProcessName}");
                }
                catch
                {
                    continue;
                }
            output.Sort();
            output.Insert(0, $"{"PID",6} {"TTY",6} {"TIME",9} {"CMD"}");
            return output.ToArray();
        }
    }
}
