using Snelle_Wiel.Objects;
using System;
using System.Collections.Generic;
using System.Data;
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

        public User Login(string lname, string lpass)
        {
            User u = new User(0, null, null, null, null, null, null);
            string query = "SELECT UserId, ULoginpass FROM Users WHERE ULoginname = '" + lname + "' ";
            DataTable dt = db.ExecuteStringQuery(query);

            if (dt.Rows.Count != 0)
            {
                DataRow dr = dt.Rows[0];
                int id = int.Parse(dr["UserId"].ToString());
                string pass = dr["ULoginpass"].ToString();

                if (BCrypt.CheckPassword(lpass, pass))
                {
                    string q = "SELECT UserId, RoleId, UNaam, UWoonplaats, UAdres, UPostcode, UEmail, UTelefoon FROM Users WHERE UserId = '" + id.ToString() + "' ";
                    DataTable udata = db.ExecuteStringQuery(q);
                    foreach (DataRow dar in udata.Rows)
                    {
                        u.Id = int.Parse(dar["UserId"].ToString());
                        u.Rol = int.Parse(dar["RoleId"].ToString());
                        u.Naam = dar["UNaam"].ToString();
                        u.Woonplaats = dar["UWoonplaats"].ToString();
                        u.Adres = dar["UAdres"].ToString();
                        u.Email = dar["UEmail"].ToString();
                        u.Telefoonnr = dar["UTelefoon"].ToString();
                    }   
                }
            }   


            return u;
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
