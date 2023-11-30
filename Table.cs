using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using RecordObject;

namespace RecordReadWrite
{
    public class Table
    {
        static string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        static string path = Path.Combine(docFolder, "Records.json");
        public static List<Record> records = new();

        public static void AddItem(DateTime times, string name, int minute, double second, int countError)
        {
            records.Add(new Record(times, name, minute, second, countError));
        }
        public static void SaveRecord()
        {
            var settings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-ddTHH:mm:ss", FloatParseHandling = FloatParseHandling.Decimal };
            string json = JsonConvert.SerializeObject(records, Formatting.Indented, settings);
            File.WriteAllText(path, json);
        }

        public static void PrintRecord()
        {
            foreach (Record record in records)
            {
                Console.WriteLine("Имя" + record.Name, "Скорость в минутах"
                + record.SymbolPerMinute, "Скорость в секундах" + record.SymbolPerSecond, "Ошибки" + record.CountError);
            }
        }
        public static void ReadRecords()
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                records = JsonConvert.DeserializeObject<List<Record>>(json);
            }


        }

    }

}
