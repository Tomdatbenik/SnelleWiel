using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snelle_Wiel.Classes
{
    class Order
    {
        public int oId { get; }
        public string oOmschrijving { get; }
        public string oTijd { get; }

        public Order(int Id, string Om, string Tijd)
        {
            this.oId = Id;
            this.oOmschrijving = Om;
            this.oTijd = Tijd;
        }
    }
}
