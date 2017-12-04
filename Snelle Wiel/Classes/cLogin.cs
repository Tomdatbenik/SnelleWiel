using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snelle_Wiel.Classes
{
    class cLogin
    {
        Database db;
        public cLogin(Database database)
        {
            this.db = database;
        }
        public void AddUser(string Name, string Pass,int Role,string naam, string woonplaats, string adres, string postcode, string email,string telefoon)
        {
            string Salt = BCrypt.GenerateSalt();
            string HashedPass = BCrypt.HashPassword(Pass, Salt);
            string query = "INSERT INTO `Users`(ULoginname, ULoginpass, USalt, RoleId, UNaam, UWoonplaats, UAdres, UPostcode, UEmail,UTelefoon) " +
                "VALUES(" +
                "'"+ Name +"'," +
                "'" + HashedPass + "'," +
                "'" + Salt + "'," +
                "'" + Role + "'," +
                "'" + naam + "'," +
                "'" + woonplaats + "'," +
                "'" + adres + "'," +
                "'" + postcode + "'," +
                "'" + email + "'," +
                "'" + telefoon + "'" +
                ");";
            db.ExecuteStringQuery(query);
        }
    }
}
