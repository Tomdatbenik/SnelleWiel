using Snelle_Wiel.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snelle_Wiel.Objects
{
    class Planning
    {
        //foreacht rit in ritten kijken welke het snelste is van rit start en rit eind maar rit start moet eerst komen
        List<Rit> Ritten = new List<Rit>();
        List<PlanningItem> planningItems = new List<PlanningItem>();
        List<Order> Orders;
        User Chauf;

        //Ritvolgorde worden de ritten gestopt op volgorde van start en van eind



        //Controlleer op wageninhoud in liters en houd dan altijd wat over zodat er makkelijk dingen ingeladen kunnen worden
        //onder 480 minuten
        //rit moet efficient mogelijk zijn voor nu weg\
        //half 9 = 510 minuten

        Database db;
        public Planning(Database database)
        {
            this.db = database;
            List<Order> Orders = Getorders();
            List<User> Chaufs = GetChaufs();

            int i = 0;
            foreach(User c in Chaufs)
            {
                i++;
            }

            int o = Orders.Count()/i;
        }


        public async Task<List<Rit>> Berekenachtuur(List<Order> Orders)
        {
            Webservice ws = new Webservice();
            List<Rit> Ritten = new List<Rit>();
            int tijd = 0;
            foreach (Order o in Orders)
            {
                Rit rit = await ws.GetTravelTime(o.Start, o.Einde);
                tijd += int.Parse(rit.Duration.ToString());
                Ritten.Add(rit);
            }
            return Ritten;
        }

        public List<User> GetChaufs()
        {
            List<User> Chaufs = new List<User>();
            DataTable dt = db.ExecuteStringQuery("SELECT UserId, RoleId, UNaam, UWoonplaats, UAdres, UPostcode, UEmail, UTelefoon FROM Users WHERE RoleId = 2");
            foreach (DataRow dr in dt.Rows)
            {
                string id = dr["UserId"].ToString();
                string naam = dr["UNaam"].ToString();
                string woonplaats = dr["UWoonplaats"].ToString();
                string adres = dr["UAdres"].ToString();
                string postcode = dr["UPostcode"].ToString();
                string email = dr["UEmail"].ToString();
                string telefoon = dr["UTelefoon"].ToString();

                User u = new User(int.Parse(id), naam, woonplaats, adres, postcode, email, telefoon);
                Chaufs.Add(u);
            }
            return Chaufs;
        }

        public List<Order> Getorders()
        {
            List<Order> Orders = new List<Order>();
            string query = "SELECT * FROM `Order` WHERE Gebruik = '0';";
            DataTable data = db.ExecuteStringQuery(query);
            foreach (DataRow dr in data.Rows)
            {
                string OphaalpuntPlaats = dr["OphaalpuntPlaats"].ToString();
                string OphaalpuntAdres = dr["OphaalpuntAdres"].ToString();
                string OphaalpuntPostcode = dr["OphaalpuntPostcode"].ToString();
                string OphaalpuntLand = dr["OphaalpuntLand"].ToString();

                string EindbestemmingPlaats = dr["EindbestemmingPlaats"].ToString();
                string EindbestemmingAdres = dr["EindbestemmingAdres"].ToString();
                string EindbestemmingPostcode = dr["EindbestemmingPostcode"].ToString();
                string EindbestemmingLand = dr["EindbestemmingLand"].ToString();



                Locatie s = new Locatie(OphaalpuntPlaats, OphaalpuntAdres, OphaalpuntPostcode, OphaalpuntLand);
                Locatie e = new Locatie(EindbestemmingPlaats, EindbestemmingAdres, EindbestemmingPostcode, EindbestemmingLand);



                Order o = new Order(int.Parse(dr["OrderId"].ToString()), dr["Omschrijving"].ToString(), s, e);
                Orders.Add(o);
            }
            return Orders;
        }









        public int uurnaarminuut(int uur)
        {
            return uur * 60;
        }

        public double minuutnaaruur(double minuut)
        {
            double Tijd = minuut / 60;
            string Uur = Tijd.ToString().Split('.')[0];
            string Minuten = Tijd.ToString().Split('.')[1];
            double Hoelaat = 0;
            return minuut / 60;
        }
       
    }
}
