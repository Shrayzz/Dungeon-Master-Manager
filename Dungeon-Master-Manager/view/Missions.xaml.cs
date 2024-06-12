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
    /// Logique d'interaction pour Missions.xaml
    /// </summary>
    public partial class Missions : Page
    {
        public Missions()
        {
            InitializeComponent();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Image clicked!"); // Placeholder
        }
        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            var image = sender as Image;
            if (image != null)
            {
                if (image.Source == null)
                {
                    MessageBox.Show($"Image source is null. Check the image path. {image.Source}");
                }
            }
        }

        private void Image_Loaded_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
