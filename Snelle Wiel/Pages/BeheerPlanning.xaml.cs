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


        public BeheerPlanning()
        {
            InitializeComponent();
        }

        private void listBox_PreviewMouseMove(object sender, MouseEventArgs e)
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

        private void Listbox_drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(_dropIdentifier))
            {
                var item = e.Data.GetData(_dropIdentifier) as ListViewItem;
                DropOnListbox(sender as ListView, item);
            }
        }

        public void DropOnListbox(ListView targetlistbox, ListViewItem item)
        {
            oldlistbox.Items.Remove(item);
            targetlistbox.Items.Add(item);
        }

        private void Listbox_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(_dropIdentifier) ||
              sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }
    }
}
