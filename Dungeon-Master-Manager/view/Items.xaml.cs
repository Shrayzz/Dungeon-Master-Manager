using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Dungeon_Master_Manager.model;

namespace Dungeon_Master_Manager.view
{
    /// <summary>
    /// Logique d'interaction pour Items.xaml
    /// </summary>
    public partial class Items : Page
    {
        public Items()
        {
            InitializeComponent();
            RenderItems();
        }

        private void RenderItems()
        {
            var availableItems = ((App)Application.Current).Inventory;
            for (var i = 0; i < availableItems.Length; i++)
            {
                var currentItem = availableItems[i];

                var tooltipText = currentItem.ToString();
                tooltipText += ((App)Application.Current).Intent switch
                {
                    Intent.View => "",
                    Intent.Select => $"\nClic gauche pour ajouter {currentItem.Name} à l'inventaire du personnage",
                    _ => throw new ArgumentOutOfRangeException()
                };

                var imgPath = currentItem.Type switch
                {
                    ItemType.Consumable => "../assets/health-potion.png",
                    ItemType.Weapon => currentItem.Range switch
                    {
                        WeaponClass.Magic => "../assets/crystal-ball.png",
                        WeaponClass.Melee => "../assets/sword.png",
                        WeaponClass.Range => "../assets/range.png"
                    },
                    _ => throw new ArgumentOutOfRangeException() // Unreachable
                };

                var itemImage = new Image()
                {
                    Tag = i.ToString(),
                    Source = new BitmapImage(new Uri(imgPath, UriKind.Relative)),
                    Margin = new Thickness(40),
                    Cursor = Cursors.Hand,
                    MaxHeight = 140,
                    ToolTip = tooltipText,
                    Stretch = Stretch.Uniform
                };
                itemImage.MouseDown += ItemClicked;
                var x = i % ItemGrid.ColumnDefinitions.Count; // Column
                var y = i / ItemGrid.ColumnDefinitions.Count; // Line

                Grid.SetColumn(itemImage, x);
                Grid.SetRow(itemImage, y);
                ItemGrid.Children.Add(itemImage);
            }
        }

        private void ItemClicked(object sender, MouseButtonEventArgs e)
        {
            var it = ((App)Application.Current).Intent;
            var clickedItem = Convert.ToInt32(((Image)sender).Tag);
            var app = ((App)Application.Current);
            switch (it)
            {
                case Intent.Select:
                {
                    // Add the item to the selected player
                    app.Characters[app.SelectedThing].Equip(clickedItem);

                    for (var i = 0; i < Application.Current.Windows.Count; i++)
                    {
                        if (Application.Current.Windows[i]!.GetType() != typeof(Game)) continue;
                        var gameWindow = (Game)(Application.Current.Windows[i])!;
                        // Here we want to trigger the change event to reset out intent

                        new CharacterWindow().ShowDialog();
                        gameWindow.MainTabControl.SelectedIndex = 0;
                        break;
                    }

                    break;
                }
                case Intent.View:
                {
                    // if (e.LeftButton == MouseButtonState.Pressed)
                    // {
                    //     ((App)Application.Current).Intent = Intent.View;
                    //     ((App)Application.Current).SelectedThing = (int)clickedItem;
                    //     new ItemWindow().ShowDialog();
                    //     return;
                    //
                    // }
                    // TODO: Find a way to click a item then a character to heal them
                    // else if (e.RightButton == MouseButtonState.Pressed && app.Inventory[clickedItem].Type == ItemType.Consumable)
                    // {
                    //     app.Intent = Intent.Select;
                    //     for (var i = 0; i < Application.Current.Windows.Count; i++)
                    //     {
                    //         if (Application.Current.Windows[i]!.GetType() != typeof(Game)) continue;
                    //         var gameWindow = (Game)(Application.Current.Windows[i])!;
                    //         gameWindow.MainTabControl.SelectionChanged -= gameWindow.TabControl_SelectionChanged;
                    //         gameWindow.MainTabControl.SelectedIndex = 1;
                    //         gameWindow.MainTabControl.SelectionChanged += gameWindow.TabControl_SelectionChanged;
                    //         gameWindow.ContentFrame.Content = new Characters();
                    //     }
                    // }

                    break;
                }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}