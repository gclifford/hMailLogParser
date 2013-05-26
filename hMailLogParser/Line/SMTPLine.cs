using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace hMailLogParser.Line
{
    public enum SMTPStatusLevel
    {
        Normal = 0,
        Warning = 1,
        Error = 2
    }

    public abstract class SMTPLine : LogLine
    {
        public SMTPLine(string[] columns)
            : base(columns)
        {

        }

        protected void ParseMessage(string message)
        {
            var match = CompiledRegex.SMTPMessage.Match(message);
            var direction = match.Groups["Direction"];
        }

        public int SMTPStatusCode { get; set; }
        public SMTPStatusLevel StatusLevel { get; set; }
        public string ParsedMessage { get; set; }
        public string IPAddress { get; set; }
    }
}
