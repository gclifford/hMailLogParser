using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hMailLogParser.Line
{
    public class UnknownLine : LogLine
    {
        internal override void Parse(string[] columns)
        {
            this.Message = string.Join("\t", columns);
        }

        const string _LINE_TYPE = "UNKNOWN";
        protected override string GetLineType()
        {
            return _LINE_TYPE;
        }
    }
}
