using Skoropechatanie;
using RecordReadWrite;
using RecordObject;
using System.Text;

namespace TypingSpeed
{
    public class Program
    {
        static string text = "";
        public static bool isOn = true;
        static Record name = new(DateTime.Now, "", 0, 0.00, 0);
        static DateTime dateTime { get; set; }
        static DateTime dt { get; set; }
        static void Main()
        {
            isOn = true;
            Table.ReadRecords();

            var builder = new StringBuilder();
            builder.AppendFormat("{0} {1} {2} {3} {4} {5}"
            , "В поезде едут 3 юзера и 3 программиста. У юзеров 3 билета, у программистов 1.Заходит контроллер.",
            "Юзеры показывают билеты, программисты прячутся в туалет. Контроллер стучится в туалет, оттуда высовывается рука с билетом.",
            " Программисты едут дальше. На обратном пути.У юзеров 1 билет, у программистов ни одного. Заходит контроллер. ",
            "Юзеры прячутся в туалет. Один из программистов стучит, из туалета высовывается рука с билетом. ",
            "Программисты забирают билет и прячутся в соседний туалет. Юзеров ссаживают с поезда. ",
            "Вывод — не всякий алгоритм, доступный программисту, доступен юзеру."
            );
            text = builder.ToString();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Введите ваше имя для таблицы рекордов <3");
            Console.WriteLine(new string('-', 120) + "\n");
            Console.SetCursorPosition(1, 2);


            var consoleName = Console.ReadLine();
            name.Name = consoleName;
            if (consoleName == "")
            {
                name.Name = "Барашек";

                Console.Clear();
                Console.WriteLine("Предустановлено имя 'Барашек', но ВЫ можете ввести своё имя.");
                Console.WriteLine(new string('-', 120) + "\n");

                consoleName = Console.ReadLine();
                if (consoleName != "")
                {
                    name.Name = consoleName;
                }
            }

            Console.Clear();
            Console.WriteLine("Нажмите Enter чтобы начать");

            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.Enter)
            {
                Console.Clear();

                Thread t = new Thread(Time);
                t.IsBackground = true;
                t.Start();

                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(text);

                TextWorker.WriteText(text);

                isOn = false;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Стоп!");
                Thread.Sleep(5000);

                TimeSpan ts = dt.Subtract(dateTime.AddMinutes(-1));
                name.SymbolPerMinute = Convert.ToInt32(TextWorker.indexText * (60 / ts.TotalSeconds));
                name.SymbolPerSecond = TextWorker.indexText / ts.TotalSeconds - ((TextWorker.indexText / ts.TotalSeconds) % 0.01);
                name.CountError = TextWorker.countError;
                Table.AddItem(dateTime, name.Name, name.SymbolPerMinute, name.SymbolPerSecond, name.CountError);
                Table.SaveRecord();

                PrintGameResult();
                EndGame();
            }

        }
        public static void Time()
        {
            dateTime = DateTime.Now;
            dt = dateTime.AddMinutes(-1);

            while (dateTime >= dt && isOn)
            {
                var ticks = (dateTime - dt).Ticks;
                Console.SetCursorPosition(0, 10);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(new DateTime(ticks).ToString("ss"));
                Thread.Sleep(1000);
                dt = dt.AddSeconds(1);
            }
            isOn = false;
        }
        static void PrintGameResult()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("{0,65}", "Таблица рекордов");
            Console.WriteLine(new string('-', Console.WindowWidth));
            Console.WriteLine("| {0,-25} | {1,-15} | {2,20} | {3,20} | {4,10} |",
                "Дата",
                "Имя",
                "Знаков в минуту",
                "Знаков в секунду",
                "Ошибки"
            );
            Console.WriteLine(new string('-', Console.WindowWidth));
            foreach (Record name in Table.records)
            {
                Console.WriteLine("| {0,-25} | {1,-15} | {2,20} | {3,20} | {4,10} |",
                name.Times,
                name.Name,
                name.SymbolPerMinute,
                name.SymbolPerSecond,
                name.CountError
            );
            }
        }
        static void EndGame()
        {
            Console.WriteLine(
                "\nЧтобы вернуться обратно в игру, нажмите Escape" +
                "\nЧтобы закончить программу, нажмите F1"
            );

            ConsoleKeyInfo cki = new ConsoleKeyInfo();
            while (true)
            {
                if (Console.KeyAvailable == true)
                {
                    cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.F1)
                    {
                        break;
                    }
                }
            }
            Console.Clear();

            if (cki.Key == ConsoleKey.Escape)
            {
                Main();
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}