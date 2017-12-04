using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snelle_Wiel.Objects
{
    class User
    {
        public int Id;
        public string Naam;
        public string Woonplaats;
        public string Adres;
        public string Postcode;
        public string Email;
        public string Telefoonnr;

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
