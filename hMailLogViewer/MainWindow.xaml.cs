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

namespace hMailLogViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICollectionView defaultView;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.InitialDirectory = Environment.CurrentDirectory;
            dlg.Filter = "Log|*.log|All Files|*.*";
            dlg.DefaultExt = ".log"; // Default file extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                this.LoadFile(filename);
            }
        }

        protected void LoadFile(string filename)
        {
            using (new CodeTimer(this.tbExectuionTime))
            {
                Parser p = new Parser();

                var lines = p.Parse(filename);

                this.defaultView = CollectionViewSource.GetDefaultView(lines);
                this.defaultView.Filter =
                    w => ((LogLine)w).Message.Contains(this.txtSearch.Text);

                this.dgLogViewer.ItemsSource = this.defaultView;
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            this.Search();
        }

        private void TextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter)
                return;
            
            e.Handled = true;
            this.Search();
        }

        private void dgLogViewer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            using (new CodeTimer())
            {
                var line = this.dgLogViewer.SelectedItem as LogLine;
                var items = this.dgLogViewer.ItemsSource.OfType<LogLine>().Where(x => x.SessionID == line.SessionID).ToArray();
                winLineDetails dialog = new winLineDetails();
                dialog.Owner = this;
                dialog.Line = line;
                dialog.RelatedLines = items;
                dialog.Show();
            }
        }
    }
}
