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
    /// Logique d'interaction pour Characters.xaml
    /// </summary>
    public partial class Characters : Page
    {
        public Characters()
        {
            InitializeComponent();
        }
        private void OpenCharacter(object sender, MouseButtonEventArgs e)
        {
            view.CharacterWindow CharWin = new view.CharacterWindow();
            CharWin.ShowDialog();
        }
    }
}
