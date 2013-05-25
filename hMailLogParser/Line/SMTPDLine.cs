using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hMailLogParser.Line
{
    public class SMTPDLine : LogLine
    {
        public SMTPDLine(string[] columns)
            : base(columns)
        {

        }

        protected override void Parse(string[] columns)
        {
            if (columns.Length == 6)
            {
                this.Number1 = int.Parse(columns[1].Sanitize());
                this.SessionID = int.Parse(columns[2].Sanitize());
                this.Date = DateTime.Parse(columns[3].Sanitize());
                this.IPAddress = columns[4].Sanitize();
                this.Message = columns[5].Sanitize();
            }
            else
            {
                throw new Exception("Malformed line");
            }
        }
        
        public string IPAddress { get; set; }
    }
}
