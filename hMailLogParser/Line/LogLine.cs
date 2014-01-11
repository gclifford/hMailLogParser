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

        protected abstract string GetLineType();

        public string LineType { get { return GetLineType(); } }
        public int ThreadID { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
    }
}
