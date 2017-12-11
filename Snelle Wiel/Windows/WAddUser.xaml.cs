using Snelle_Wiel.Classes;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for WAddUser.xaml
    /// </summary>
    public partial class WAddUser : Window
    {
        Database db;
        cLogin lg;
        public WAddUser(Database database)
        {
            InitializeComponent();
            this.db = database;
            this.lg = new cLogin(database);
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            if (TbLName.Text != "" && PbPass.Password != "" && TbName.Text != "")
            {
                string tag = CbRole.SelectedValue.ToString();
                int Roleid = int.Parse(tag);
                lg.AddUser(TbLName.Text, PbPass.Password, Roleid,TbName.Text,TbWoonplaats.Text, TbAdres.Text, TbPostcode.Text, TbEmail.Text,TbTelefoon.Text);
                this.Close();
            }
            else
            {
                TbError.Text = "Vul de gevraagde gegevens in";
            }
        }

        //returns true if its allowed
        private static bool IsTextAllowed(string text)
        {
            char[] characters = text.ToCharArray();
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
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

        private void TbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if(IsTextAllowed(tb.Text))
            {
                Console.WriteLine("Het is text!");
            }
            else
            {
                Console.WriteLine("Het is een nummer!");
            }
        }
    }
}
