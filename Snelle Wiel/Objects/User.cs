using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snelle_Wiel.Objects
{
    public class User
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Woonplaats { get; set; }
        public string Adres { get; set; }
        public string Postcode { get; set; }
        public string Email { get; set; }
        public string Telefoonnr { get; set; }

        public User(int id, string naam, string woonplaats, string adres, string postcode, string email, string telefoonnr)
        {
            this.Id = id;
            this.Naam = naam;
            this.Woonplaats = woonplaats;
            this.Adres = adres;
            this.Postcode = postcode;
            this.Email = email;
            this.Telefoonnr = telefoonnr;
        }


    }
}
