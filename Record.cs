using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordObject
{
    public class Record
    {
        public Record(DateTime times, string name, int minute, double second, int countError)
        {
            this.Times = times;
            this.Name = name;
            this.SymbolPerMinute = minute;
            this.SymbolPerSecond = second;
            this.CountError = countError;
        }
        public DateTime Times { get; set; }
        public string Name { get; set; }
        public int SymbolPerMinute { get; set; }
        public double SymbolPerSecond { get; set; }
        public int CountError { get; set; }
    }
}