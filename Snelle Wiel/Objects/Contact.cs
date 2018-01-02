using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snelle_Wiel.Objects
{
    class Contact
    {
        public string Naam { get; private set; }
        public string Telefoon { get; private set; }
        public string Email { get; private set; }

        public Contact() { }

        public Contact(string naam, string telefoon, string email)
        {
            this.Naam = naam;
            this.Telefoon = telefoon;
            this.Email = email;
        }
    }
}
