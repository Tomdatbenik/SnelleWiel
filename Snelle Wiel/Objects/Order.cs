using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snelle_Wiel.Objects
{
    public class Order
    {
        public int Id { get; private set; }
        public string Omschrijving { get; private set; }
        public string Status { get; private set; }
        public Locatie Start { get; set; }
        public Locatie Einde { get; set; }
        public int opgehaald {get;set;}

        public Order(int id,string omschrijving, Locatie start, Locatie einde)
        {
            this.Id = id;
            this.Omschrijving = omschrijving;
            this.Start = start;
            this.Einde = einde;
            opgehaald = 0;
        }
    }
}
