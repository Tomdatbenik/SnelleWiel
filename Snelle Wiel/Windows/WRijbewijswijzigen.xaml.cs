using Snelle_Wiel.Classes;
using Snelle_Wiel.Objects;
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
using System.Windows.Shapes;

namespace Snelle_Wiel.Windows
{
    /// <summary>
    /// Interaction logic for WRijbewijswijzigen.xaml
    /// </summary>
    public partial class WRijbewijswijzigen : Window
    {

        #region Variables
        private ListView oldlistbox;
        private ObservableCollection<Rijbewijs> oldlist;
        private static readonly string _dropIdentifier = "dropIdentifier";
        private Database db;
        private int Id;
        #endregion

        public WRijbewijswijzigen(Database database, int id)
        {
            InitializeComponent();
            this.db = database;
            Id = id;
            Setup(id);
        }

        public void Setup(int id)
        {
            UserData ud = new UserData(this.db);
            LvGot.ItemsSource = ud.GetRijbewijzenOnId(id);
            LvGive.ItemsSource = ud.GetRijbewijzendiehijnietheeftonid(id);
        }

        private void ListView_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            oldlistbox = sender as ListView;
            oldlist = oldlistbox.ItemsSource as ObservableCollection<Rijbewijs>;

            //get rijbewijs obeservarable list die de chauffeur niet heeft



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
                Rijbewijs item = e.Data.GetData(_dropIdentifier) as Rijbewijs;
                DropOnListView(sender as ListView, item);
            }
        }

        public void DropOnListView(ListView targetlistbox, Rijbewijs item)
        {
            oldlist.Remove(item);
            oldlistbox.ItemsSource = oldlist;
            ObservableCollection<Rijbewijs> newlist = targetlistbox.ItemsSource as ObservableCollection<Rijbewijs>;
            newlist.Add(item);
            targetlistbox.ItemsSource = newlist;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Rijbewijs> Rijbewijzen = LvGot.ItemsSource as ObservableCollection<Rijbewijs>;
            ObservableCollection<Rijbewijs> nrijbewijzen = LvGive.ItemsSource as ObservableCollection<Rijbewijs>;
            UserData ud = new UserData(this.db);
            ud.AddRijbewijzon(this.Id, Rijbewijzen,nrijbewijzen);
            this.Close();
        }

        private void LvGive_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView havinglistview = LvGot as ListView;
            ObservableCollection<Rijbewijs> HaveCollection = havinglistview.ItemsSource as ObservableCollection<Rijbewijs>;
            ListView givelist = LvGive as ListView;
            ObservableCollection<Rijbewijs> Givecollection = givelist.ItemsSource as ObservableCollection<Rijbewijs>;

            Rijbewijs listBoxItemhas = LvGive.SelectedItem as Rijbewijs;


            if (listBoxItemhas != null)
            {
                HaveCollection.Add(listBoxItemhas);
                Givecollection.Remove(listBoxItemhas);

                LvGive.ItemsSource = Givecollection;
                LvGot.ItemsSource = HaveCollection;
            }
        }

        private void LvGot_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView havinglistview = LvGot as ListView;
            ObservableCollection<Rijbewijs> HaveCollection = havinglistview.ItemsSource as ObservableCollection<Rijbewijs>;
            ListView givelist = LvGive as ListView;
            ObservableCollection<Rijbewijs> Givecollection = givelist.ItemsSource as ObservableCollection<Rijbewijs>;

            Rijbewijs listBoxItemhas = LvGot.SelectedItem as Rijbewijs;


            if(listBoxItemhas != null)
            {
                HaveCollection.Remove(listBoxItemhas);
                Givecollection.Add(listBoxItemhas);

                LvGive.ItemsSource = Givecollection;
                LvGot.ItemsSource = HaveCollection;
            }
        }
    }
}
