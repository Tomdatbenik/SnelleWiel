using Snelle_Wiel.Classes;
using Snelle_Wiel.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for WEditVrachtwagen.xaml
    /// </summary>
    public partial class WEditVrachtwagen : Window
    {
        Database db;
        cLogin lg;
        int id;
        Wagen wagen;

        public WEditVrachtwagen(Database database, int Id,Wagen w)
        {
            InitializeComponent();
            wagen = w;
            Setup();

            this.db = database;
            this.lg = new cLogin(database);
            this.id = Id;
            string query = "SELECT * FROM Wagens WHERE Kenteken = '"+ id.ToString() +"'";
            DataTable dt = db.ExecuteStringQuery(query);
            foreach (DataRow dr in dt.Rows)
            {
                if(this.id == db.Userid)
                {
                    tbKenteken.Text = dr["Kenteken"].ToString();
                    tbChauffeur.Text = dr["Chauffeur"].ToString();
                    tbMerk.Text = dr["Merk"].ToString();
                    tbType.Text = dr["Type"].ToString();
                    tbBouwjaar.Text = dr["Bouwjaar"].ToString();
                    tbBrandstof.Text = dr["Brandstof"].ToString();
                    tbVermogen.Text = dr["Vermogen"].ToString();
                    tbAPK.Text = dr["APK"].ToString();
                    tbVoertuighoogte.Text = dr["Voertuig hoogte"].ToString();
                    tbMassarijklaar.Text = dr["MassaRijKlaar"].ToString();
                    tbMaxmassa.Text = dr["ToegestaneMaxMassaVoertuig"].ToString();
                    tbMaxlaadvermogen.Text = dr["MaximaleLaadVermogen"].ToString();
                    tbLaadruimteinhoud.Text = dr["LaadRuimteInhoud"].ToString();
                    tbStatus.Text = dr["Status"].ToString();

                }
                else
                {
                    tbKenteken.Text = dr["Kenteken"].ToString();
                    tbChauffeur.Text = dr["Chauffeur"].ToString();
                    tbMerk.Text = dr["Merk"].ToString();
                    tbType.Text = dr["Type"].ToString();
                    tbBouwjaar.Text = dr["Bouwjaar"].ToString();
                    tbBrandstof.Text = dr["Brandstof"].ToString();
                    tbVermogen.Text = dr["Vermogen"].ToString();
                    tbAPK.Text = dr["APK"].ToString();
                    tbVoertuighoogte.Text = dr["Voertuig hoogte"].ToString();
                    tbMassarijklaar.Text = dr["MassaRijKlaar"].ToString();
                    tbMaxmassa.Text = dr["ToegestaneMaxMassaVoertuig"].ToString();
                    tbMaxlaadvermogen.Text = dr["MaximaleLaadVermogen"].ToString();
                    tbLaadruimteinhoud.Text = dr["LaadRuimteInhoud"].ToString();
                    tbStatus.Text = dr["Status"].ToString();
                }
            }
        }


        public void Setup()
        {
            int wagenid = wagen.WagenId;
            tbChauffeur.Text = wagen.Chauffeur;
            tbKenteken.Text = wagen.Kenteken;
            tbMerk.Text = wagen.Merk;
            tbType.Text = wagen.Type;
            tbBouwjaar.Text = wagen.Bouwjaar;
            tbBrandstof.Text = wagen.Brandstof;
            tbVermogen.Text = wagen.Vermogen;
            tbAPK.Text = wagen.APK;
            tbVoertuighoogte.Text = wagen.VoertuigHoogte;
            tbMassarijklaar.Text = wagen.MassaRijKlaar;
            tbMaxmassa.Text = wagen.ToegestaandeMaximaleMassa;
            tbMaxlaadvermogen.Text = wagen.ToegestaandeLaadVermogen;
            tbLaadruimteinhoud.Text = wagen.Laadruimte;
            tbStatus.Text = wagen.Status;

        }

        private void BtnEditUser_Click(object sender, RoutedEventArgs e)
        {
            if(tbKenteken.Text != "" && tbChauffeur.Text != "" && tbMerk.Text != "" && tbType.Text != "" && tbBouwjaar.Text != "" && tbBrandstof.Text != "" && tbVermogen.Text != "" && tbAPK.Text != "" && tbVoertuighoogte.Text != "" && tbMassarijklaar.Text != "" && tbMaxmassa.Text != "" && tbMaxlaadvermogen.Text != "" && tbLaadruimteinhoud.Text != "" && tbStatus.Text != "")
            {
                EditUser(tbKenteken.Text, tbChauffeur.Text, tbMerk.Text, tbType.Text, tbBouwjaar.Text, tbBrandstof.Text, tbVermogen.Text, tbAPK.Text, tbVoertuighoogte.Text, tbMassarijklaar.Text, tbMaxmassa.Text, tbMaxlaadvermogen.Text, tbLaadruimteinhoud.Text, tbStatus.Text, wagen.WagenId);

                this.Close();
            }
            else
            {
                TbError.Text = "Vul de gevraagde gegevens in";
            }
        }

        public void EditUser(string kenteken, string chauffeur, string merk, string type, string bouwjaar, string brandstof, string vermogen, string apk, string voertuighoogte, string massarijklaar, string maxmassa, string maxlaadvermogen, string laadruimteinhoud, string status, int wagenid)
        {
                string query = "UPDATE `snellewiel`.`Wagens` SET `Chauffeur` = '" + chauffeur + "'" +
                ", `Merk`= '" + merk + " '" +
                ", `Type`= '" + type + "', " +
                "`Bouwjaar`= '" + bouwjaar + "', " +
                "`Brandstof`= '" + brandstof + " ', " +
                "`Vermogen`= '" + vermogen + "'," +
                "`APK`= '" + apk + "'," +
                "`Voertuig hoogte`= '" + voertuighoogte + "'," +
                "`MassaRijKlaar`= '" + massarijklaar + "'," +
                "`ToegestaneMaxMassaVoertuig`= '" + maxmassa + "'," +
                "`MaximaleLaadVermogen`= '" + maxlaadvermogen + "'," +
                "`LaadRuimteInhoud`= '" + laadruimteinhoud + "'," +
                "`Kenteken`= '" + kenteken + "'," +
                "`Status`= '" + status + " ' " +
                "WHERE  `WagenId`='" + wagenid + "';";
                db.ExecuteStringQuery(query);
            
        }
    }
}
