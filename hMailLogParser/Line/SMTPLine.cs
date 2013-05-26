using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hMailLogParser.Line
{
    public abstract class SMTPLine : LogLine
    {
        public SMTPLine(string[] columns)
            : base(columns)
        {

        }

        protected void ParseMessage()
        {

        }
    }
}
