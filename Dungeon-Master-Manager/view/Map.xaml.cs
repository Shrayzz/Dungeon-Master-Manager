using Dungeon_Master_Manager.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            LoadMissions();
        }

        private void LoadMissions()
        {
            string jsonString = File.ReadAllText("../../../assets/missions.json");
            ((App)Application.Current).Missions = JsonSerializer.Deserialize<List<model.Mission>>(jsonString);

            if (((App)Application.Current).Missions.Count == 0) return;

            for (int i = 0; i < ((App)Application.Current).Missions.Count; i++)
            {
                model.Mission m = ((App)Application.Current).Missions[i];
                Button b = new Button();
                b.Style = (Style)this.Resources["MissionDot"];
                b.Width = 7;
                b.Height = 7;
                b.Tag = i.ToString();

                b.Margin = new Thickness(m.MarginLeft, m.MarginTop, m.MarginRight, m.MarginBottom);
                b.Click += SelectMissionEvent;
                MapDots.Children.Add(b);
            }
        }

        public void SelectMissionEvent(Object? caller, EventArgs e)
        {
            uint mission_id = Convert.ToUInt32(((Button)caller).Tag);
            model.Mission m = ((App)Application.Current).SelectMission(mission_id);
            UIMissionName.Text = "Nom de la mission :" + m.Name;
            DescriptionTextBlock.Text = m.Description;

            RewardsTextBlock.Text = $"💰 Gold :  {m.RewardGold}\n";
            RewardsTextBlock.Text += "----------\n";
            foreach (var i in m.RewardItems)
            {
                RewardsTextBlock.Text += i + "\n";
            }
            RewardsTextBlock.Text += "----------\n";
            foreach (var c in m.RewardCharacters)
            {
                RewardsTextBlock.Text += c + "\n";
            }
        }


        public class Mission
        {
            public string? Name { get; set; }
            public string? Description { get; set; }
            public string? Rewards { get; set; }
        }

        private void MissionViewBackButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}