using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Dungeon_Master_Manager.model;

namespace Dungeon_Master_Manager.view
{
    /// <summary>
    /// Logique d'interaction pour Characters.xaml
    /// </summary>
    public partial class Characters
    {
        private void RenderCharacters()
        {
            var availableCharacters = ((App)Application.Current).Characters;
            for (var i = 0; i < availableCharacters.Length; i++)
            {
                var currentPlayer = availableCharacters[i];

                var tooltipText = currentPlayer.ToString();
                tooltipText += ((App)Application.Current).Intent switch
                {
                    Intent.View => $"\nClic gauche pour modifier {currentPlayer.Name}",
                    Intent.Select => $"\nClic gauche pour ajouter {currentPlayer.Name} à l'équipe",
                    _ => throw new ArgumentOutOfRangeException()
                };

                var characterImage = new Image()
                {
                    Tag = i.ToString(),
                    Source = new BitmapImage(new Uri("../assets/man.png", UriKind.Relative)),
                    Margin = new Thickness(40),
                    Cursor = Cursors.Hand,
                    MaxHeight = 140,
                    ToolTip = tooltipText,
                    Stretch = Stretch.Uniform
                };
                characterImage.MouseDown += CharacterClicked;
                var x = i % CharacterGrid.ColumnDefinitions.Count; // Column
                var y = i / CharacterGrid.ColumnDefinitions.Count; // Line

                Grid.SetColumn(characterImage, x);
                Grid.SetRow(characterImage, y);
                CharacterGrid.Children.Add(characterImage);
            }
        }

        public Characters()
        {
            InitializeComponent();
            RenderCharacters();
        }

        private void CharacterClicked(object sender, MouseButtonEventArgs e)
        {
            var it = ((App)Application.Current).Intent;
            var clickedCharacter = Convert.ToUInt32(((Image)sender).Tag);
            switch (it)
            {
                case Intent.Select:
                {
                    ((App)Application.Current).AddTeamMember(clickedCharacter);
                    for (var i = 0; i < Application.Current.Windows.Count; i++)
                    {
                        if (Application.Current.Windows[i]!.GetType() != typeof(Game)) continue;
                        var gameWindow = (Game)(Application.Current.Windows[i])!;
                        gameWindow.MainTabControl.SelectedIndex =
                            0; // Here we want to trigger the change event to reset out intent
                        break;
                    }

                    break;
                }
                case Intent.View:
                {
                    if (e.LeftButton == MouseButtonState.Pressed)
                    {
                        ((App)Application.Current).Intent = Intent.View;
                        var characterEditorWindow = new CharacterWindow();
                        characterEditorWindow.ShowDialog();
                    }

                    break;
                }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}