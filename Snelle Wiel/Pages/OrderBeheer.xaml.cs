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
    /// Interaction logic for OrderBeheer.xaml
    /// </summary>
    public partial class OrderBeheer : Page
    {
        Database db;
        public OrderBeheer(Database database )
        {
            InitializeComponent();
            this.db = database;
            setup();
        }

        public void setup()
        {
            LvOrders.ItemsSource = Getorders();
        }

        public ObservableCollection<Order> Getorders()
        {
            ObservableCollection<Order> Orders = new ObservableCollection<Order>();
            string query = "SELECT * FROM `Order`";
            DataTable data = db.ExecuteStringQuery(query);
            foreach (DataRow dr in data.Rows)
            {
                string OphaalpuntPlaats = dr["OphaalpuntPlaats"].ToString();
                string OphaalpuntAdres = dr["OphaalpuntAdres"].ToString();
                string OphaalpuntPostcode = dr["OphaalpuntPostcode"].ToString();
                string OphaalpuntLand = dr["OphaalpuntLand"].ToString();

                string EindbestemmingPlaats = dr["EindbestemmingPlaats"].ToString();
                string EindbestemmingAdres = dr["EindbestemmingAdres"].ToString();
                string EindbestemmingPostcode = dr["EindbestemmingPostcode"].ToString();
                string EindbestemmingLand = dr["EindbestemmingLand"].ToString();



                Locatie s = new Locatie(OphaalpuntPlaats, OphaalpuntAdres, OphaalpuntPostcode, OphaalpuntLand);
                Locatie e = new Locatie(EindbestemmingPlaats, EindbestemmingAdres, EindbestemmingPostcode, EindbestemmingLand);



                Order o = new Order(int.Parse(dr["OrderId"].ToString()), dr["Omschrijving"].ToString(), s, e);
                o.Status = dr["Status"].ToString();
                Orders.Add(o);
            }

            return Orders;
        }

        private void TbZoeken_TextChanged(object sender, TextChangedEventArgs e)
        {
                LvOrders.ItemsSource = null;
                string zoek = TbZoeken.Text;
                LvOrders.ItemsSource = SearchOrder(zoek);
        }

        private ObservableCollection<Order> SearchOrder(string zoek)
        {
            ObservableCollection<Order> Orders = new ObservableCollection<Order>();

            DataTable dt = db.ExecuteStringQuery("SELECT * FROM `Order` WHERE `Omschrijving` LIKE '" + zoek + "%' OR `Status` LIKE '"+zoek+ "%' OR `OphaalpuntPostcode` LIKE '" + zoek + "%'");
            foreach (DataRow dr in dt.Rows)
            {
                string OphaalpuntPlaats = dr["OphaalpuntPlaats"].ToString();
                string OphaalpuntAdres = dr["OphaalpuntAdres"].ToString();
                string OphaalpuntPostcode = dr["OphaalpuntPostcode"].ToString();
                string OphaalpuntLand = dr["OphaalpuntLand"].ToString();

                string EindbestemmingPlaats = dr["EindbestemmingPlaats"].ToString();
                string EindbestemmingAdres = dr["EindbestemmingAdres"].ToString();
                string EindbestemmingPostcode = dr["EindbestemmingPostcode"].ToString();
                string EindbestemmingLand = dr["EindbestemmingLand"].ToString();



                Locatie s = new Locatie(OphaalpuntPlaats, OphaalpuntAdres, OphaalpuntPostcode, OphaalpuntLand);
                Locatie e = new Locatie(EindbestemmingPlaats, EindbestemmingAdres, EindbestemmingPostcode, EindbestemmingLand);



                Order o = new Order(int.Parse(dr["OrderId"].ToString()), dr["Omschrijving"].ToString(), s, e);
                o.Status = dr["Status"].ToString();
                Orders.Add(o);
            }

            return Orders;
        }

        private void BtnWijzigen_Click(object sender, RoutedEventArgs e)
        {
            if (LvOrders.SelectedItem != null)
            {
                Order o = LvOrders.SelectedItem as Order;
                OrderInfo oi = new OrderInfo(o);
                oi.ShowDialog();
                setup();
            }
            else
            {
                MessageBox.Show("Selecteer een order.");
            }
        }
    }
}
