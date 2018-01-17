using Snelle_Wiel.Objects;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for OrderInfo.xaml
    /// </summary>
    public partial class OrderInfo : Window
    {
        Order o;
        bool loaded = false;
        public OrderInfo(Order order)
        {
            InitializeComponent();
            o = order;
            if (o.Id != null)
            {
                fillboxes(o);
            }
            loaded = true;
        }


        public void fillboxes(Order o)
        {
            loaded = false;
            OrderId.Text = "Id: " + o.Id.ToString();
            Omschrijving.Text = "Omschrijving: " + o.Omschrijving;
            OphaalpuntAdres.Text = "Ophaal adres: " + o.Start.Adres;
            OphaalpuntPlaats.Text = "Ophaal plaats: " + o.Start.Plaats;
            OphaalpuntPostcode.Text = "Ophaal postcode: " + o.Start.Postcode;
            OphaalpuntLand.Text = "Ophaal land: " + o.Start.Land;
            EindbestemmingAdres.Text = "Aflever adres: " + o.Einde.Adres;
            EindbestemmingPlaats.Text = "Aflever plaats: " + o.Einde.Plaats;
            EindbestemmingPostcode.Text = "Aflever postcode: " + o.Einde.Postcode;
            EindbestemmingLand.Text = "Aflever land: " + o.Einde.Land;

            foreach (ComboBoxItem cbi in cbStatus.Items)
            {
                if (cbi.Tag.ToString() == o.Status)
                {
                    cbStatus.SelectedItem = cbi;
                    cbi.IsSelected = true;
                }
            }
            loaded = true;
        }


    }
}
