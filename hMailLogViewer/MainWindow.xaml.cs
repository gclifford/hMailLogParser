using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

// hMailLogParser
using hMailLogParser;
using hMailLogParser.Line;
using System.Threading.Tasks;

namespace hMailLogViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string _DEFAULT_HMAIL_LOG_PATH = "C:\\Program Files\\hMailServer\\Logs";

        ICollectionView defaultView;

        public MainWindow()
        {
            InitializeComponent();
        }

       async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var args = Environment.GetCommandLineArgs();
            if (args.Length == 2)
            {
                await this.LoadFile(args[1]);
            }
        }

        async void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog();

            string defaultLogDirectory = _DEFAULT_HMAIL_LOG_PATH;
            if (!System.IO.Directory.Exists(defaultLogDirectory))
                defaultLogDirectory = Environment.CurrentDirectory;

            dlg.InitialDirectory = defaultLogDirectory;
            dlg.Multiselect = true;
            dlg.Filter = "Log|*.log|All Files|*.*";
            dlg.DefaultExt = ".log"; // Default file extension

            // Show open file dialog box
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Open document 
                await this.LoadFile(dlg.FileNames);
            }
        }

        async protected Task LoadFile(params string[] filenames)
        {
            using (new CodeTimer(this.tbExectuionTime))
            {
                var p = new Parser();

                var lines = new List<LogLine>();
                foreach (var filename in filenames)
                {
                    var l = await p.Parse(filename);
                    lines.AddRange(l);
                }

                this.defaultView = CollectionViewSource.GetDefaultView(lines);
                this.defaultView.Filter =
                    w =>
                    {
                        bool statusFilter = true;
                        var smtpLine = w as LogLine;
                        if (smtpLine == null)
                        {               
                            switch (smtpLine.MessageStatus)
                            {
                                case MessageStatusLevel.Error:
                                    statusFilter = tbFilterError.IsChecked.GetValue();
                                    break;
                                case MessageStatusLevel.Warning:
                                    statusFilter = tbFilterTransient.IsChecked.GetValue();
                                    break;
                                case MessageStatusLevel.Infomation:
                                    statusFilter = tbFilterNormal.IsChecked.GetValue();
                                    break;
                            }

                        }

                        var line = w as LogLine;
                        return line.Message.Contains(this.txtSearch.Text) && statusFilter;
                    };

                this.dgLogViewer.ItemsSource = this.defaultView;
                this.tbNumberOfLines.Text = lines.Count().ToString();
            }

            this.txtSearch.IsEnabled = true;
            this.btnSearch.IsEnabled = true;
        }

        protected void Search()
        {
            using (new CodeTimer(this.tbExectuionTime))
            {
                this.defaultView.Refresh();
            }
        }

        void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            this.Search();
        }

        void TextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter)
                return;
            
            e.Handled = true;
            this.Search();
        }

        void dgLogViewer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            using (new CodeTimer())
            {
                var line = this.dgLogViewer.SelectedItem as SessionBasedLine;
                if (line != null)
                {
                    var items = this.defaultView.SourceCollection.OfType<SessionBasedLine>().Where(x => x.SessionID == line.SessionID).ToArray();
                    var dialog = new winLineDetails();
                    dialog.Owner = this;
                    dialog.Line = line;
                    dialog.RelatedLines = items;
                    dialog.Show();
                }
            }
        }

        void tbFilter_Checked(object sender, RoutedEventArgs e)
        {
            this.Search();
        }

        void MenuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new winAbout();
            dialog.Owner = this;
            dialog.ShowDialog();
        }
    }
}
