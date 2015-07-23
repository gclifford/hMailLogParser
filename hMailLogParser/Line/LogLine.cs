using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hMailLogParser.Line
{
    public enum MessageStatusLevel
    {
        Infomation = 0,
        Warning = 1,
        Error = 2
    }

    public abstract class LogLine
    {
        internal abstract void Parse(string[] columns);

        protected abstract string GetLineType();

        public string LineType { get { return GetLineType(); } }
        public int ThreadID { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public MessageStatusLevel MessageStatus { get; set; }
    }
}
