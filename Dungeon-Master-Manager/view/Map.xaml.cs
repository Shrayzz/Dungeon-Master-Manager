using Dungeon_Master_Manager.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
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
            LoadButtons();
        }
        private void LoadButtons()
        {
            string jsonString = File.ReadAllText("../assets/missions.json");
            var missions = JsonSerializer.Deserialize<Dictionary<string, Mission>>(jsonString);

            int index = 1;
            // Ajouter la récupération des buttons concernant les missions
            // Syncroniser les index par rapport aux JSON
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Mission mission)
            {
                // Insérer les infos dans les boites de texte
            }
        }
        public class Mission
        {
            public string? Name { get; set; }
            public string? Description { get; set; }
            public string? Rewards { get; set; }
        }
    }
}
