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
    /// Interaction logic for BeheerChauffeur.xaml
    /// </summary>
    public partial class BeheerChauffeur : Page
    {
        Database db;
        ObservableCollection<User> Users = new ObservableCollection<User>();
        public BeheerChauffeur(Database database)
        {
            InitializeComponent();
            this.db = database;
        }

        public void Setup()
        {
            List<User> ulist = new List<User>();

            LvChauffeurs.ItemsSource = null;
            Users.Clear();

            DataTable dt = db.ExecuteStringQuery("SELECT UserId, RoleId, UNaam, UWoonplaats, UAdres, UPostcode, UEmail, UTelefoon FROM Users WHERE RoleId = 2");
            foreach(DataRow dr in dt.Rows)
            {
                User u = new User(int.Parse(dr["UserId"].ToString()), dr["UNaam"].ToString(),null,null,null,null,null);
                Users.Add(u);
            }
            LvChauffeurs.ItemsSource = Users;
        }

        private void btnaddchauffeur_Click(object sender, RoutedEventArgs e)
        {
            WAddUser aw = new WAddUser(this.db);
            aw.ShowDialog();
            Setup();
        }

        private void btnChauffeurWijzigen_Click(object sender, RoutedEventArgs e)
        {
            if (LvChauffeurs.SelectedItem != null)
            {
                User u = LvChauffeurs.SelectedItem as User;
                WEditUser ew = new WEditUser(this.db, u.Id);
                ew.ShowDialog();
                Setup();
            }
            else
            {
                MessageBox.Show("Selecteer een gebruiker");
            }
        }

        private void btnChauffeurVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            int UserId = Users[LvChauffeurs.SelectedIndex].Id;
            string query = "DELETE FROM `snellewiel`.`Users` WHERE  `UserId`= " + UserId + ";";
            db.ExecuteStringQuery(query);
            Setup();
        }
    }
}
