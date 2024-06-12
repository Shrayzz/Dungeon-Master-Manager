using Dungeon_Master_Manager.view;
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
    public partial class Game : Window
    {
        public Game()
        {
            InitializeComponent();

            // Default content
            ContentFrame.Content = new Missions();

            // Event handlers for tab changes
            MainTabControl.SelectionChanged += TabControl_SelectionChanged;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl tabControl)
            {
                if (tabControl.SelectedItem is TabItem selectedTab && selectedTab.Header != null)
                {
                    switch (selectedTab.Header.ToString())
                    {
                        case "Missions":
                            ContentFrame.Content = new Missions();
                            break;
                        case "Characters":
                            ContentFrame.Content = new Characters();
                            break;
                        case "Items":
                            ContentFrame.Content = new Items();
                            break;
                        case "Credits":
                            ContentFrame.Content = new Credits();
                            break;
                    }
                }
            }
        }
    }
}
