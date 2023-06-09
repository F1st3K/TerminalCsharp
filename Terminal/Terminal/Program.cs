﻿namespace Terminal
{
    /// <summary>
    /// Class <c>Program</c> start point programm.
    /// </summary>
    class Program
    {
        //Run separate Task
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            await System.Threading.Tasks.Task.Run(() => Work());
        }
        //Main cycle
        static void Work()
        {
            var view = new View();
            var executor = new Executor();
            view.Start();
            while (true)
                try
                {
                    string[] commands = view.Run().Split(new char[] {' '}, System.StringSplitOptions.RemoveEmptyEntries);
                    if (commands.Length == 0)
                        continue;
                    switch (commands[0])
                    {
                        case "exit":
                            view.Stop();
                            return;
                        case "clear":
                            view.Clear();
                            continue;
                        default:
                            break;
                    }
                    if (executor.CommandIsExist(commands[0]))
                        view.Output(executor.RunCommand(commands));
                    else view.CommandNotFound(commands[0]);
                }
                catch
                {
                    view.Output("Terminal: ooops..");
                    continue;
                }
        }
    }
}
