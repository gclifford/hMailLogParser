using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hMailLogViewer
{
    public class CodeTimer : IDisposable
    {
        readonly System.Diagnostics.Stopwatch _stopWatch;
        readonly System.Windows.Controls.TextBlock _control;

        public CodeTimer()
            :this(null)
        {
            
        }

        public CodeTimer(System.Windows.Controls.TextBlock text)
        {
            _stopWatch = new System.Diagnostics.Stopwatch();
            _control = text;
            _stopWatch.Start();
        }

        public void Dispose()
        {
            _stopWatch.Stop();
            string time = _stopWatch.Elapsed.ToString();

            if (_control != null)
            {
                _control.Text = time;
            }
            else
            {
                System.Diagnostics.Trace.WriteLine(string.Format("Execution Time: {0}s", time));
            }
        }
    }
}
