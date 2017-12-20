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
        UserData ud;
        User SelectedChauffeur;
        public BeheerChauffeur(Database database)
        {
            InitializeComponent();
            this.db = database;
        }

        public void Setup()
        {
            List<User> ulist = new List<User>();
            ud = new UserData(this.db);

            LvChauffeurs.ItemsSource = null;
            LvRijbewijzen.ItemsSource = null;
            Users.Clear();

            DataTable dt = db.ExecuteStringQuery("SELECT UserId, RoleId, UNaam, UWoonplaats, UAdres, UPostcode, UEmail, UTelefoon FROM Users WHERE RoleId = 2");
            foreach(DataRow dr in dt.Rows)
            {
                string id = dr["UserId"].ToString();
                string naam = dr["UNaam"].ToString();
                string woonplaats = dr["UWoonplaats"].ToString();
                string adres = dr["UAdres"].ToString();
                string postcode = dr["UPostcode"].ToString();
                string email = dr["UEmail"].ToString();
                string telefoon = dr["UTelefoon"].ToString();

                User u = new User(int.Parse(id),naam,woonplaats,adres,postcode,email,telefoon);
                Users.Add(u);
            }
            LvChauffeurs.ItemsSource = Users;
            Fillinfoboxes(Users[0]);
            SelectedChauffeur = Users[0];
            LvRijbewijzen.ItemsSource = ud.GetRijbewijzenOnId(Users[0].Id);
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

        private void Fillinfoboxes(User chauf)
        {
            TbChaufNaam.Text = chauf.Naam;
            TbChaufWoonplaats.Text = chauf.Woonplaats;
            TbChaufAdres.Text = chauf.Adres;
            TbChaufPostcode.Text = chauf.Postcode;
            TbChaufEmail.Text = chauf.Email;
            TbChaufTelefoon.Text = chauf.Telefoonnr;
        }

        private void LvChauffeurs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectedChauffeur = LvChauffeurs.SelectedItem as User;
            LvRijbewijzen.ItemsSource = ud.GetRijbewijzenOnId(SelectedChauffeur.Id);
            Fillinfoboxes(SelectedChauffeur);
        }

        private void btnrijbewijswijzigen_Click(object sender, RoutedEventArgs e)
        {
            WRijbewijswijzigen wrw = new WRijbewijswijzigen(this.db,SelectedChauffeur.Id);
            wrw.ShowDialog();
            Setup();
        }
    }
}
