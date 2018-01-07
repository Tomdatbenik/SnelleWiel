using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snelle_Wiel.Objects
{
    public class Order
    {
        public int Id { get; }
        public Locatie Start;
        public Locatie Einde;


        public Order(int Id,Locatie start, Locatie einde)
        {
            this.Id = Id;
            this.Start = start;
            this.Einde = einde
        }
    }
}
