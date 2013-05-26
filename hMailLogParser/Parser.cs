using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using hMailLogParser.Line;

namespace hMailLogParser
{
    public class Parser
    {
        public IEnumerable<LogLine> Parse(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                return this.Parse(sr);
            }
        }

        public IEnumerable<LogLine> Parse(StreamReader stream)
        {
            List<LogLine> lines = new List<LogLine>();
            while (!stream.EndOfStream)
            {
                string[] columns = stream.ReadLine().Split('\t');
                lines.Add(this.CreateLine(columns));
            }

            return lines;
        }

        private LogLine CreateLine(string[] columns)
        {
            LogLine line = null;
            var type = columns[0].Sanitize();
            switch (type)
            {
                case "SMTPD":
                    line = new SMTPDaemonLine(columns);
                    break;
                case "SMTPC":
                    line = new SMTPClientLine(columns);
                    break;
                default:
                    line = new UnknownLine(columns);
                    break;
            }

            return line;
        }
    }
}
