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
using System.Windows.Shapes;
using Loki2D.Core.Attributes;
using Loki2D.Core.Component;
using Loki2D.Core.GameObject;

namespace LokiEditor.Windows
{
    /// <summary>
    /// Interaction logic for EntityEditor.xaml
    /// </summary>
    public partial class EntityEditor : Window
    {
        public EntityEditor()
        {
            InitializeComponent();
        }

        private Entity _entity;
        public EntityEditor(Entity entity)
        {
            InitializeComponent();
            _entity = entity;

            EntityNameTextbox.Text = entity.Name;
            PopulateComponents(entity);
            Components.SelectionChanged += Components_OnSelected;
        }
        
        public void PopulateComponents(Entity entity)
        {
            Components.ItemsSource = entity.Components;
        }

        private void Components_OnSelected(object sender, SelectionChangedEventArgs e)
        {
            var component = (Component)e.AddedItems[0];
            ComponentGrid.Children.Clear();
            
            foreach (var property in component.GetType().GetProperties())
            {
                var canEdit = Attribute.IsDefined(property, typeof(EditorInspectable));
                if (!canEdit) continue;

                var propertyGrid = new Grid();
                propertyGrid.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = new GridLength(1, GridUnitType.Auto)
                });
                propertyGrid.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = new GridLength(1, GridUnitType.Star)
                });


                var propertyName = new TextBlock();
                propertyName.FontSize = 15;
                propertyName.Text = property.Name;
                var attribute = Attribute.GetCustomAttribute(property, typeof(EditorInspectable)) as EditorInspectable;
                if (attribute.DisplayName != null)
                {
                    propertyName.Text = attribute.DisplayName;
                }


                propertyName.VerticalAlignment = VerticalAlignment.Bottom;
                propertyName.Margin = new Thickness(25, 0, 0, 5);


                var propertyTextBox = new TextBox();
                propertyTextBox.FontSize = 15;
                propertyTextBox.VerticalAlignment = VerticalAlignment.Bottom;
                propertyTextBox.HorizontalAlignment = HorizontalAlignment.Right;
                propertyTextBox.Margin = new Thickness(0, 5, 20, 5);
                

                var valueBinding = new Binding(property.Name);
                valueBinding.Source = component;
                propertyTextBox.SetBinding(TextBox.TextProperty, valueBinding);


                propertyGrid.Children.Add(propertyName);
                propertyGrid.Children.Add(propertyTextBox);
                Grid.SetColumn(propertyName, 0);
                Grid.SetColumn(propertyTextBox, 1);

                ComponentGrid.Children.Add(propertyGrid);
            }
        }
    }
}
