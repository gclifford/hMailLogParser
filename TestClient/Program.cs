using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using hMailLogParser;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser p = new Parser();

            var lines = p.Parse("log.log");
            Console.WriteLine(lines.Count());

            var grouped = lines.GroupBy(x => x.SessionID);
            Console.WriteLine(grouped.Count());
        }
    }
}
