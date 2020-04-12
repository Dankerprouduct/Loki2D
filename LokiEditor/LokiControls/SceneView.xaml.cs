﻿using System;
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

namespace LokiEditor.LokiControls
{
    /// <summary>
    /// Interaction logic for SceneView.xaml
    /// </summary>
    public partial class SceneView : UserControl
    {
        public SceneView()
        {
            InitializeComponent();
        }

        public enum EditType
        {
            Selection,
            Transform,
            Brush,
            Magnet
        }

        public static EditType CurrentEditType;

        private void SelectionButtonClick(object sender, RoutedEventArgs e)
        {
            ClearColors();
            CurrentEditType = EditType.Selection;
            SelectionButton.Foreground = Brushes.Green;
        }

        private void TransformButtonClick(object sender, RoutedEventArgs e)
        {
            ClearColors();
            CurrentEditType = EditType.Transform;
            TransformButton.Foreground = Brushes.Green;
        }

        private void BrushButtonClick(object sender, RoutedEventArgs e)
        {
            ClearColors();
            CurrentEditType = EditType.Brush;
            BrushButton.Foreground = Brushes.Green;
        }

        private void MagnetButtonClick(object sender, RoutedEventArgs e)
        {
            ClearColors();
            CurrentEditType = EditType.Magnet;
            MagnetButton.Foreground = Brushes.Green;
        }

        public void ClearColors()
        {
            TransformButton.Foreground = Brushes.White;
            SelectionButton.Foreground = Brushes.White;
            TransformButton.Foreground = Brushes.White;
            BrushButton.Foreground = Brushes.White;
            MagnetButton.Foreground = Brushes.White;
        }
    }
}
