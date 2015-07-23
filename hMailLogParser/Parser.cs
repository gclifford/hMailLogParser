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
            var lines = new List<LogLine>();
            while (!stream.EndOfStream)
            {
                string line = await stream.ReadLineAsync();
                string[] columns = line.Split('\t');
                lines.Add(this.CreateLine(columns));
            }

            return lines;
        }

        LogLine CreateLine(string[] columns)
        {
            LogLine line = null;
            var type = columns[0].Sanitize();
            switch (type)
            {
                case SMTPDaemonLine.LINE_TYPE:
                    line = new SMTPDaemonLine();
                    break;
                case SMTPClientLine.LINE_TYPE:
                    line = new SMTPClientLine();
                    break;
                case TCPIPLine.LINE_TYPE:
                    line = new TCPIPLine();
                    break;
                case ApplicationLine.LINE_TYPE:
                    line = new ApplicationLine();
                    break;
                case POP3DaemonLine.LINE_TYPE:
                    line = new POP3DaemonLine();
                    break;
                default:
                    line = new UnknownLine();
                    break;
            }

            line.Parse(columns);

            return line;
        }
    }
}
