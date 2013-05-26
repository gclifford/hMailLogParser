using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using hMailLogParser.Line;

namespace hMailLogViewer
{
    /// <summary>
    /// Interaction logic for winLineDetails.xaml
    /// </summary>
    public partial class winLineDetails : Window
    {
        public LogLine Line { get; set; }
        public IEnumerable<LogLine> RelatedLines { get; set; }

        public winLineDetails()
        {
            InitializeComponent();
        }
    }
}
