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

namespace Dungeon_Master_Manager.view
{
    /// <summary>
    /// Logique d'interaction pour Map.xaml
    /// </summary>
    public partial class Map : Window
    {
        public Map()
        {
            InitializeComponent();
        }
        private void GeneratePoints()
        {
            // Coordonnées des points verts (extraites du script Python)
            var coordinates = new (double, double)[]
            {
                (50, 710), (691, 704), (184, 644), (600, 608), (354, 600),
                (164, 543), (294, 513), (614, 506), (469, 492), (67, 450),
                (386, 428), (176, 393), (328, 376), (495, 363), (236, 342),
                (605, 311), (336, 255), (221, 233), (626, 226), (538, 194),
                (406, 155), (100, 91), (671, 38)
            };

            for (int i = 0; i < coordinates.Length; i++)
            {
                var coord = coordinates[i];
                AddPoint(i, coord.Item1, coord.Item2);
            }
        }

        private void AddPoint(int index, double x, double y)
        {
            Button pointButton = new Button
            {
                Width = 10,
                Height = 10,
                Background = Brushes.Red,
                Tag = index // Attribuer l'index au Tag pour référence
            };

            // Ajustement de la position pour tenir compte des dimensions de l'image
            double adjustedX = x * 300 / 1024;
            double adjustedY = y * 300 / 1024;

            Canvas.SetLeft(pointButton, adjustedX);
            Canvas.SetTop(pointButton, adjustedY);

            pointButton.Click += PointButton_Click;

            MapCanvas.Children.Add(pointButton);
        }

        private void PointButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                int index = (int)button.Tag;
                MessageBox.Show($"Point index: {index}");
            }
        }
    }
}
