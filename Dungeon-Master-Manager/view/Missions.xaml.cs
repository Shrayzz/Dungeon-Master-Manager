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
    public Missions()
    {
        InitializeComponent();
        var missionIndex = ((App)Application.Current).SelectedMission;
        MissionsMissionDetailTextBlock.Text = ((App)Application.Current).Missions[(int)missionIndex].Description;

        var playerTeam = ((App)Application.Current).Team;

        for (uint i = 0; i < playerTeam.Length; i++)
        {
            if (playerTeam[i] != null)
            {
                var slot = new Image
                {
                    Tag = "character",
                    Uid = i.ToString(),
                    Source = new BitmapImage(new Uri("../assets/man.png", UriKind.Relative)),
                    Height = 140,
                    Cursor = Cursors.Hand
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

    private void Image_OpenMissionsDetails(object sender, MouseButtonEventArgs e)
    {
        var map = new view.Map();
        map.ShowDialog();
        var missionIndex = ((App)Application.Current).SelectedMission;
        MissionsMissionDetailTextBlock.Text = ((App)Application.Current).Missions[(int)missionIndex].Description;
    }

    private static void PlayerSlot_OnMouseDown(object sender, MouseButtonEventArgs e)
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
                ((Game)(Application.Current.Windows[i])).MainTabControl.SelectedIndex = 1;
                ((Game)(Application.Current.Windows[i])).ContentFrame.Content = new Characters();
                break;
            }
        }
        else
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                ((App)Application.Current).RemoveTeamMember(pslot);

                // Reset intent just in case
                ((App)Application.Current).Intent = Intent.View;

                // Reload the view
                for (var i = 0; i < Application.Current.Windows.Count; i++)
                {
                    if (Application.Current.Windows[i]!.GetType() != typeof(Game)) continue;
                    ((Game)(Application.Current.Windows[i])).MainTabControl.SelectedIndex = 0;
                    ((Game)(Application.Current.Windows[i])).ContentFrame.Content = new Missions();
                    break;
                }
            }
        }
    }
}