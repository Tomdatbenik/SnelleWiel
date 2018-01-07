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
 
            string query = "SELECT `UserId`,`UNaam`,`UWoonplaats`,`UAdres`,`UPostcode`,`UEmail`,`UTelefoon`,`RoleId` FROM Users";

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
                string Rol = "";
                switch(int.Parse(dr["RoleId"].ToString()))
                {
                    case 1:
                        Rol = "Planner";
                        break;
                    case 2:
                        Rol = "Chauffeur";
                        break;
                    case 3:
                        Rol = "Administratie";
                        break;
                }

                User u = new User(id,naam,woonplaats,adres,postcode,email,telefoonnr);
                u.Roltext = Rol;
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
            if (lvUserlist.SelectedItem != null)
            {
                if(Users[lvUserlist.SelectedIndex].Id == db.Userid)
                {
                    MessageBox.Show("U kunt niet uwzelf verwijderen");
                }
                else
                {
                    int UserId = Users[lvUserlist.SelectedIndex].Id;
                    string query = "DELETE FROM `snellewiel`.`Users` WHERE  `UserId`= " + UserId + ";";
                    db.ExecuteStringQuery(query);
                    loadusers();
                }
            }
            else
            {
                MessageBox.Show("Selecteer een gebruiker");
            }
        }

        public class ItemFromList
        {
            public int Id { get; set; }
            public string Naam { get; set; }
            public string Woonplaats { get; set; }
            public string Adres { get; set; }
            public string Postcode { get; set; }
            public string Email { get; set; }
            public string Telefoonnr { get; set; }
        }


        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if(lvUserlist.SelectedItem != null)
            {
                User u = lvUserlist.SelectedItem as User;
                WEditUser ew = new WEditUser(this.db, u.Id);
                ew.ShowDialog();
                loadusers();
            }
            else
            {
                MessageBox.Show("Selecteer een gebruiker");
            }
        }
    }
}
