using Plugin.Geolocator;
using Snelle_Wiel.Classes;
using Snelle_Wiel.Objects;
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


        public BeheerPlanning(Database database)
        {
            InitializeComponent();
            this.db = database;
            setup();

            Planning p = new Planning(db);

            ObservableCollection<Planning> planning = new ObservableCollection<Planning>();
            


        }

        public async void setup()
        {
            Chauffeurone.ItemsSource = new ObservableCollection<Order>();
            Chauffeurtwo.ItemsSource = new ObservableCollection<Order>();
            Chauffeurthree.ItemsSource = new ObservableCollection<Order>();
            Chauffeurfour.ItemsSource = new ObservableCollection<Order>();
            Chauffeurfive.ItemsSource = new ObservableCollection<Order>();
            Chauffeursix.ItemsSource = new ObservableCollection<Order>();


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

        }


        private void ListView_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            oldlistbox = sender as ListView;
            oldlist = oldlistbox.ItemsSource as ObservableCollection<Order>;

            var listBox = sender as ListView;
            var listBoxItem = listBox.SelectedItem;

            if (listBoxItem != null)
            {
                DataObject dragData = new DataObject(_dropIdentifier, listBoxItem);
                DragDrop.DoDragDrop(listBox, dragData, DragDropEffects.Move);
            }

        }

        private void ListView_drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(_dropIdentifier))
            {
                Order item = e.Data.GetData(_dropIdentifier) as Order;
                DropOnListView(sender as ListView, item);
            }
        }

        public void DropOnListView(ListView targetlistbox, Order item)
        {
            oldlist.Remove(item);
            oldlistbox.ItemsSource = oldlist;
            ObservableCollection<Order> newlist = targetlistbox.ItemsSource as ObservableCollection<Order>;
            newlist.Add(item);
            targetlistbox.ItemsSource = newlist;
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
                int current = i;

                for (int o = 0; o < 6; o++)
                {
                    textblocks[o].Text = "Chauffeur " + current;
                    //current = dadelijk welke chauffeur


                    current += 1;
                }
            }
        }

        private void btnVolgende_Click(object sender, RoutedEventArgs e)
        {
            if(Chaufs.Count <= 6)
            {
                i += 6;
                int current = i;

                for (int o = 0; o < 6; o++)
                {
                    textblocks[o].Text = "Chauffeur " + current;
                    //current = dadelijk welke chauffeur


                    current += 1;
                }
            }
            else
            {
                MessageBox.Show("U heeft niet meer chauffeurs");
            }
        }
    }
}
