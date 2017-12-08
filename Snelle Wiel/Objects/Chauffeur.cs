using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snelle_Wiel.Objects
{
    class Chauffeur
    {
        public int Id { get; set; }
        public int Rol { get; set; }
        public string Naam { get; set; }
        public string Woonplaats { get; set; }
        public string Adres { get; set; }
        public string Postcode { get; set; }
        public string Email { get; set; }
        public string Telefoonnr { get; set; }
        public List<int> Rijbeijzen { get; set; }
    }
}
