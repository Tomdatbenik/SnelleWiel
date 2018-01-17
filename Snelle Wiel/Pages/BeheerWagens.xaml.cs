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
        Wagen SelectedWagen;
        public BeheerWagens(Database database)
        {
            InitializeComponent();
            this.db = database;
            Setup();
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
            LvWagens.ItemsSource = new ObservableCollection<Wagen>();
            LvWagens.ItemsSource = null;
            Vrachtwagens.Clear();

            DataTable dt = db.ExecuteStringQuery("SELECT * FROM Wagens");
            foreach (DataRow dr in dt.Rows)
            {
                string sWagen = dr["WagenId"].ToString();
                int wagennummer = Int32.Parse(sWagen);
                string chauffeur = dr["Chauffeur"].ToString();
                string kenteken = dr["Kenteken"].ToString();
                string merk = dr["Merk"].ToString();
                string type = dr["Type"].ToString();
                string bouwjaar = dr["Bouwjaar"].ToString();
                string brandstof = dr["Brandstof"].ToString();
                string vermogen = dr["Vermogen"].ToString();
                string apk = dr["APK"].ToString();
                string voertuighoogte = dr["Voertuig hoogte"].ToString();
                string massarijklaar = dr["MassaRijKlaar"].ToString();
                string toegestanemaxmassavoertuig = dr["ToegestaneMaxMassaVoertuig"].ToString();
                string maximalelaadvermogen = dr["MaximaleLaadVermogen"].ToString();
                string laadruiminhoud = dr["LaadRuimteInhoud"].ToString();
                string status = dr["Status"].ToString();

                Wagen u = new Wagen();
                u.WagenId = wagennummer;
                u.Chauffeur = chauffeur;
                u.Kenteken = kenteken;
                u.Merk = merk;
                u.Type = type;
                u.Bouwjaar = bouwjaar;
                u.Brandstof = brandstof;
                u.Vermogen = vermogen;
                u.APK = apk;
                u.VoertuigHoogte = voertuighoogte;
                u.MassaRijKlaar = massarijklaar;
                u.ToegestaandeMaximaleMassa = toegestanemaxmassavoertuig;
                u.ToegestaandeLaadVermogen = maximalelaadvermogen;
                u.Laadruimte = laadruiminhoud;
                u.Status = status;
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
            System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Weet u het zeker", "Stop!", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                string Kenteken = Vrachtwagens[LvWagens.SelectedIndex].Kenteken;
                string query = "DELETE FROM `snellewiel`.`Wagens` WHERE  `Kenteken`= '" + Kenteken + "';";
                db.ExecuteStringQuery(query);
                Setup();
            }
        }

        private void LvWagens_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectedWagen = LvWagens.SelectedItem as Wagen;
            TbWagenNummer.Text = SelectedWagen.WagenId.ToString();
            TbChauffeur.Text = SelectedWagen.Chauffeur;
            TbKenteken.Text = SelectedWagen.Kenteken;
            TbMerk.Text = SelectedWagen.Merk;
            TbType.Text = SelectedWagen.Type;
            TbBouwjaar.Text = SelectedWagen.Bouwjaar;
            TbBrandstof.Text = SelectedWagen.Brandstof;
            TbVermogen.Text = SelectedWagen.Vermogen;
            TbAPK.Text = SelectedWagen.APK;
            TbVoertuighoogte.Text = SelectedWagen.VoertuigHoogte;
            TbMassaRijklaar.Text = SelectedWagen.MassaRijKlaar;
            TbToegestaanmaxMassa.Text = SelectedWagen.ToegestaandeMaximaleMassa;
            TbToegestaanLaadvermogen.Text = SelectedWagen.ToegestaandeLaadVermogen;
            TbLaadruimte.Text = SelectedWagen.Laadruimte;
            TbSatus.Text = SelectedWagen.Status;
        }

        private void Btninformatie_Click(object sender, RoutedEventArgs e)
        {
            if (LvWagens.SelectedItem != null)
            {
                Wagen w = LvWagens.SelectedItem as Wagen;
                WEditVrachtwagen ew = new WEditVrachtwagen(this.db, w.WagenId,w);
                ew.ShowDialog();
                Setup();
            }
            else
            {
                MessageBox.Show("Selecteer een chauffeur");
            }
        }
    }
}
