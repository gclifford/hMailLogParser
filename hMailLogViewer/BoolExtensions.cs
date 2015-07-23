using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hMailLogViewer
{
    public static class BoolExtensions
    {
        public static bool GetValue(this bool? input)
        {
            return input.HasValue && input.Value;
        }
    }
}
