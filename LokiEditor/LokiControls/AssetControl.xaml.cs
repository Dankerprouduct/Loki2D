using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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
using Loki2D.Core.GameObject;
using Loki2D.Core.Utilities;
using Loki2D.Systems;
using LokiEditor.Game;
using LokiEditor.Systems;
using Path = System.IO.Path;

namespace LokiEditor.LokiControls
{
    /// <summary>
    /// Interaction logic for AssetControl.xaml
    /// </summary>
    public partial class AssetControl : UserControl
    {
        public static AssetControl Instance;
        public ObservableCollection<Asset> Assets { get; set; } = new ObservableCollection<Asset>();
        public Assembly LoadedAssembly; 

        public Asset SelectedAsset { get; set; }
        public AssetControl()
        {
            InitializeComponent();
            AssetList.SelectionMode = SelectionMode.Single;
            
            Instance = this;
            AssetList.ItemsSource = Assets;
        }

        public void LoadAssets(LokiData lokiData)
        {
            var path = Path.GetDirectoryName(AssetManagement.Instance.LokiFilePath);
            var assemblyPath = Path.Combine(path, lokiData.Project);
            var assembly = Assembly.LoadFile(assemblyPath);
            LoadedAssembly = assembly;
            // load entity types
            foreach (var type in assembly.GetTypes())
            {
                if (type.BaseType == typeof(Entity))
                {
                    var asset = new Asset()
                    {
                        Name = type.Name,
                        FullType = type.FullName
                    };

                    // checks if the asset hasn't already been added
                    if (Assets.All(i => i.FullType != asset.FullType))
                    {
                        Assets.Add(asset);
                    }
                }
            }

            // load images
            TextureManager.Instance.LoadFolder(Path.Combine(path, lokiData.Content));


        }

        private void AssetList_OnSelected(object sender, RoutedEventArgs e)
        {
        }

        private void AssetList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var asset = (Asset) e.AddedItems[0];
            Console.WriteLine(asset);
            SelectedAsset = asset;
        }
    }
}
