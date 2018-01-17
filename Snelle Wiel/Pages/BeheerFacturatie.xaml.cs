using Microsoft.Office.Interop.Word;
using Novacode;
using Snelle_Wiel.Classes;
using Snelle_Wiel.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
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
using System.Windows.Xps.Packaging;




namespace Snelle_Wiel.Pages
{
    /// <summary>
    /// Interaction logic for BeheerFacturatie.xaml
    /// </summary>
    public partial class BeheerFacturatie : System.Windows.Controls.Page
    {
        Order SelectedOrder;
        Database db;
        ObservableCollection<Producten> producten = new ObservableCollection<Producten>();
        double prijs1 = 5.25;
        double prijs2 = 10.63;
        double prijs3 = 20.50;
        public BeheerFacturatie(Database database)
        {
            InitializeComponent();
            this.db = database;
            lvOrders.ItemsSource = Getorders();
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

        private void lvOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectedOrder = lvOrders.SelectedItem as Order;
            string query = "SELECT * FROM `Producten` WHERE `OrderId` = '" + SelectedOrder.Id + "'";
            System.Data.DataTable data = db.ExecuteStringQuery(query);
            foreach (DataRow dr in data.Rows)
            {
                int ProductID = Int32.Parse(dr["ProductId"].ToString());
                int OrderID = Int32.Parse(dr["OrderId"].ToString());
                string Omschrijving = dr["Omschrijving"].ToString();
                int Inhoud = Int32.Parse(dr["Inhoud"].ToString());
                int Aantal = Int32.Parse(dr["Aantal"].ToString());
                int Gewicht = Int32.Parse(dr["Gewicht"].ToString());

                Producten p = new Producten(ProductID, OrderID, Omschrijving, Inhoud, Aantal, Gewicht);
                producten.Add(p);

            }
            lvOrderArtikelen.ItemsSource = producten;
            btGenereer.Visibility = System.Windows.Visibility.Visible;
        }
        private void btOrder1_Click(object sender, RoutedEventArgs e)
        {
            //string[] row = {"Adres 1", "Adres 2", "Bak bier", "17-1-2018", "Omschrijving" };
            //var listViewItem = new ListViewItem(row);
            //lvOrderArtikelen.Items.Add(listViewItem);
        }
        public ObservableCollection<Order> Getorders()
        {
            ObservableCollection<Order> Orders = new ObservableCollection<Order>();
            string query = "SELECT * FROM `Order`;";
            System.Data.DataTable data = db.ExecuteStringQuery(query);
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
                Orders.Add(o);
            }
            return Orders;
        }

