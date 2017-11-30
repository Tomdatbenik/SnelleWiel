using Snelle_Wiel.Classes;
using Snelle_Wiel.Windows;
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

namespace Snelle_Wiel.Pages
{
    /// <summary>
    /// Interaction logic for BeheerApplicatie.xaml
    /// </summary>
    public partial class BeheerApplicatie : Page
    {
        Database db;
        public BeheerApplicatie(Database database)
        {
            InitializeComponent();
            this.db = database;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WAddUser aw = new WAddUser(this.db);
            aw.ShowDialog();
        }
    }
}
