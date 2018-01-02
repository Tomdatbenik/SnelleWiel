using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snelle_Wiel.Objects
{
    class Klant
    {
        public int Id;
        public string Naam { get; private set; }
        public string Actief { get; private set; }

        public Klant() { }

        public Klant(int id, string naam, string actief)
        {
            this.Id = id;
            this.Naam = naam;
            this.Actief = actief;
        }
    }
}
