using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hMailLogParser.Line
{
    public class UnknownLine : LogLine
    {
        public UnknownLine(string[] columns)
            : base(columns)
        {

        }

        protected override void Parse(string[] columns)
        {
            this.Message = string.Join("\t", columns);
        }
    }
}
