using Snelle_Wiel.Classes;
using Snelle_Wiel.Objects;
using Snelle_Wiel.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
        ObservableCollection<User> Users = new ObservableCollection<User>();
        public BeheerApplicatie(Database database)
        {
            InitializeComponent();
            this.db = database;
            loadusers();
        }

        public void loadusers()
        {
            lvUserlist.ItemsSource = null;
            Users.Clear();

            DataTable dtResult = new DataTable();
 
            string query = "SELECT `UserId`,`UNaam`,`UWoonplaats`,`UAdres`,`UPostcode`,`UEmail`,`UTelefoon` FROM Users";

            dtResult =  db.ExecuteStringQuery(query);
            foreach (DataRow dr in dtResult.Rows)
            {
                int id = int.Parse(dr["UserId"].ToString());
                string naam = dr["UNaam"].ToString();
                string woonplaats = dr["UWoonplaats"].ToString();
                string adres = dr["UAdres"].ToString();
                string postcode = dr["UPostcode"].ToString();
                string email = dr["UEmail"].ToString();
                string telefoonnr = dr["UTelefoon"].ToString();

                User u = new User(id,naam,woonplaats,adres,postcode,email,telefoonnr);
                Users.Add(u);
            }

            lvUserlist.ItemsSource = Users;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WAddUser aw = new WAddUser(this.db);
            aw.ShowDialog();
            loadusers();
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            int UserId = Users[lvUserlist.SelectedIndex].Id;
            string query = "DELETE FROM `snellewiel`.`Users` WHERE  `UserId`= "+UserId+";";
            db.ExecuteStringQuery(query);
            loadusers();
        }
    }
}
