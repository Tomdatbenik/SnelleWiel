using Snelle_Wiel.Classes;
using Snelle_Wiel.Objects;
using Snelle_Wiel.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

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
                System.Windows.MessageBox.Show("Selecteer een order.");
            }
        }

        private void BtnXML_Click(object sender, RoutedEventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "XML files|*.xml";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = theDialog.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            string filename = theDialog.FileName;
                            XmlDocument doc = new XmlDocument();
                            doc.Load(filename);
                            XMLRootObject ro = new XMLRootObject();

                            foreach (XmlNode xmlitem in doc.SelectNodes("bezorgorder"))
                            {
                                    //XMLItem item = new XMLItem();

                                    //item.Name = xmlitem.ChildNodes[i].Name;
                                    //item.Innertext = xmlitem.ChildNodes[i].InnerText;
                                    //item.Innerxml = xmlitem.ChildNodes[i].InnerXml;

                                    //ro.Add(item);


                                    string omschrijving = xmlitem["ophaaladres"]["naam"].InnerText;
                                    string Status = "In behandeling";
                                    string OphaalpuntPlaats = xmlitem["ophaaladres"]["plaats"].InnerText;
                                    string OphaalpuntAdres = xmlitem["ophaaladres"]["straat"].InnerText + " " + xmlitem["ophaaladres"]["huisnr"].InnerText;
                                    string OphaalpuntPostcode = xmlitem["ophaaladres"]["postcode"].InnerText;
                                    string OphaalpuntLand = "Nederlands";
                                    string EindbestemmingPlaats = xmlitem["afleveradres"]["plaats"].InnerText;
                                    string EindbestemmingAdres = xmlitem["afleveradres"]["straat"].InnerText + " " + xmlitem["afleveradres"]["huisnr"].InnerText;
                                    string EindbestemmingPostcode = xmlitem["afleveradres"]["postcode"].InnerText; 
                                    string EindbestemmingLand = "Nederlands";
                                    string Klantid = xmlitem["opdrachtgever"]["nr"].InnerText;

                                    string query = "INSERT INTO `snellewiel`.`Order` (`Omschrijving`, `Status`, `OphaalpuntPlaats`, `OphaalpuntAdres`, `OphaalpuntPostcode`, `OphaalpuntLand`, `EindbestemmingPlaats`, `EindbestemmingAdres`, `EindbestemmingPostcode`, `EindbestemmingLand`, `Klantid`)" +
                                        " VALUES ('" + omschrijving + "'," +
                                        " '" + Status + "'," +
                                        " '" + OphaalpuntPlaats + "'," +
                                        " '" + OphaalpuntAdres + "'," +
                                        " '" + OphaalpuntPostcode + "'," +
                                        " '" + OphaalpuntLand + "'," +
                                        " '" + EindbestemmingPlaats + "'," +
                                        " '" + EindbestemmingAdres + "'," +
                                        " '" + EindbestemmingPostcode + "'," +
                                        " '" + EindbestemmingLand + "'," +
                                        " '" + Klantid + "');";

                                    db.ExecuteStringQuery(query);
                                    setup();
                            }


                            //Console.WriteLine(ro.Items);


                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}
