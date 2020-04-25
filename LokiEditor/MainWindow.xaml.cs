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
using Loki2D.Core.Scene;
using LokiEditor.Game;
using LokiEditor.LokiControls;
using LokiEditor.Systems;
using Microsoft.Win32;

namespace LokiEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static EventHandler<LoadLokiProjectEvent> NewProjectEvent { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            AssetManagement.Instance = new AssetManagement();
            LoadSceneButton.IsEnabled = false;
            NewSceneButton.IsEnabled = false;

            SaveSceneButton.IsEnabled = true;

        }

        private void LoadProjectClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Loki Project Files|*.loki";
            openFileDialog.Multiselect = false;

            var fileName = "";
            if (openFileDialog.ShowDialog() == true)
            {
                fileName = openFileDialog.FileName;
                var loadProject = new LoadLokiProjectEvent {FilePath = fileName};
                NewProjectEvent.Invoke(this, loadProject);

                LoadSceneButton.IsEnabled = true;
                NewSceneButton.IsEnabled = true;
                SaveSceneButton.IsEnabled = true; 
            }
            else
            {
                return;
            }
        }

        private void CloseApplicationClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LoadScene(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Loki Scene Files|*.sloki";
            openFileDialog.Multiselect = false;

            var fileName = "";
            if (openFileDialog.ShowDialog() == true)
            {
                fileName = openFileDialog.FileName;

                LokiGame.Instance.LoadScene(fileName, AssetControl.Instance.LoadedAssembly);
            }
            else
            {
                return;
            }
        }

        private void SaveSceneClick(object sender, RoutedEventArgs e)
        {
            if (SceneManagement.Instance.CurrentScene.Name != null)
            {
                SceneManagement.Instance.SaveScene();
            }
        }
    }
}
