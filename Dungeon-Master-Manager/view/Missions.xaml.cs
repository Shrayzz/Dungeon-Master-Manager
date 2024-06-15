using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Dungeon_Master_Manager.model;

namespace Dungeon_Master_Manager.view;

/// <summary>
/// Logique d'interaction pour Missions.xaml
/// </summary>
public partial class Missions
{
    private void RenderTeam()
    {
        CGrid.Children.Clear();
        var playerTeam = ((App)Application.Current).Team;

        for (uint i = 0; i < playerTeam.Length; i++)
        {
            if (playerTeam[i] != null)
            {
                var currentPlayer = playerTeam[i]!;
                var slot = new Image
                {
                    Tag = "character",
                    Uid = i.ToString(),
                    Source = new BitmapImage(new Uri("../assets/man.png", UriKind.Relative)),
                    Height = 140,
                    Cursor = Cursors.Hand,
                    ToolTip = currentPlayer + $"\nClic gauche pour modifier {currentPlayer.Name}\nClic droit pour enlever {currentPlayer.Name} de l'équipe"
                };
                slot.MouseDown += PlayerSlot_OnMouseDown;
                Grid.SetColumn(slot, (int)i);
                CGrid.Children.Add(slot);
                continue;
            }


            var sslot = new Image
            {
                Tag = "plus",
                Uid = i.ToString(),
                Source = new BitmapImage(new Uri("../assets/plus.png", UriKind.Relative)),
                Height = 60,
                Cursor = Cursors.Hand
            };
            sslot.MouseDown += PlayerSlot_OnMouseDown;
            Grid.SetColumn(sslot, (int)i);
            CGrid.Children.Add(sslot);
        }
    }

    public Missions()
    {
        InitializeComponent();
        var missionIndex = ((App)Application.Current).SelectedMission;
        MissionsMissionDetailTextBlock.Text = ((App)Application.Current).Missions[(int)missionIndex].Description;
        RenderTeam();
    }

    private void Image_OpenMissionsDetails(object sender, MouseButtonEventArgs e)
    {
        var map = new view.Map();
        map.ShowDialog();
        var missionIndex = ((App)Application.Current).SelectedMission;
        MissionsMissionDetailTextBlock.Text = ((App)Application.Current).Missions[(int)missionIndex].Description;
    }

    private void PlayerSlot_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        var pslot = Convert.ToUInt32(((Image)sender).Uid);
        var slotEmpty = (string)((Image)sender).Tag == "plus";
        var slotText = slotEmpty ? "no character" : "a character";

        Trace.WriteLine($"Slot {pslot} contains {slotText}");

        if (slotEmpty)
        {
            ((App)Application.Current).Intent = Intent.Select;

            for (var i = 0; i < Application.Current.Windows.Count; i++)
            {
                if (Application.Current.Windows[i]!.GetType() != typeof(Game)) continue;
                var gameWindow = (Game)(Application.Current.Windows[i])!;
                
                // Hack to not trigger the event, so it does not reset out intent
                gameWindow.MainTabControl.SelectionChanged -= gameWindow.TabControl_SelectionChanged;

                gameWindow.MainTabControl.SelectedIndex = 1;

                gameWindow.MainTabControl.SelectionChanged += gameWindow.TabControl_SelectionChanged;

                gameWindow.ContentFrame.Content = new Characters();
                break;
            }
        }
        else
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                var userIntention = MessageBox.Show("Voulez-vous vraiment supprimer ce personnage de votre équipe ?", "Enlever le joueur de l'équipe ?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (userIntention != MessageBoxResult.Yes) return;
                
                ((App)Application.Current).RemoveTeamMember(pslot);

                // Reset intent just in case
                ((App)Application.Current).Intent = Intent.View;
                // ReRender the team viewer
                RenderTeam();
                return;
            }

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ((App)Application.Current).Intent = Intent.View;
                var characterEditorWindow = new CharacterWindow();
                characterEditorWindow.ShowDialog();
            }
        }
    }

    private void SimulateButtonClicked(object sender, RoutedEventArgs e)
    {
        ((App)Application.Current).StartMission();
    }
}