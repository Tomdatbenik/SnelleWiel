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
        int orderperschauf = 0;
        Webservice wb = new Webservice();
        int startvandedag = 510;
        //Ritvolgorde worden de ritten gestopt op volgorde van start en van eind



        //Controlleer op wageninhoud in liters en houd dan altijd wat over zodat er makkelijk dingen ingeladen kunnen worden
        //onder 480 minuten
        //rit moet efficient mogelijk zijn voor nu weg\
        //half 9 = 510 minuten

        Database db;
        public Planning(Database database)
        {
            this.db = database;
            calculateordersperchauf();
        }

        public void calculateordersperchauf()
        {
            List<Order> Orders = Getorders();
            List<User> Chaufs = GetChaufs();

            int i = 0;
            foreach (User c in Chaufs)
            {
                i++;
            }
            orderperschauf = Orders.Count() / i;
            Console.WriteLine("Order per chauf " + orderperschauf);
        }

        public async Task<ObservableCollection<PlanningItem>> GetPlanningItems(int chauffeurId, string datum)
        {
            Console.WriteLine("Order per chauf " + orderperschauf);
            ObservableCollection<PlanningItem> PlanningItems = new ObservableCollection<PlanningItem>();
            List<Order> Orders = Getorders();
            List<User> Chaufs = GetChaufs();
            DataTable data = null; 
            //Check of planning bestaad if(query database ding)
                string q = "SELECT PlanningId FROM Planning WHERE `Date` = '" + datum + "';";
                data = db.ExecuteStringQuery(q);

                if(data.Rows.Count == 0)
                {
                    data = null;
                }

            if (data == null)
            {
                int i = 0;
                foreach (User c in Chaufs)
                {
                    i++;
                }

                List<Order> SelectedOrders = SelectordersAndSet();
                double k = startvandedag;

                Order Lastorder;
                int x = 0;
                foreach (Order o in SelectedOrders)
                {
                    if (x == 0)
                    {
                        Rit r1 = await wb.GetTravelTime(new Locatie("Sint-Oedenrode", "Liempdseweg43", "5492 SM", "Nederland"), o.Start);
                        k = k += r1.Duration;
                        Lastorder = o;
                        x++;
                    }
                    else
                    {
                        Rit r1 = await wb.GetTravelTime(o.Start, o.Start);
                        k = k += r1.Duration;
                    }

                    PlanningItem pi = new PlanningItem();
                    pi.OrderId = o.Id;
                    pi.OrderBeschrijving = o.Omschrijving;
                    pi.Tijd = MinutenNaarTijd(int.Parse(k.ToString()));
                    pi.ChauffeurId = chauffeurId;
                    pi.OAText = "Ophalen";
                    pi.locatie = o.Start;

                    PlanningItems.Add(pi);

                    Rit r2 = await wb.GetTravelTime(o.Start, o.Einde);
                    k = k += r2.Duration;
                    PlanningItem pi2 = new PlanningItem();
                    pi2.OrderId = o.Id;
                    pi2.OrderBeschrijving = o.Omschrijving;
                    pi2.Tijd = MinutenNaarTijd(int.Parse(k.ToString()));
                    pi2.ChauffeurId = chauffeurId;
                    pi2.OAText = "Afleveren";
                    pi2.locatie = o.Start;

                    PlanningItems.Add(pi2);
                    Lastorder = o;
                }
                List<PlanningItem> pls = new List<PlanningItem>();

                foreach (PlanningItem pl in PlanningItems)
                {
                    pls.Add(pl);
                }
            }
            else
            {
                if(datum != "")
                {
                string query = "SELECT * FROM `PlanningItems` WHERE `ChauffeurId` = '" + chauffeurId + " ' ";
                DataTable dt = db.ExecuteStringQuery(query);
                foreach (DataRow dr in dt.Rows)
                {
                    PlanningItem pi = new PlanningItem();
                    pi.OrderId = int.Parse(dr["OrderId"].ToString());
                    pi.OrderBeschrijving = dr["OrderBeschrijving"].ToString();
                    pi.Tijd = dr["Tijd"].ToString();
                    pi.ChauffeurId = int.Parse(dr["ChauffeurId"].ToString());
                    pi.OAText = dr["Tijd"].ToString();
                    Locatie l = new Locatie(dr["Plaats"].ToString(), dr["Adres"].ToString(), dr["Postcode"].ToString(), dr["Land"].ToString());
                    pi.locatie = l;
                    PlanningItems.Add(pi);
                }
                }
            }


            return PlanningItems;
        }

        public void saveplanning(List<PlanningItem> PlanningItems,string datum)
        {
            string q = "INSERT INTO `Planning` (`Date`) VALUES ('"+ datum +"');";
            db.ExecuteStringQuery(q);

            string qu = "SELECT PlanningId FROM Planning WHERE `Date` = '" + datum + "';";
            DataTable dt =  db.ExecuteStringQuery(qu);

            DataRow dr = dt.Rows[0];
            string date = dr["PlanningId"].ToString();


            string query = "INSERT INTO `snellewiel`.`PlanningItems` (`PlanningId`, `OrderBeschrijving`, `OAText`, `Tijd`, `OrderId`, `ChauffeurId`, `Plaats`, `Adres`, `Postcode`, `Land`) VALUES";
            foreach(PlanningItem pi in PlanningItems)
            {
                query += "('"+date+"', '"+pi.OrderBeschrijving+"', '"+pi.OAText+"', '"+pi.Tijd+"', '" +pi.OrderId+"', '" +pi.ChauffeurId+"', '"+pi.locatie.Plaats +"', '" + pi.locatie.Adres + "', '" + pi.locatie.Postcode+ "', '" + pi.locatie.Land+ "'),";
            }
            query = query.Remove(query.Length - 1) + ";";
            db.ExecuteStringQuery(query);
        }





        public List<Order> SelectordersAndSet()
        {
            List<Order> Orders = new List<Order>();
            string query = "SELECT * FROM `Order` WHERE Gebruik = '0' ORDER BY `OphaalpuntPostcode`  LIMIT "+ orderperschauf + "; ";
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

            foreach(Order o in Orders)
            {
                string q = "UPDATE `snellewiel`.`Order` SET `Gebruik`='1' WHERE  `OrderId`="+o.Id+";";
                DataTable dt = db.ExecuteStringQuery(q);
            }

            return Orders;
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

        public string MinutenNaarTijd(int minuut)
        {
            double M = double.Parse(minuut.ToString());
            double Uur = M / 60;
            Console.WriteLine(Uur);

            string[] DeelVanUur = Uur.ToString().Split(',');
            double m = 0;
            if (DeelVanUur.Count() == 2)
            {
               m = double.Parse(DeelVanUur[1]);
            }
            double Minuut = 00;
            if(m != 0)
            {
                Minuut = m / 100 * 60 * 10;
            }

            char[] fixminuten = Minuut.ToString().ToArray();

            int minuten = 0;
            Console.WriteLine(fixminuten.Count());
            Console.WriteLine(fixminuten.Length);
            if (fixminuten.Count() > 1)
            {
                minuten = int.Parse(fixminuten[0].ToString() + fixminuten[1].ToString());
                return DeelVanUur[0] + ":" + minuten;
            }
            else
            {
                if(Minuut == 0)
                {
                    return DeelVanUur[0] + ":" + Minuut + "0";
                }
                
                return DeelVanUur[0] + ":" + Minuut;
            }

        }
       
    }
}
