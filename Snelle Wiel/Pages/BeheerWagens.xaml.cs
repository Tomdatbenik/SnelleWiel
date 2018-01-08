using Snelle_Wiel.Classes;
using System;
using System.Collections.Generic;
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
                TbChauffeur.Text = dr["ChauffeurId"].ToString();
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

        private void BtnToevoegen_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
