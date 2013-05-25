using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hMailLogParser.Line
{
    public abstract class LogLine
    {
        public LogLine(string[] columns)
        {
            this.Parse(columns);
        }

        protected abstract void Parse(string[] columns);

        public int Number1 { get; set; }
        public int SessionID { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
    }
}
