using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
using Loki2D.Core.GameObject;
using LokiEditor.Game;

namespace LokiEditor.LokiControls
{
    /// <summary>
    /// Interaction logic for SceneHierarchy.xaml
    /// </summary>
    public partial class SceneHierarchy : UserControl
    {
        public EntitySceneList EntitySceneList;
        public SceneHierarchy()
        {
            InitializeComponent();
            LokiGame.SceneLoadedHandler += LoadHierarchy;
            LokiGame.EditedEntityHandler += OnEditedEntity;
            LokiGame.AddedEntityHandler += OnAddedEntity;


            TreeView.SelectedItemChanged += SelectedItem;

            EntitySceneList = new EntitySceneList();

            TreeView.ItemsSource = EntitySceneList;
        }

        private void OnAddedEntity(object sender, AddedEntityArgs e)
        {
            EntitySceneList.Add(e.AddedEntity);
        }

        private void SelectedItem(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            PropertyControl.Instance.SetInspector((Entity)e.NewValue);
        }

        private void OnEditedEntity(object sender, EditedEntityArgs e)
        {
            var index = EntitySceneList.IndexOf(e.EditedEntity);
            
        }
        
        private void LoadHierarchy(object sender, LoadedSceneArgs e)
        {
            EntitySceneList.Clear();
            var validCells = e.CellSpacePartition.Cells.Where(i => i.Entities != null);
            var loadedEntities = validCells.SelectMany(cell => cell.Entities).ToList();

            foreach (var entity in loadedEntities)
            {
                EntitySceneList.Add(entity);
            }

        }

    }

    public class EntitySceneList : ObservableCollection<Entity>
    {

        public EntitySceneList()
        {

        }

    }
}
