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
        private IList<string> logs = new List<string>();
        public LogControl()
        {
            InitializeComponent();

            ListBox.ItemsSource = logs;
            Debug.Log += LogToConsole;
        }

        private void LogToConsole(object sender, LogEvent e)
        {
            logs.Add(e.Log.ToString());
            ListBox.Items.Refresh();
        }
    }
}
