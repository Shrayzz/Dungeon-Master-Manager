using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Dungeon_Master_Manager.model;

namespace Dungeon_Master_Manager.view
{
    /// <summary>
    /// Logique d'interaction pour CharacterWindow.xaml
    /// </summary>
    public partial class CharacterWindow
    {
        public CharacterWindow()
        {
            InitializeComponent();
            var app = ((App)Application.Current);
            var selectedCharacter = app.Characters[app.SelectedThing];
            CNameBox.Text = selectedCharacter.Name;

            var snowflakeImage = new BitmapImage(new Uri("../assets/snowflake.png", UriKind.Relative));
            var lightningImage = new BitmapImage(new Uri("../assets/lightning.png", UriKind.Relative));
            var earthImage = new BitmapImage(new Uri("../assets/earth.png", UriKind.Relative));

            var rangeImage = new BitmapImage(new Uri("../assets/range.png", UriKind.Relative));
            var meleeImage = new BitmapImage(new Uri("../assets/sword.png", UriKind.Relative));
            var magicImage = new BitmapImage(new Uri("../assets/crystal-ball.png", UriKind.Relative));

            var potionImage = new BitmapImage(new Uri("../assets/health-potion.png", UriKind.Relative));

            BitmapImage[] elmentImages = [lightningImage, snowflakeImage, earthImage];
            BitmapImage[] weapongClassImages = [rangeImage, meleeImage, magicImage];

            for (var i = 0; i < elmentImages.Length; i++)
            {
                var image = new Image()
                {
                    Height = 50
                };
                if (i != (int)selectedCharacter.Element)
                {
                    var grayImage = new FormatConvertedBitmap(elmentImages[i], PixelFormats.Gray8, null, 50);
                    image.Source = grayImage;
                }
                else
                {
                    image.Source = elmentImages[i];
                }

                Grid.SetColumn(image, i);
                CInfoGrid.Children.Add(image);
            }

            for (var i = 0; i < weapongClassImages.Length; i++)
            {
                var image = new Image()
                {
                    Height = 50
                };
                if (i != (int)selectedCharacter.AcceptedWeapons)
                {
                    var grayImage = new FormatConvertedBitmap(weapongClassImages[i], PixelFormats.Gray8, null, 50);
                    image.Source = grayImage;
                }
                else
                {
                    image.Source = weapongClassImages[i];
                }

                Grid.SetRow(image, 1);
                Grid.SetColumn(image, i);
                CInfoGrid.Children.Add(image);
            }

            for (var i = 0; i < selectedCharacter.Inventory.Count; i++)
            {
                var item = selectedCharacter.Inventory[i];

                if (item == null)
                {
                    var plusImage = new Image()
                    {
                        Source = new BitmapImage(new Uri("../assets/plus.png", UriKind.Relative)),
                        ToolTip = "Click gauche pour ajouter un object",
                        Tag = "plus",
                        Cursor = Cursors.Hand,
                        Uid = i.ToString(),
                        Height = 50,
                    };
                    plusImage.MouseDown += OnCharacterInvSlotClicked;
                    Grid.SetColumn(plusImage, i);
                    CInvGrid.Children.Add(plusImage);
                    continue;
                }

                var imageSource = item.Type switch
                {
                    ItemType.Consumable => potionImage,
                    ItemType.Weapon => item.Range switch
                    {
                        WeaponClass.Magic => magicImage,
                        WeaponClass.Melee => meleeImage,
                        WeaponClass.Range => rangeImage
                    }
                };

                var image = new Image()
                {
                    ToolTip =
                        item + $"\n Click gauche pour enlever l'objet de l'inventaire de {selectedCharacter.Name}",
                    Source = imageSource,
                    Cursor = Cursors.Hand,
                    Tag = "item",
                    Uid = i.ToString(),
                    Height = 50,
                };

                image.MouseDown += OnCharacterInvSlotClicked;
                Grid.SetColumn(image, i);
                CInvGrid.Children.Add(image);
            }
        }

        private void CNameBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var app = ((App)Application.Current);
            var selectedCharacter = app.Characters[app.SelectedThing];
            selectedCharacter.Name = CNameBox.Text;
        }

        private void OnCharacterInvSlotClicked(object sender, MouseEventArgs e)
        {
            var isSlotEmpty = (string)((Image)sender).Tag == "plus";
            var islot = Convert.ToInt32(((Image)sender).Uid);

            ((App)Application.Current).Intent = Intent.Select;
            for (var i = 0; i < Application.Current.Windows.Count; i++)
            {
                if (Application.Current.Windows[i]!.GetType() != typeof(Game)) continue;
                var gameWindow = (Game)(Application.Current.Windows[i])!;

                // Hack to not trigger the event, so it does not reset out intent
                gameWindow.MainTabControl.SelectionChanged -= gameWindow.TabControl_SelectionChanged;

                gameWindow.MainTabControl.SelectedIndex = 2;

                gameWindow.MainTabControl.SelectionChanged += gameWindow.TabControl_SelectionChanged;

                gameWindow.ContentFrame.Content = new Items();
                Close();
                break;
            }
        }
    }
}