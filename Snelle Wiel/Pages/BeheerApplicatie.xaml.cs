using Snelle_Wiel.Classes;
using Snelle_Wiel.Objects;
using Snelle_Wiel.Windows;
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
    /// Interaction logic for BeheerApplicatie.xaml
    /// </summary>
    public partial class BeheerApplicatie : Page
    {
        Database db;
        List<User> Users = new List<User>();
        public BeheerApplicatie(Database database)
        {
            InitializeComponent();
            this.db = database;
        }

        public void loadusers()
        {
            // Add columns
            var gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "UserId",
                DisplayMemberBinding = new Binding("PBarcode")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Naam",
                DisplayMemberBinding = new Binding("POmschrijving")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Woonplaats",
                DisplayMemberBinding = new Binding("PPrijs")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Adres",
                DisplayMemberBinding = new Binding("PPrijs")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Postcode",
                DisplayMemberBinding = new Binding("PPrijs")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Email",
                DisplayMemberBinding = new Binding("PPrijs")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Telefoon",
                DisplayMemberBinding = new Binding("PPrijs")
            });

            lvUserlist.View = gridView;

            DataTable dtResult = new DataTable();
            db.Connect();
            db.Verify();
            string query = "SELECT `UserId`,`UNaam`,`UWoonplaats`,`UAdres`,`UPostcode`,`UEmail`,`UTelefoon`";

            dtResult =  db.ExecuteStringQuery(query);
            foreach (DataRow dr in dtResult.Rows)
            {
                int id = int.Parse(dr["UserId"].ToString());
                string naam = dr["UNaam"].ToString();
                string woonplaats = dr["UWoonplaats"].ToString();
                string adres = dr["UAdres"].ToString();
                string postcode = dr["UPostcode"].ToString();
                string email = dr["UEmail"].ToString();
                string telefoonnr = dr["UTelefoon"].ToString();
                User u = new User(id,naam,woonplaats,adres,postcode,email,telefoonnr);
                Users.Add(u);
            }
            foreach(User u in Users)
            {
                lvUserlist.Items.Add(u);
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WAddUser aw = new WAddUser(this.db);
            aw.ShowDialog();
        }
    }
}
