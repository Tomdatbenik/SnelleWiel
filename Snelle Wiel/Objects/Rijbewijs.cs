using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snelle_Wiel.Objects
{
    public class Rijbewijs
    {
        public int Id { get; set; }
        public string Catogorie { get; set; }

        public string Omschrijving { get; set; }

        public Rijbewijs() { }

        public Rijbewijs(int id, string catogorie, string omschrijving)
        {
            this.Id = id;
            this.Catogorie = catogorie;
            this.Omschrijving = omschrijving;
        }

    }
}
