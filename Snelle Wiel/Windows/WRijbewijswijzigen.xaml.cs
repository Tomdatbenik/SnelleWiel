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
        #endregion

        public WRijbewijswijzigen(Database database, int id)
        {
            InitializeComponent();
            this.db = database;
            Setup(id);
        }

        public void Setup(int id)
        {
            UserData ud = new UserData(this.db);
            LvGot.ItemsSource = ud.GetRijbewijzenOnId(id);

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
            targetlistbox.Items.Add(item);
        }
    }
}
