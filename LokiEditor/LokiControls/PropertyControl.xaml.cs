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
using System.Windows.Threading;
using Loki2D.Core.Attributes;
using Loki2D.Core.GameObject;
using LokiEditor.Game;
using LokiEditor.Windows;
using Underline = MaterialDesignThemes.Wpf.Underline;

namespace LokiEditor.LokiControls
{
    /// <summary>
    /// Interaction logic for PropertyControl.xaml
    /// </summary>
    public partial class PropertyControl : UserControl
    {
        public static PropertyControl Instance;
        public PropertyControl()
        {
            InitializeComponent();
            Instance = this; 
        }

        private bool _loading;
        private Entity _entity;

        public void SetInspector(Entity entity)
        {
            _entity = entity;

            _loading = true; 
            this.Dispatcher.Invoke(() =>
            {
                ContentGrid.Children.Clear();
                NamePanel.Children.Clear();

                var entityName = new TextBlock();
                entityName.Text = "Name:";
                entityName.FontSize = 15;
                entityName.HorizontalAlignment = HorizontalAlignment.Left;
                entityName.VerticalAlignment = VerticalAlignment.Bottom;
                entityName.Margin = new Thickness(5, 0, 0, 0);

                var nameTextBox = new TextBox();
                nameTextBox.FontSize = 15;
                nameTextBox.HorizontalAlignment = HorizontalAlignment.Right;
                nameTextBox.Margin = new Thickness(0, 0, 30, 0);
                nameTextBox.TextChanged += (sender, args) =>
                {
                    var editEvent = new EditedEntityArgs();
                    editEvent.EditedEntity = entity;
                    
                    LokiGame.EditedEntityHandler?.Invoke(this, editEvent);
                };

                var nameBinding = new Binding("Name");
                nameBinding.Source = entity;
                nameTextBox.SetBinding(TextBox.TextProperty, nameBinding);

                var nameGrid = new Grid();
                nameGrid.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = new GridLength(1, GridUnitType.Auto)
                });
                nameGrid.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = new GridLength(1, GridUnitType.Star)
                });
                nameGrid.Margin = new Thickness(0, 0, 0, 10);
                nameGrid.Children.Add(entityName);
                nameGrid.Children.Add(nameTextBox);

                Grid.SetColumn(entityName, 0);
                Grid.SetColumn(nameTextBox, 1);

                NamePanel.Children.Add(nameGrid);
                NamePanel.Margin = new Thickness(0, 15, 0, 0);


                foreach (var component in entity.Components)
                {
                    var componentName = new TextBlock();
                    componentName.Margin = new Thickness(5, 0, 0, 0);
                    componentName.FontSize = 20;
                    componentName.Text = component.GetType().Name;

                    var componentAttributes = component.GetType().GetCustomAttributes(typeof(EditorInspectable), true);
                    foreach (var componentAttribute in componentAttributes)
                    {
                        var attribute = ((EditorInspectable)componentAttribute);
                        if (attribute.DisplayName != null)
                        {
                            componentName.Text = attribute.DisplayName;
                        }
                    }

                    componentName.TextDecorations = TextDecorations.Underline;

                    ContentGrid.Children.Add(componentName);



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
                        propertyTextBox.TextChanged += (sender, args) =>
                        {
                            var editEvent = new EditedEntityArgs();
                            editEvent.EditedEntity = entity;

                            LokiGame.EditedEntityHandler?.Invoke(this, editEvent);
                        };

                        var valueBinding = new Binding(property.Name);
                        valueBinding.Source = component;
                        propertyTextBox.SetBinding(TextBox.TextProperty, valueBinding);


                        propertyGrid.Children.Add(propertyName);
                        propertyGrid.Children.Add(propertyTextBox);
                        Grid.SetColumn(propertyName, 0);
                        Grid.SetColumn(propertyTextBox, 1);

                        ContentGrid.Children.Add(propertyGrid);

                    }
                }


                Color color = (Color)ColorConverter.ConvertFromString("#FF505050");

                var button = new Button();
                button.Content = "Add Component";
                button.FontWeight = FontWeights.Bold;

                button.Foreground = Brushes.White;
                button.Background = new SolidColorBrush(color);

                button.ContextMenu = new ComponentsMenu();
                button.Click += (sender, args) => { button.ContextMenu.IsOpen = true; };

                ContentGrid.Children.Add(button);


            }, DispatcherPriority.Background);

            _loading = false;
        }

        private void EditEntity(object sender, RoutedEventArgs e)
        {
            if (_entity != null)
            {
                var editor = new EntityEditor(_entity);
                editor.Show();
            }
        }

        private void DeleteEntity(object sender, RoutedEventArgs e)
        {
            _entity?.Destroy();
        }
    }

    public class ComponentsMenu : ContextMenu
    {
        public ComponentsMenu()
        {
            var tests = new ObservableCollection<string>()
            {
                "test 1",
                "test 2",
                "test 3",
                "test 4",
                "test 5",
                "test 6"
            };

            var engineComponents = new MenuItem();
            engineComponents.Header = "Engine Components";

            var gameComponents = new MenuItem();
            gameComponents.Header = "Local Components";

            Items.Add(engineComponents);
            Items.Add(gameComponents);


        }
    }
}
