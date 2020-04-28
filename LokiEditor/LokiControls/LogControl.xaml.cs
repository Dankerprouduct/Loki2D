using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Log> logs = new ObservableCollection<Log>();
        public LogControl()
        {
            InitializeComponent();
            
            Logs.ItemsSource = logs;
            Debug.Logged += LogToConsole;
        }

        private void LogToConsole(object sender, LogEvent e)
        {
            logs.Add(new Log(e.Log.ToString()));
            LogScrollViewer.ScrollToBottom();
            //Logs.Items.Refresh();
        }

    }

    public class Log
    {
        public string Text { get; set; }

        private string _time;
        public string Time
        {
            get { return $"[{_time}] "; }
            set => value = _time;
        }

        public Log(string text)
        {
            Text = text;
            _time = DateTime.Now.ToString("t");
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
