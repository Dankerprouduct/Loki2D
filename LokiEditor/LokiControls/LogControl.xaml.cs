using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Loki2D.Core.Utilities;

namespace LokiEditor.LokiControls
{
    /// <summary>
    /// Interaction logic for LogControl.xaml
    /// </summary>
    public partial class LogControl : UserControl
    {
        private IList<Log> logs = new List<Log>();
        public LogControl()
        {
            InitializeComponent();
            
            Logs.ItemsSource = logs;
            Debug.Logged += LogToConsole;
        }

        private void LogToConsole(object sender, LogEvent e)
        {
            logs.Add(new Log(e.Log.ToString()));
            Logs.Items.Refresh();
        }

    }

    public class Log
    {
        public string Text { get; set; }

        public Log(string text)
        {
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
