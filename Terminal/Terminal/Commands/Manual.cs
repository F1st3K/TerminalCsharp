using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Terminal.Commands
{
    internal class Manual : Command
    {
        public Manual(string name) : base(name)
        {
            _command = Start;
        }

        private string Start()
        {
            var bufer = Console.BufferHeight;
            try
            {
                if (_values.Count <= 0)
                    return "What manual page do you wand?\nFor example, try 'man man'.";
                var file = Directory.GetParent(Assembly.GetExecutingAssembly().Location) + "\\manual\\" + _values[0] + ".man";
                if (File.Exists(file) == false)
                    return "No manual entry " + _values[0];
                var lines = File.ReadAllLines(file);

                bool run = true;
                int currentLine = 0;
                Console.BufferHeight = Console.WindowHeight;
                Console.CursorVisible = false;
                Console.Clear();
                Render(lines, currentLine, Console.WindowHeight, _values[0]);
                while (run)
                {
                    var key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.Q: run = false; 
                            continue;
                        case ConsoleKey.J:
                        case ConsoleKey.UpArrow: currentLine -= currentLine > 0 ? 1 : 0;
                            break;
                        case ConsoleKey.K:
                        case ConsoleKey.DownArrow: currentLine += currentLine < lines.Length - Console.WindowHeight ? 1 : 0;
                            break;
                        default: 
                            break;
                    }
                    Render(lines, currentLine, Console.WindowHeight, _values[0]);
                }
            }
            catch (Exception ex)
            {
                return "man: " + ex.Message;
            }
            Console.Clear();
            Console.ResetColor();
            Console.CursorVisible = true;
            Console.BufferHeight = bufer;
            return string.Empty;
        }

        private void Render(string[] lines, int start, int maxLengh, string name)
        {
            var output = string.Empty;
            string spase = String.Concat(Enumerable.Repeat(" ", Console.WindowWidth));
            spase = String.Concat(Enumerable.Repeat(spase + "\n", Console.WindowHeight));
            Console.SetCursorPosition(0, 0);
            Console.Write(spase);
            Console.SetCursorPosition(0, 0);
            for (int i = start; i < start + maxLengh-1; i++)
            {
                if (i > lines.Length-1)
                {
                    output += "\n";
                    continue;
                }
                output += $"{i + 1, 4}|{lines[i]} \n";
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(output);
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write($" Manual page {name} line {start + 1} (press q for quit)");
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
