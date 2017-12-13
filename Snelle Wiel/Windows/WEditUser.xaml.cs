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
    /// Interaction logic for WEditUser.xaml
    /// </summary>
    public partial class WEditUser : Window
    {
        Database db;
        cLogin lg;
        int id;

        public WEditUser(Database database, int Id)
        {
            InitializeComponent();
            this.db = database;
            this.lg = new cLogin(database);
            this.id = Id;
            string query = "SELECT * FROM Users WHERE UserId = '"+ id.ToString() +"'";
            DataTable dt = db.ExecuteStringQuery(query);
            foreach (DataRow dr in dt.Rows)
            {
                TbLName.Text = dr["ULoginname"].ToString();
                CbRole.SelectedIndex = int.Parse(dr["RoleId"].ToString()) -1;
                TbName.Text = dr["Unaam"].ToString();
                TbWoonplaats.Text = dr["UWoonplaats"].ToString();
                TbAdres.Text = dr["UAdres"].ToString();
                TbPostcode.Text = dr["UPostcode"].ToString();
                TbEmail.Text = dr["UEmail"].ToString();
                TbTelefoon.Text = dr["UTelefoon"].ToString();
            }
        }

        private void BtnEditUser_Click(object sender, RoutedEventArgs e)
        {
            if(TbLName.Text != "" && TbName.Text != "" && TbWoonplaats.Text != "" && TbAdres.Text != "" && TbPostcode.Text != "" && TbEmail.Text != "" && TbTelefoon.Text != "")
            {
                string tag = CbRole.SelectedValue.ToString();
                int Roleid = int.Parse(tag);
                lg.EditUser(this.id,TbLName.Text, PbPass.Password, Roleid, TbName.Text, TbWoonplaats.Text, TbAdres.Text, TbPostcode.Text, TbEmail.Text, TbTelefoon.Text);
                this.Close();
            }
            else
            {
                TbError.Text = "Vul de gevraagde gegevens in";
            }
        }

        private static bool IsTextAllowed(string text)
        {
            char[] characters = text.ToCharArray();
            Regex regex = new Regex("[^0-9]+"); //regex that matches disallowed text
            bool istext = false;
            foreach (char c in characters)
            {
                if (regex.IsMatch(c.ToString()))
                {
                    istext = true;
                }
                else
                {
                    return false;
                }
            }
            return istext;
        }

        private void Textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (!IsTextAllowed(tb.Text))
            {
                string text = tb.Text;
                if (text != "")
                {
                    char[] characters = text.ToCharArray();
                    tb.Text = text.TrimEnd(characters[characters.Length - 1]);
                    tb.IsEnabled = false;
                    MessageBox.Show("U kunt hier geen nummers invullen");
                    tb.IsEnabled = true;
                }
            }
        }
    }
}
