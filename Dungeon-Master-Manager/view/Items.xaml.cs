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
    /// Logique d'interaction pour Items.xaml
    /// </summary>
    public partial class Items : Page
    {
        public Items()
        {
            InitializeComponent();
        }
        private void OpenItem(object sender, MouseButtonEventArgs e)
        {
            view.ItemWindow itemWin = new view.ItemWindow();
            itemWin.ShowDialog();
        }
    }
}
