using System;
using System.Collections.Generic;
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
        public AssetControl()
        {
            InitializeComponent();
            Instance = this;
        }

        public void LoadAssets(LokiData lokiData)
        {
            var path = Path.GetDirectoryName(AssetManagement.Instance.LokiFilePath);
            path = Path.Combine(path, lokiData.Project);
            var assembly = Assembly.LoadFile(path);

            foreach (var type in assembly.GetTypes())
            {
                if (type.BaseType == typeof(Entity))
                {
                    Debug.WriteLog($"FOUND ENTITY: {type.Name}");
                }
            }
        }
    }
}
