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
    /// Interaction logic for BeheerKlanten.xaml
    /// </summary>
    public partial class BeheerKlanten : Page
    {
        Database db;
        ObservableCollection<Klant> Klanten = new ObservableCollection<Klant>();
        int currentId;
        public BeheerKlanten(Database database)
        {
            InitializeComponent();
            this.db = database;
            setup();
        }

        public void setup()
        {
            LvLocaties.ItemsSource = null;
            LvContacten.ItemsSource = null;
            Klanten.Clear();

            string query = "SELECT * FROM Klanten";
            DataTable dtdata = db.ExecuteStringQuery(query);

            foreach (DataRow dr in dtdata.Rows)
            {
                int id = int.Parse(dr["KlantId"].ToString());
                string Naam = dr["KNaam"].ToString();

                Klant k = new Klant(id, Naam);
                Klanten.Add(k);
            }
            TbNaam.Text = Klanten[0].Naam;
            LvKlanten.ItemsSource = Klanten;

            string LocatieQuery = "SELECT * FROM KlantLocaties WHERE Klantid = '" + Klanten[0].Id +"'";
            DataTable DtLData = db.ExecuteStringQuery(LocatieQuery);

            ObservableCollection<Locatie> klantlocaties = new ObservableCollection<Locatie>();
            foreach (DataRow dar in DtLData.Rows)
            {
                string Locatie = dar["Locatie"].ToString();
                string Adres = dar["Adres"].ToString();
                string Postcode = dar["Postcode"].ToString();
                string Land = dar["Land"].ToString();
                Locatie l = new Locatie(Locatie,Adres,Postcode, Land);
                klantlocaties.Add(l);
            }

            LvLocaties.ItemsSource = klantlocaties;


            string ContactQuery = "SELECT * FROM ContactInformatie WHERE Klantid = '" + Klanten[0].Id + "'";
            currentId = Klanten[0].Id;
            DataTable DtCData = db.ExecuteStringQuery(ContactQuery);

            ObservableCollection<Contact> Contactenlijst = new ObservableCollection<Contact>();
            foreach (DataRow datarow in DtCData.Rows)
            {
                string Naam = datarow["Naam"].ToString();
                string Telefoon = datarow["Telefoon"].ToString();
                string Email = datarow["Email"].ToString();

                Contact c = new Contact(Naam, Telefoon, Email);
                Contactenlijst.Add(c);
            }

            LvContacten.ItemsSource = Contactenlijst;
        }

        private void TbKlantenZoeken_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Query = "SELECT * FROM Klanten WHERE KNaam LIKE '" + TbKlantenZoeken.Text + "%'";
            DataTable DtData = db.ExecuteStringQuery(Query);
            ObservableCollection<Klant> klanten = new ObservableCollection<Klant>();

            foreach(DataRow dr in DtData.Rows)
            {
                int id = int.Parse(dr["KlantId"].ToString());
                string naam = dr["KNaam"].ToString();
                Klant k = new Klant(id, naam);
                klanten.Add(k);
            }
            LvKlanten.ItemsSource = klanten;
        }

        private void LvKlanten_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Klant selectedklant = LvKlanten.SelectedItem as Klant;

            TbNaam.Text = selectedklant.Naam;
            currentId = selectedklant.Id;

            string LocatieQuery = "SELECT * FROM KlantLocaties WHERE Klantid = '" + selectedklant.Id + "'";
            DataTable DtLData = db.ExecuteStringQuery(LocatieQuery);

            ObservableCollection<Locatie> klantlocaties = new ObservableCollection<Locatie>();
            foreach (DataRow dar in DtLData.Rows)
            {
                string Locatie = dar["Locatie"].ToString();
                string Adres = dar["Adres"].ToString();
                string Postcode = dar["Postcode"].ToString();
                string Land = dar["Land"].ToString();
                Locatie l = new Locatie(Locatie, Adres, Postcode, Land);
                klantlocaties.Add(l);
            }

            LvLocaties.ItemsSource = klantlocaties;


            string ContactQuery = "SELECT * FROM ContactInformatie WHERE Klantid = '" + selectedklant.Id + "'";
            DataTable DtCData = db.ExecuteStringQuery(ContactQuery);

            ObservableCollection<Contact> Contactenlijst = new ObservableCollection<Contact>();
            foreach (DataRow datarow in DtCData.Rows)
            {
                string Naam = datarow["Naam"].ToString();
                string Telefoon = datarow["Telefoon"].ToString();
                string Email = datarow["Email"].ToString();

                Contact c = new Contact(Naam, Telefoon, Email);
                Contactenlijst.Add(c);
            }

            LvContacten.ItemsSource = Contactenlijst;
        }

        private void BtnWijzigen_Click(object sender, RoutedEventArgs e)
        {
            Klantwijzigen kw = new Klantwijzigen(this.db,this.currentId);
            kw.ShowDialog();
            this.setup();
        }
    }
}
