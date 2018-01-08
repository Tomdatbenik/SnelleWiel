using Snelle_Wiel.Classes;
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
    /// Interaction logic for WAddVrachtwagen.xaml
    /// </summary>
    public partial class WAddVrachtwagen : Window
    {
        Database db;
        public WAddVrachtwagen(Database database)
        {
            InitializeComponent();
            this.db = database;
        }

        public string AddVrachtwagen(string kenteken, string chauffeur, string merk, string type, string bouwjaar, string brandstof, string vermogen, string apk, string voertuighoogte, string massarijklaar, string maxmassa, string maxlaadvermogen, string laadruimteinhoud, string status)
        {
            string q = "SELECT * FROM `Wagens` WHERE `Kenteken` = '" + kenteken + "'";
            DataTable dt = db.ExecuteStringQuery(q);
            if (dt.Rows.Count == 0)
            {
                string query = "INSERT INTO `Wagens`(`Kenteken`, `Chauffeur`, `Merk`, `Type`, `Bouwjaar`, `Brandstof`, `Vermogen`, `APK`, `Voertuig hoogte`, `MassaRijKlaar`, `ToegestaneMaxMassaVoertuig`, `MaximaleLaadVermogen`, `LaadRuimteInhoud`, `Status`) " +
                    "VALUES(" +
                    "'" + kenteken + "'," +
                    "'" + chauffeur + "'," +
                    "'" + merk + "'," +
                    "'" + type + "'," +
                    "'" + bouwjaar + "'," +
                    "'" + brandstof + "'," +
                    "'" + vermogen + "'," +
                    "'" + apk + "'," +
                    "'" + voertuighoogte + "'," +
                    "'" + massarijklaar + "'," +
                    "'" + maxmassa + "'," +
                    "'" + maxlaadvermogen + "'," +
                    "'" + laadruimteinhoud + "'," +
                    "'" + status + "'" +
                    ");";
                db.ExecuteStringQuery(query);

                return "OK";
            }
            else
            {
                return "ERROR";
            }
        }

        private void BtnAddVrachtwagen_Click(object sender, RoutedEventArgs e)
        {
            if (tbKenteken.Text != "" && tbChauffeur.Text != "" && tbMerk.Text != "" && tbType.Text != "" && tbBouwjaar.Text != "" && tbBrandstof.Text != "" && tbVermogen.Text != "" && tbAPK.Text != "" && tbVoertuighoogte.Text != "" && tbMassarijklaar.Text != "" && tbMaxmassa.Text != "" && tbMaxlaadvermogen.Text != "" && tbLaadruimteinhoud.Text != "" && tbStatus.Text != "")
            {
                if(AddVrachtwagen(tbKenteken.Text, tbChauffeur.Text, tbMerk.Text, tbType.Text, tbBouwjaar.Text, tbBrandstof.Text, tbVermogen.Text, tbAPK.Text, tbVoertuighoogte.Text, tbMassarijklaar.Text, tbMaxmassa.Text, tbMaxlaadvermogen.Text, tbLaadruimteinhoud.Text, tbStatus.Text) == "OK")
                {
                    this.Close();
                }
                else
                {
                    TbError.Text = "Wagen met dit kenteken bestaat al";
                }
            }
            else
            {
                TbError.Text = "Vul de gevraagde gegevens in";
            }
        }

        //returns true if its allowed
        //private static bool IsTextAllowed(string text)
        //{
        //    char[] characters = text.ToCharArray();
        //    Regex regex = new Regex("[^0-9]+"); //regex that matches disallowed text
        //    bool istext = false;
        //    foreach (char c in characters)
        //    {
        //        if (regex.IsMatch(c.ToString()))
        //        {
        //            istext = true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    return istext;
        //}

        //private void Textbox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    TextBox tb = sender as TextBox;
        //    if(!IsTextAllowed(tb.Text))
        //    {
        //        string text = tb.Text;
        //        if(text != "")
        //        {
        //            char[] characters = text.ToCharArray();
        //            tb.Text = text.TrimEnd(characters[characters.Length - 1]);
        //            tb.IsEnabled = false;
        //            MessageBox.Show("U kunt hier geen nummers invullen");
        //            tb.IsEnabled = true;
        //        }
        //    }
        //}

        //private void TbPostcode_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    Console.WriteLine(IsPostcode(TbPostcode.Text));
        //}

        //private static bool IsPostcode(string text)
        //{
        //    return false;
        //}

    }
}
