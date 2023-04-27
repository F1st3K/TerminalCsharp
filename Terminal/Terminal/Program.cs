namespace Terminal
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            await System.Threading.Tasks.Task.Run(() => Work());
        }

        static void Work()
        {
            var view = new View();
            var executor = new Executor();
            view.Start();
            while (true)
            {
                string[] commands = view.Run().Split(' ');
                if (commands.Length == 0)
                    continue;
                if (commands[0] == "exit")
                {
                    view.Stop();
                    break;
                }
                if (executor.CommandIsExist(commands[0]))
                    view.Output(executor.RunCommand(commands));
                else view.CommandNotFound();
            }
        }
    }
}
