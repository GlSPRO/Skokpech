using TypingSpeed;

namespace Skoropechatanie
{
    public class TextWorker
    {
        public static int countError = 0;
        public static int indexText = 0;
        public static void WriteText(string text)
        {
            int column = 0;
            int line = 0;
            var w = Console.WindowWidth;

            foreach (char item in text)
            {
                if (Program.isOn)
                {
                    indexText++;
                    var e = Console.ReadKey(true).KeyChar;
                    if (column == w)
                    {
                        column = 0;
                        line++;
                    }
                    if (item == e)
                    {
                        Console.SetCursorPosition(column, line);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(item);
                        column++;
                    }
                    else
                    {
                        Console.SetCursorPosition(column, line);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(item);
                        column++;
                        countError++;
                    }
                    Thread.Sleep(0);
                }
            }
        }
    }
}
