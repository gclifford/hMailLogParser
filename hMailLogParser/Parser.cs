using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using hMailLogParser.Line;
using System.Threading.Tasks;

namespace hMailLogParser
{
    public class Parser
    {
        async public Task<IEnumerable<LogLine>> Parse(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                return await this.Parse(sr);
            }
        }

        async public Task<IEnumerable<LogLine>> Parse(StreamReader stream)
        {
            List<LogLine> lines = new List<LogLine>();
            while (!stream.EndOfStream)
            {
                string line = await stream.ReadLineAsync();
                string[] columns = line.Split('\t');
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
                case SMTPDaemonLine.LINE_TYPE:
                    line = new SMTPDaemonLine(columns);
                    break;
                case SMTPClientLine.LINE_TYPE:
                    line = new SMTPClientLine(columns);
                    break;
                case TCPIPLine.LINE_TYPE:
                    line = new TCPIPLine(columns);
                    break;
                case ApplicationLine.LINE_TYPE:
                    line = new ApplicationLine(columns);
                    break;
                case POP3DaemonLine.LINE_TYPE:
                    line = new POP3DaemonLine(columns);
                    break;
                default:
                    line = new UnknownLine(columns);
                    break;
            }

            return line;
        }
    }
}
