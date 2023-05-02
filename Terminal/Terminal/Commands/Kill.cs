using System;
using System.Diagnostics;

namespace Terminal.Commands
{
    /// <summary>
    /// Class <c>Kill</c> kill process by pid.
    /// </summary>
    internal class Kill : Command
    {
        public Kill(string name) : base(name)
        {
            PossibleKeys.Add("-l");
            PossibleKeys.Add("-v");
            _command += Start;
        }

        private string Start()
        {
            string output = string.Empty;
            try
            {
                if (_keys.Contains("-l"))
                    return ListSignal();
                if (_values.Count <= 0)
                    throw new Exception("too many arguments");
                foreach (var pid in _values)
                {
                    if (int.TryParse(pid, out var id) == false)
                        throw new Exception(pid + ": syntax error");
                    var p = Process.GetProcessById(id);
                    p.Kill();
                    if (_keys.Contains("-v"))
                        output += "\n " + p.ProcessName + ": killed";
                }
            }
            catch(Exception ex)
            {
                output += "kill: " + ex.Message;
            }
            return output;
        }

        private string ListSignal()
        {
            return " 1) SIGHUP       2) SIGINT       3) SIGQUIT      4) SIGILL       5) SIGTRAP\n" +
                   " 6) SIGABRT      7) SIGEMT       8) SIGFPE       9) SIGKILL     10) SIGBUS\n" +
                   "11) SIGSEGV     12) SIGSYS      13) SIGPIPE     14) SIGALRM     15) SIGTERM\n" +
                   "16) SIGURG      17) SIGSTOP     18) SIGTSTP     19) SIGCONT     20) SIGCHLD\n" +
                   "21) SIGTTIN     22) SIGTTOU     23) SIGIO       24) SIGXCPU     25) SIGXFSZ\n" +
                   "26) SIGVTALRM   27) SIGPROF     28) SIGWINCH    29) SIGPWR      30) SIGUSR1\n" +
                   "31) SIGUSR2     32) SIGRTMIN    33) SIGRTMIN+1  34) SIGRTMIN+2  35) SIGRTMIN+3\n" +
                   "36) SIGRTMIN+4  37) SIGRTMIN+5  38) SIGRTMIN+6  39) SIGRTMIN+7  40) SIGRTMIN+8\n" +
                   "41) SIGRTMIN+9  42) SIGRTMIN+10 43) SIGRTMIN+11 44) SIGRTMIN+12 45) SIGRTMIN+13\n" +
                   "46) SIGRTMIN+14 47) SIGRTMIN+15 48) SIGRTMIN+16 49) SIGRTMAX-15 50) SIGRTMAX-14\n" +
                   "51) SIGRTMAX-13 52) SIGRTMAX-12 53) SIGRTMAX-11 54) SIGRTMAX-10 55) SIGRTMAX-9\n" +
                   "56) SIGRTMAX-8  57) SIGRTMAX-7  58) SIGRTMAX-6  59) SIGRTMAX-5  60) SIGRTMAX-4\n" +
                   "61) SIGRTMAX-3  62) SIGRTMAX-2  63) SIGRTMAX-1  64) SIGRTMAX ";
        }
    }
}
