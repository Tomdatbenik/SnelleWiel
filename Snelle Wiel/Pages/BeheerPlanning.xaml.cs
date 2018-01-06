using Plugin.Geolocator;
using Snelle_Wiel.Classes;
using Snelle_Wiel.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        

        string APISTRING = "https://maps.googleapis.com/maps/api/distancematrix/json?units=metric&origins=51.507764,5.397848&destinations=51.441642%2C5.469722";
        string origins = "&origins=51.507764,5.397848";


        public BeheerPlanning(Database database)
        {
            InitializeComponent();
            this.db = database;
            setup();
        }

        public async void setup()
        {
            Webservice ws = new Webservice();
            Coordinaat start = new Coordinaat("51.507764", "5.397848");
            Coordinaat einde = new Coordinaat("51.441642", "5.469722");

            Rit r = await ws.GetTravelTime(start,einde);
            Console.WriteLine(r.Distance);
            Console.WriteLine(r.Duration);
        }


        private void ListView_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            oldlistbox = sender as ListView;

            var listBox = sender as ListView;
            var listBoxItem = listBox.SelectedItem;

            if(listBoxItem != null)
            {
                DataObject dragData = new DataObject(_dropIdentifier, listBoxItem);
                DragDrop.DoDragDrop(listBox, dragData, DragDropEffects.Move);
            }
            
        }

        private void ListView_drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(_dropIdentifier))
            {
                var item = e.Data.GetData(_dropIdentifier) as ListViewItem;
                DropOnListView(sender as ListView, item);
            }
        }

        public void DropOnListView(ListView targetlistbox, ListViewItem item)
        {
            oldlistbox.Items.Remove(item);
            targetlistbox.Items.Add(item);
        }
    }
}
