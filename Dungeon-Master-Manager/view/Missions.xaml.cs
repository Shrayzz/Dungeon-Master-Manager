using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Logique d'interaction pour Missions.xaml
    /// </summary>
    public partial class Missions : Page
    {
        public Missions()
        {
            InitializeComponent();
        }

        private void Image_OpenMissionsDetails(object sender, MouseButtonEventArgs e)
        {
            view.Map map = new view.Map();
            map.ShowDialog();
        }

        private void PlayerSlot_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            uint pslot = Convert.ToUInt32(((Image)sender).Uid);
            bool slotEmpty = (string)((Image)sender).Tag == "plus";
            
        }
        
    }
}
