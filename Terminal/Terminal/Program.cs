using System.Reflection;

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
        }
    }
}
