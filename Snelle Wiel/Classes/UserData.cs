using Snelle_Wiel.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snelle_Wiel.Classes
{
    class UserData
    {
        Database db;
        public UserData(Database database)
        {
            this.db = database;
        }


        public User GetUser(int id)
        {
            User u = new User();

            string q = "SELECT UserId, RoleId, UNaam, UWoonplaats, UAdres, UPostcode, UEmail, UTelefoon FROM Users WHERE UserId = '" + id.ToString() + "' ";
            DataTable udata = db.ExecuteStringQuery(q);
            if(udata.Rows.Count != 0)
            {
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

            return u;
        }

        public Chauffeur GetChauffeur(int id)
        {
            Chauffeur c = new Chauffeur();

            string q = "SELECT UserId, RoleId, UNaam, UWoonplaats, UAdres, UPostcode, UEmail, UTelefoon FROM Users WHERE UserId = '" + id.ToString() + "' ";
            DataTable udata = db.ExecuteStringQuery(q);
            if (udata.Rows.Count != 0)
            {
                foreach (DataRow dar in udata.Rows)
                {
                    c.Id = int.Parse(dar["UserId"].ToString());
                    c.Rol = int.Parse(dar["RoleId"].ToString());
                    c.Naam = dar["UNaam"].ToString();
                    c.Woonplaats = dar["UWoonplaats"].ToString();
                    c.Adres = dar["UAdres"].ToString();
                    c.Email = dar["UEmail"].ToString();
                    c.Telefoonnr = dar["UTelefoon"].ToString();
                }
            }

            return c;
        }

        public ObservableCollection<Rijbewijs> GetRijbewijzenOnId(int id)
        {
            ObservableCollection<Rijbewijs> rijbewijzen = new ObservableCollection<Rijbewijs>();

            string q = "SELECT * FROM Rijbewijs WHERE RijbewijsId IN (SELECT Rid FROM ChauffeursRijbewijs WHERE Uid = '"+id+"')";
            DataTable Data = db.ExecuteStringQuery(q);

            if (Data.Rows.Count != 0)
            {
                foreach (DataRow dr in Data.Rows)
                {
                    Rijbewijs r = new Rijbewijs();
                    r.Id = int.Parse(dr["RijbewijsId"].ToString());
                    r.Catogorie = dr["RCatogorie"].ToString();
                    r.Omschrijving = dr["ROmschrijving"].ToString();
                    rijbewijzen.Add(r);
                }
            }

            return rijbewijzen;
        }

        public ObservableCollection<Rijbewijs> GetRijbewijzendiehijnietheeftonid(int id)
        {
            ObservableCollection<Rijbewijs> rijbewijzen = new ObservableCollection<Rijbewijs>();

            string q = "SELECT * FROM Rijbewijs WHERE RijbewijsId NOT IN (SELECT distinct RId FROM ChauffeursRijbewijs WHERE Uid = "+id+")";
            DataTable Data = db.ExecuteStringQuery(q);
            if (Data.Rows.Count != 0)
            {
                foreach (DataRow dr in Data.Rows)
                {
                        Rijbewijs r = new Rijbewijs();
                        r.Id = int.Parse(dr["RijbewijsId"].ToString());
                        r.Catogorie = dr["RCatogorie"].ToString();
                        r.Omschrijving = dr["ROmschrijving"].ToString();
                        rijbewijzen.Add(r);
                }
            }

            return rijbewijzen;
        }

        public void AddRijbewijzon(int id,ObservableCollection<Rijbewijs> rijbewijzen, ObservableCollection<Rijbewijs>Nrijbewijs)
        {

            string q = "DELETE FROM `snellewiel`.`ChauffeursRijbewijs` WHERE";
            foreach (Rijbewijs r in Nrijbewijs)
            {
                 q += " (`RId`=" + r.Id + " AND `UId`=" + id + ") OR";
            }
            q = q.Remove(q.Length - 2) + ";";
            db.ExecuteStringQuery(q);

            if(rijbewijzen.Count != 0)
            {
                string qu = "INSERT INTO `snellewiel`.`ChauffeursRijbewijs` (`RId`, `UId`) VALUES ";
                foreach (Rijbewijs r in rijbewijzen)
                {
                    qu += "('" + r.Id + "', '" + id + "'),";
                }
                qu = qu.Remove(qu.Length - 1) + ";";
                db.ExecuteStringQuery(qu);
            }
        }
    }
}
