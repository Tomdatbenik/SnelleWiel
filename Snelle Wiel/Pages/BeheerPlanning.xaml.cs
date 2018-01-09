using Plugin.Geolocator;
using Snelle_Wiel.Classes;
using Snelle_Wiel.Objects;
using Snelle_Wiel.Windows;
using System;
using System.Collections;
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
    /// Interaction logic for BeheerPlanning.xaml
    /// </summary>
    public partial class BeheerPlanning : Page
    {
        #region Variables
        private ListView oldlistbox;
        private static readonly string _dropIdentifier = "dropIdentifier";
        #endregion

        Database db;
        ObservableCollection<Order> oldlist = new ObservableCollection<Order>();
        int i = 1;
        ObservableCollection<User> Chaufs = new ObservableCollection<User>();
        List<ListView> Chauflisten = new List<ListView>();
        List<TextBlock> textblocks = new List<TextBlock>();
        List<Rit> ritten = new List<Rit>();
        ObservableCollection<Order> Orders = new ObservableCollection<Order>();


        string APISTRING = "https://maps.googleapis.com/maps/api/distancematrix/json?units=metric&origins=51.507764,5.397848&destinations=51.441642%2C5.469722";
        string origins = "&origins=51.507764,5.397848";
        Planning p;
        MainWindow main;
        public BeheerPlanning(Database database, MainWindow home)
        {
            InitializeComponent();
            main = home;
            this.db = database;
            p = new Planning(db);
            Setupplanning();
        }

        public async void Setupplanning()
        {
            ObservableCollection<PlanningItem> Items = new ObservableCollection<PlanningItem>();
            List<PlanningItem> Totalitems = new List<PlanningItem>();
            setup();
            btReset.IsEnabled = false;
            btnGenereer.IsEnabled = false;
            LvOrders.ItemsSource = null;
            int i = 0;


            foreach (ListView lv in Chauflisten)
            {
                lv.ItemsSource = new ObservableCollection<PlanningItem>();
            }
            MessageBox.Show("De berekeningen worden in de achtergrond gemaakt. U kunt verder nadat alles berekend is. De applicatie niet sluiten!");
            Loading l = new Loading();
            l.Show();
            foreach (User c in Chaufs)
            {
                Console.WriteLine("In behandeling is chauffeur: " + c.Naam);
                if(dpdate.Text != "")
                {
                    Items = await p.GetPlanningItems(c.Id, dpdate.Text);
                    foreach(PlanningItem pl in Items)
                    {
                        Totalitems.Add(pl);
                    }
                    Chauflisten[i].ItemsSource = Items;
                }
                else
                {
                    string[] date = DateTime.Now.Date.ToString().ToString().Split(' ');
                    string datum = date[0];
                    Items = await p.GetPlanningItems(c.Id, datum);
                    foreach (PlanningItem pl in Items)
                    {
                        Totalitems.Add(pl);
                    }


                    Chauflisten[i].ItemsSource = Items;
                }
                i++;
            }

            if (dpdate.Text != "" && Items.Count != 0 && Totalitems.Count != 0)
            {
                string[] date = DateTime.Now.Date.ToString().ToString().Split(' ');
                string datum = date[0];

                string q = "SELECT PlanningId FROM Planning WHERE `Date` = '" + datum + "';";
                DataTable da = db.ExecuteStringQuery(q);

                if (da.Rows.Count == 0)
                {
                    p.saveplanning(Totalitems, dpdate.Text);
                }
            }
            else
            {
                string[] date = DateTime.Now.Date.ToString().ToString().Split(' ');
                string datum = date[0];

                string q = "SELECT PlanningId FROM Planning WHERE `Date` = '" + datum + "';";
                DataTable da = db.ExecuteStringQuery(q);

                if (da.Rows.Count == 0)
                {
                    da = null;
                }

                if(da == null && Items.Count != 0 && Totalitems.Count != 0)
                {
                    p.saveplanning(Totalitems, datum);
                }
            }

            l.Hide();
            setup();
        }

        public async void setup()
        {
            LvOrders.ItemsSource = null;
            Orders.Clear();
            Chaufs.Clear();
            Chauflisten.Clear();
            textblocks.Clear();

            Chauflisten.Add(Chauffeurone);
            Chauflisten.Add(Chauffeurtwo);
            Chauflisten.Add(Chauffeurthree);
            Chauflisten.Add(Chauffeurfour);
            Chauflisten.Add(Chauffeurfive);
            Chauflisten.Add(Chauffeursix);

            textblocks.Add(tbchaufone);
            textblocks.Add(tbchauftwo);
            textblocks.Add(tbchauftree);
            textblocks.Add(tbchauffour);
            textblocks.Add(tbchauffive);
            textblocks.Add(tbchaufsix);

            DataTable dt = db.ExecuteStringQuery("SELECT UserId, RoleId, UNaam, UWoonplaats, UAdres, UPostcode, UEmail, UTelefoon FROM Users WHERE RoleId = 2");
            foreach (DataRow dr in dt.Rows)
            {
                string id = dr["UserId"].ToString();
                string naam = dr["UNaam"].ToString();
                string woonplaats = dr["UWoonplaats"].ToString();
                string adres = dr["UAdres"].ToString();
                string postcode = dr["UPostcode"].ToString();
                string email = dr["UEmail"].ToString();
                string telefoon = dr["UTelefoon"].ToString();

                User u = new User(int.Parse(id), naam, woonplaats, adres, postcode, email, telefoon);
                Chaufs.Add(u);
            }

            for (int o = 0; o < 6; o++)
            {
                if (Chaufs.Count() > o)
                {
                    textblocks[o].Text = Chaufs[o].Naam;
                }
                else
                {
                    textblocks[o].Text = "placeholder";
                }
            }



                string query = "SELECT * FROM `Order` WHERE Gebruik = '0';";
            DataTable data = db.ExecuteStringQuery(query);
            foreach(DataRow dr in data.Rows)
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



                Order o = new Order(int.Parse(dr["OrderId"].ToString()),dr["Omschrijving"].ToString(),s,e);
                Orders.Add(o);
            }

            LvOrders.ItemsSource = Orders;
            btReset.IsEnabled = true;
            btnGenereer.IsEnabled = true;
        }


        private void ListView_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            //oldlistbox = sender as ListView;
            //oldlist = oldlistbox.ItemsSource as ObservableCollection<Order>;

            //var listBox = sender as ListView;
            //var listBoxItem = listBox.SelectedItem;

            //if (listBoxItem != null)
            //{
            //    DataObject dragData = new DataObject(_dropIdentifier, listBoxItem);
            //    DragDrop.DoDragDrop(listBox, dragData, DragDropEffects.Move);
            //}

        }

        private void ListView_drop(object sender, DragEventArgs e)
        {
            //if (e.Data.GetDataPresent(_dropIdentifier))
            //{
            //    Order item = e.Data.GetData(_dropIdentifier) as Order;
            //    DropOnListView(sender as ListView, item);
            //}
        }

        public void DropOnListView(ListView targetlistbox, Order item)
        {
            //oldlist.Remove(item);
            //oldlistbox.ItemsSource = oldlist;
            //ObservableCollection<Order> newlist = targetlistbox.ItemsSource as ObservableCollection<Order>;
            //newlist.Add(item);
            //targetlistbox.ItemsSource = newlist;
        }

        private void btnVorige_Click(object sender, RoutedEventArgs e)
        {
            if(i == 1)
            {
                MessageBox.Show("U kunt niet verder terug");
            }
            else
            {
                i -= 6;

                for (int o = i - 1; o < i; o++)
                {
                    if(Chaufs.Count() < i)
                    {
                        textblocks[o].Text = Chaufs[i].Naam;
                    }
                }
            }
        }

        private void btnVolgende_Click(object sender, RoutedEventArgs e)
        {
            if(Chaufs.Count <= i)
            {
                i += 6;

                for (int o = i - 1; o < i; o++)
                {
                    if (Chaufs.Count() < i)
                    {
                        textblocks[o].Text = Chaufs[i].Naam;
                    }
                }
            }
            else
            {
                MessageBox.Show("U heeft niet meer chauffeurs");
            }
        }

        private void btReset_Click(object sender, RoutedEventArgs e)
        {
            btReset.IsEnabled = false;
            btnGenereer.IsEnabled = false;
            p.calculateordersperchauf();
            setup();
            Setupplanning();
        }

        private void dpdate_KeyDown(object sender, KeyEventArgs e)
        {
             e.Handled = true;
        }

    }
}
