using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snelle_Wiel.Objects
{
    public class Locatie
    {
        public string Plaats { get; private set; }
        public string Adres { get; private set; }
        public string Postcode { get; private set; }

        public string Land { get; private set; }

        public Locatie() { }

        public Locatie(string loc,string adres, string postcode,string land)
        {
            this.Plaats = loc;
            this.Adres = adres;
            this.Postcode = postcode;
            this.Land = land;
        }
    }
}
