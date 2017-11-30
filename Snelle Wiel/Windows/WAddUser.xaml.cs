using Snelle_Wiel.Classes;
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
using System.Windows.Shapes;

namespace Snelle_Wiel.Windows
{
    /// <summary>
    /// Interaction logic for WAddUser.xaml
    /// </summary>
    public partial class WAddUser : Window
    {
        Database db;
        public WAddUser(Database database)
        {
            InitializeComponent();
            this.db = database;
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
