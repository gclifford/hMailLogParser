using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace hMailLogParser
{
    public static class CompiledRegex
    {
        public static readonly Regex SMTPMessage = new Regex("^(?<Direction>(SENT|RECEIVED)): (?<SMTPStatus>\\d{3})?(\\s?(?<Message>.*))$", RegexOptions.Compiled);
    }
}
