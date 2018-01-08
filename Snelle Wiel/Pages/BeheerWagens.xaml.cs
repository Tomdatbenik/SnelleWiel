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
    /// Interaction logic for BeheerWagens.xaml
    /// </summary>
    public partial class BeheerWagens : Page
    {
        Database db;
        ObservableCollection<Wagen> Vrachtwagens = new ObservableCollection<Wagen>();
        public BeheerWagens(Database database)
        {
            InitializeComponent();
            this.db = database;
        }

        private void TbWagenZoeken_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetWagen();
                TbWagenZoeken.Clear();
            }
        }

        
        public void GetWagen()
        {
            DataTable dt = db.ExecuteStringQuery("SELECT * FROM `snellewiel`.`Wagens` WHERE `Kenteken` LIKE '%" + TbWagenZoeken.Text + "%'");
            foreach (DataRow dr in dt.Rows)
            {
                TbWagenNummer.Text = dr["WagenId"].ToString();
                TbChauffeur.Text = dr["Chauffeur"].ToString();
                TbKenteken.Text = dr["Kenteken"].ToString();
                TbMerk.Text = dr["Merk"].ToString();
                TbType.Text = dr["Type"].ToString();
                TbBouwjaar.Text = dr["Bouwjaar"].ToString();
                TbBrandstof.Text = dr["Brandstof"].ToString();
                TbVermogen.Text = dr["Vermogen"].ToString();
                TbAPK.Text = dr["APK"].ToString();
                TbVoertuighoogte.Text = dr["Voertuig hoogte"].ToString();
                TbMassaRijklaar.Text = dr["MassaRijKlaar"].ToString();
                TbToegestaanmaxMassa.Text = dr["ToegestaneMaxMassaVoertuig"].ToString();
                TbToegestaanLaadvermogen.Text = dr["MaximaleLaadVermogen"].ToString();
                TbLaadruimte.Text = dr["LaadRuimteInhoud"].ToString();
                TbSatus.Text = dr["Status"].ToString();
            }
        }

        public void Setup()
        {
            List<Wagen> wagenlist = new List<Wagen>();

            LvWagens.ItemsSource = null;
            Vrachtwagens.Clear();

            DataTable dt = db.ExecuteStringQuery("SELECT * FROM `snellewiel`.`Wagens` WHERE `Kenteken` LIKE '%" + TbWagenZoeken.Text + "%'");
            foreach (DataRow dr in dt.Rows)
            {
                string wagennummer = dr["WagenId"].ToString();
                string chauffeur = dr["Chauffeur"].ToString();
                string kenteken = dr["Kenteken"].ToString();
                string merk = dr["Merk"].ToString();
                string type = dr["Type"].ToString();
                string bouwjaar = dr["Bouwjaar"].ToString();
                string brandstof = dr["Brandstof"].ToString();
                string vermorgen = dr["Vermogen"].ToString();
                string apk = dr["APK"].ToString();
                string voertuighoogte = dr["Voertuig hoogte"].ToString();
                string massarijklaar = dr["MassaRijKlaar"].ToString();
                string toegestanemaxmassavoertuig = dr["ToegestaneMaxMassaVoertuig"].ToString();
                string maximalelaadvermogen = dr["MaximaleLaadVermogen"].ToString();
                string laadruiminhoud = dr["LaadRuimteInhoud"].ToString();
                string status = dr["Status"].ToString();

                Wagen u = new Wagen();
                Vrachtwagens.Add(u);
            }
            LvWagens.ItemsSource = Vrachtwagens;
        }

        private void BtnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            WAddVrachtwagen aw = new WAddVrachtwagen(this.db);
            aw.ShowDialog();
            Setup();
        }

        private void BtnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            ////int UserId = Users[LvChauffeurs.SelectedIndex].Id;
            //string query = "DELETE FROM `snellewiel`.`Users` WHERE  `UserId`= " + UserId + ";";
            //string qureyresetplanning = "UPDATE `Order` SET `Gebruik`='0';";
            //db.ExecuteStringQuery(qureyresetplanning);
            //db.ExecuteStringQuery(query);
            //Setup();
        }
    }
}
