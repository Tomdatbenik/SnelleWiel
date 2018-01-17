using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snelle_Wiel.Objects
{
    class Producten
    {
        public int ProductID { get; set; }
        public int OrderID { get; set; }
        public string Omschrijving { get; set; }
        public int Inhoud { get; set; }
        public int Aantal { get; set; }
        public int Gewicht { get; set; }

        public Producten()
        {

        }

        public Producten(int id, int orderid, string omschrijving, int inhoud, int aantal, int gewicht)
        {
            this.ProductID = id;
            this.OrderID = orderid;
            this.Omschrijving = omschrijving;
            this.Inhoud = inhoud;
            this.Aantal = aantal;
            this.Gewicht = gewicht;
        }
    }
}
