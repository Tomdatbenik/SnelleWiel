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

        public bool Login(string lname, string lpass)
        {
            string query = "SELECT UserId , ULoginpass FROM Users WHERE ULoginname = '" + lname + "' ";
            DataTable dt = db.ExecuteStringQuery(query);
            int id;
            string pass = "";

            if (dt.Rows != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    id = int.Parse(dr["UserId"].ToString());
                    pass = dr["ULoginpass"].ToString();
                }

                BCrypt.CheckPassword(lpass, pass);
            }


            return false;
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