        private void btGenereer_Click(object sender, RoutedEventArgs e)
        {

            // Modify to suit your machine:
            string fileName = @"C:\Users\Ron\Documents\DocXExample.docx";
  
            // Create a document in memory:
            var doc = DocX.Create(fileName);


            string headlineText = "Factuur\n";
            // A formatting object for our headline:
            var headLineFormat = new Formatting();
            headLineFormat.Size = 36;
            headLineFormat.Position = 12;
            headLineFormat.Bold = true;

            // Set up our paragraph contents:
            string paraOne = ""
                + "\n\nGroene Vingers\n"
                + "Crediteurenadministratie   Hr. A. van Hest\n"
                + "Kanaaldijk 34\n"
                + "5501 XL  Best\n\n\n\n\n\n";

            // Set up our paragraph contents:
            string paraTwo = ""
                + "Factuurnummer:	14000345\n"
                + "Factuurdatum:	              " + DateTime.Today.ToString("d") +"\n\n";
            

            // Add a Table to this document.
            Novacode.Table t = doc.AddTable(8, 8);
            // Specify some properties for this Table.
            t.Alignment = Alignment.left;
            t.Design = TableDesign.TableNormal;
            // Add content to this Table.
            t.Rows[0].Cells[0].Paragraphs.First().Append("Datum").Bold();
            t.Rows[0].Cells[0].Width = 50;
            t.Rows[0].Cells[1].Paragraphs.First().Append("Order").Bold();
            t.Rows[0].Cells[1].Width = 50;
            t.Rows[0].Cells[2].Paragraphs.First().Append("L/R").Bold();
            t.Rows[0].Cells[2].Width = 50;
            t.Rows[0].Cells[3].Paragraphs.First().Append("Postcode").Bold();
            t.Rows[0].Cells[3].Width = 50;
            t.Rows[0].Cells[4].Paragraphs.First().Append("Aantal").Bold();
            t.Rows[0].Cells[4].Width = 50;
            t.Rows[0].Cells[5].Paragraphs.First().Append("KG").Bold();
            t.Rows[0].Cells[5].Width = 50;
            t.Rows[0].Cells[6].Paragraphs.First().Append("M3").Bold();
            t.Rows[0].Cells[6].Width = 50;
            t.Rows[0].Cells[7].Paragraphs.First().Append("Prijs").Bold();
            t.Rows[0].Cells[7].Width = 50;
            t.Rows[1].Cells[0].Paragraphs.First().Append("19-11");
            t.Rows[1].Cells[1].Paragraphs.First().Append("123456");
            t.Rows[1].Cells[2].Paragraphs.First().Append("L");
            t.Rows[1].Cells[3].Paragraphs.First().Append("5512CC");
            t.Rows[1].Cells[4].Paragraphs.First().Append("5");
            t.Rows[1].Cells[5].Paragraphs.First().Append("10");
            t.Rows[1].Cells[6].Paragraphs.First().Append("0.5");
            t.Rows[1].Cells[7].Paragraphs.First().Append(""+ prijs1 +"");

            t.Rows[2].Cells[0].Paragraphs.First().Append("19-11");
            t.Rows[2].Cells[1].Paragraphs.First().Append("123457");
            t.Rows[2].Cells[2].Paragraphs.First().Append("R");
            t.Rows[2].Cells[3].Paragraphs.First().Append("5945AB");
            t.Rows[2].Cells[4].Paragraphs.First().Append("5");
            t.Rows[2].Cells[5].Paragraphs.First().Append("5");
            t.Rows[2].Cells[6].Paragraphs.First().Append("1");
            t.Rows[2].Cells[7].Paragraphs.First().Append(""+ prijs2 +"");

            t.Rows[3].Cells[0].Paragraphs.First().Append("19-11");
            t.Rows[3].Cells[1].Paragraphs.First().Append("123458");
            t.Rows[3].Cells[2].Paragraphs.First().Append("L");
            t.Rows[3].Cells[3].Paragraphs.First().Append("5589CB");
            t.Rows[3].Cells[4].Paragraphs.First().Append("6");
            t.Rows[3].Cells[5].Paragraphs.First().Append("24");
            t.Rows[3].Cells[6].Paragraphs.First().Append("2");
            t.Rows[3].Cells[7].Paragraphs.First().Append(""+ prijs3 +"");

            t.Rows[4].Cells[6].Paragraphs.First().Append("");
            t.Rows[4].Cells[7].Paragraphs.First().Append("");

            double totaalexbtw = prijs1 + prijs2 + prijs3;
            double BTW = totaalexbtw * 0.21;
            double totaal = totaalexbtw + BTW;

            t.Rows[5].Cells[6].Paragraphs.First().Append("Ex BTW:").Bold();
            t.Rows[6].Cells[6].Paragraphs.First().Append("BTW:").Bold();
            t.Rows[7].Cells[6].Paragraphs.First().Append("Totaal:").Bold();

            t.Rows[5].Cells[7].Paragraphs.First().Append(""+ totaalexbtw +"");
            t.Rows[6].Cells[7].Paragraphs.First().Append("" + BTW + "");
            t.Rows[7].Cells[7].Paragraphs.First().Append("€"+ totaal +"");



            // Set up our paragraph contents:
            string paraThree = ""
                + "\n\n\nTe betalen voor 12 december 2016 onder vermelding van het factuurnummer. Bij betaling voor 29 november geldt een korting van 9%.\n\n"
                + "De Algemene Leveringsvoorwaarden gelden. Deze kunt u vinden op  www.snwi.nl/alv. Informatie over onze actuele tarieven kunt u vinden op www.snwi.nl / tarieven.\n\n\n\n\n\n";

            //// Set up our paragraph contents:
            string paraFour = ""
                + "__________________________________________________________________________________\n\n";

            // Add a Table to this document.
            Novacode.Table footer = doc.AddTable(3, 2);
            // Specify some properties for this Table.
            footer.Alignment = Alignment.left;
            footer.Design = TableDesign.TableNormal;
            // Add content to this Table.
            footer.Rows[0].Cells[0].Paragraphs.First().Append("Snelle Wiel");
            footer.Rows[0].Cells[1].Paragraphs.First().Append("IBAN:  NL44ASNB0034178902");
            footer.Rows[1].Cells[0].Paragraphs.First().Append("Spoordijk 23");
            footer.Rows[1].Cells[1].Paragraphs.First().Append("KVK nr:  10672392 Eindhoven");
            footer.Rows[2].Cells[0].Paragraphs.First().Append("5610 XL  Best");
            footer.Rows[2].Cells[1].Paragraphs.First().Append("t.v.mierlo@snwi.nl");


            // Body Formatting
            var paraFormat = new Formatting();
            paraFormat.FontColor = System.Drawing.Color.Gray;

            //Insert all objects in document
            doc.InsertParagraph(headlineText, false, headLineFormat);
            doc.InsertParagraph(paraOne);
            doc.InsertParagraph(paraTwo);
            doc.InsertTable(t);
            doc.InsertParagraph(paraThree);
            doc.InsertParagraph(paraFour, false, paraFormat);
            doc.InsertTable(footer);




            // Save to the output directory:
            doc.Save();
  
            // Open in Word:
            Process.Start("WINWORD.EXE", fileName);
        }
    }
}
