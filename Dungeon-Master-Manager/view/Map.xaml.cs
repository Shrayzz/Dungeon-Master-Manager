using System.Windows;
using System.Windows.Controls;


namespace Dungeon_Master_Manager.view
{
    /// <summary>
    /// Logique d'interaction pour Map.xaml
    /// </summary>
    public partial class Map
    {
        public Map()
        {
            InitializeComponent();
            LoadMissions();
            RenderMission(((App)Application.Current).SelectedMission);
        }

        private void LoadMissions()
        {
            
            if (((App)Application.Current).Missions.Count == 0) return;

            for (int i = 0; i < ((App)Application.Current).Missions.Count; i++)
            {
                var m = ((App)Application.Current).Missions[i];
                var b = new Button
                {
                    Style = ((Style)this.Resources["MissionDot"]),
                    Width = 7,
                    Height = 7,
                    Tag = i.ToString(),
                    Margin = new Thickness(m.MarginLeft, m.MarginTop, m.MarginRight, m.MarginBottom)
                };

                b.Click += SelectMissionEvent;
                MapDots.Children.Add(b);
            }
        }

        private void RenderMission(uint missionId)
        {
            var m = ((App)Application.Current).SelectMission(missionId);
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

        private void SelectMissionEvent(object? caller, EventArgs e)
        {
            var missionId = Convert.ToUInt32(((Button)caller).Tag);
            RenderMission(missionId);
            
        }

        private void MissionViewBackButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}