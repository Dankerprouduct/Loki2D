﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using Loki2D.Core.Utilities;
using LokiEditor.Game;
using LokiEditor.LokiControls;
using LokiEditor.Systems;
using Microsoft.Win32;
using Path = System.IO.Path;

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


                FileMenuText.Text = Path.GetFileNameWithoutExtension(AssetManagement.LokiData.Project);
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
            if(SceneManagement.Instance.CurrentScene == null)
                return;

            if (SceneManagement.Instance.CurrentScene.Name != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Loki Scene Files|*.sloki";
                if (saveFileDialog.ShowDialog() == true)
                {
                    Debug.Log($"Saved {saveFileDialog.FileName}");
                    SceneManagement.Instance.CurrentScene.Name = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
                    SceneManagement.Instance.SaveScene(
                        SceneManagement.Instance.CurrentScene,
                        saveFileDialog.FileName);
                }
            }

            if (SceneManagement.Instance.CurrentScene.Name != null)
            {
                SceneManagement.Instance.SaveScene();
            }
        }
    }
}
