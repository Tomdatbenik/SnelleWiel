using Microsoft.Office.Interop.Word;
using Snelle_Wiel.Classes;
using System;
using System.Collections.Generic;
using System.IO;
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
//using Microsoft.Win32;
//using System.Printing;
using System.Windows.Xps.Packaging;
//using Microsoft.Office.Interop.Word;



namespace Snelle_Wiel.Pages
{
    /// <summary>
    /// Interaction logic for BeheerFacturatie.xaml
    /// </summary>
    public partial class BeheerFacturatie : System.Windows.Controls.Page
    {
        Database db;
        public BeheerFacturatie(Database database)
        {
            InitializeComponent();
            VulListView();
            this.db = database;
            //GetDocument();
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //private XpsDocument ConvertWordDocToXPSDoc(string wordDocName, string xpsDocName)

        //{
        //    // Create a WordApplication and add Document to it
        //    Microsoft.Office.Interop.Word.Application
        //    wordApplication = new Microsoft.Office.Interop.Word.Application();
        //    wordApplication.Documents.Add(wordDocName);

        //    Document doc = wordApplication.ActiveDocument;
        //    // You must ensure you have Microsoft.Office.Interop.Word.Dll version 12.
        //    // Version 11 or previous versions do not have WdSaveFormat.wdFormatXPS option
        //    try
        //    {
        //        doc.SaveAs(xpsDocName, WdSaveFormat.wdFormatXPS);
        //        wordApplication.Quit();
        //        XpsDocument xpsDoc = new XpsDocument(xpsDocName, System.IO.FileAccess.Read);
        //        return xpsDoc;
        //    }
        //    catch (Exception exp)
        //    {
        //        string str = exp.Message;
        //    }
        //    return null;
        //}

        //private void BrowseButton_Click(object sender, RoutedEventArgs e)

        //{
        //    // Create OpenFileDialog
        //    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

        //    // Set filter for file extension and default file extension
        //    dlg.DefaultExt = ".doc";
        //    dlg.Filter = "Word documents (.doc)|*.doc";

        //    // Display OpenFileDialog by calling ShowDialog method

        //    Nullable<bool> result = dlg.ShowDialog();

        //    // Get the selected file name and display in a TextBox
        //    if (result == true)
        //    {
        //        if (dlg.FileName.Length > 0)
        //        {
        //            SelectedFileTextBox.Text = dlg.FileName;
        //            string newXPSDocumentName = String.Concat(System.IO.Path.GetDirectoryName(dlg.FileName), "\\",
        //                           System.IO.Path.GetFileNameWithoutExtension(dlg.FileName), ".xps");

        //            // Set DocumentViewer.Document to XPS document
        //            documentViewer1.Document =
        //                ConvertWordDocToXPSDoc(dlg.FileName, newXPSDocumentName).GetFixedDocumentSequence();
        //        }
        //    }
        //}

        //public void GetDocument()
        //{
        //    XpsDocument xpsDocument = new XpsDocument("C:/Users/ronli/Downloads/RFC snelle wiel/OHA Factuur SNWI.xps", FileAccess.Read);
        //    FixedDocumentSequence fds = xpsDocument.GetFixedDocumentSequence();
        //    docViewer.Document = fds;
        //}
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void VulListView ()
        {
            lvOrders.Items.Add("Order 1");
            lvOrders.Items.Add("Order 2");
            lvOrders.Items.Add("Order 3");
            lvOrders.Items.Add("Order 4");
            lvOrders.Items.Add("Order 5");
        }

        private void lvOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string SelectedItem = lvOrders.SelectedIndex.ToString();
            if (SelectedItem == "Order 1")
            {
                MessageBox.Show(SelectedItem);
            }
            
        }
    }
}
