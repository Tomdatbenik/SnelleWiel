using Newtonsoft.Json.Linq;
using Snelle_Wiel.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Snelle_Wiel.Classes
{
    class Webservice
    {

        public async Task<Rit> GetTravelTime(Locatie origins, Locatie destinations)
        {
            List<Rit> r = new List<Rit>();

            string BaseUrl = "https://maps.googleapis.com/maps/api/distancematrix/json?units=metric&origins=";
            string Url = BaseUrl + origins.Adres + "," + origins.Postcode + "," + origins.Plaats + "," + origins.Land + "&destinations=" + destinations.Adres + "," + destinations.Postcode + "," + destinations.Plaats + "," + destinations.Land;

            Uri httprequest = new Uri(Url);

            HttpClient client = new HttpClient();
            HttpResponseMessage respons = await client.GetAsync(httprequest);

            respons.EnsureSuccessStatusCode();
            string responsecontent = await respons.Content.ReadAsStringAsync();


            JObject content = JObject.Parse(responsecontent);
            IList<JToken> parentjson = content["rows"].Children().Children().ToList();
            JToken Row = parentjson[0].First;

            foreach (JToken Elements in Row)
            {
                JToken distance = Elements["distance"];
                JToken duration = Elements["duration"];
                string[] Distance = distance["text"].ToString().Split(' ');
                string[] Duration = duration["text"].ToString().Split(' ');

                double m = 0;
                if (Duration.Count() == 4)
                {
                    double f = m + double.Parse(Duration[0]) * 60;
                    m = f + double.Parse(Duration[2]);
                }
                else
                {
                    m = m + double.Parse(Duration[0]);
                }



                string Dis = Distance[0].Replace(".", ",");
                double d = double.Parse(Dis);
                r.Add(new Rit(d,m));
            }
            return r[0];
        }
    }
}
