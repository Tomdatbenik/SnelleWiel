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

        public async Task<Rit> GetTravelTime(Coordinaat origins, Coordinaat destinations)
        {
            List<Rit> r = new List<Rit>();

            string BaseUrl = "https://maps.googleapis.com/maps/api/distancematrix/json?units=metric&origins=";
            string Url = BaseUrl + origins.NB + "," + origins.OL + "&destinations=" + destinations.NB + "%2C" + destinations.OL;

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
                r.Add(new Rit(distance["text"].ToString(), duration["text"].ToString()));
            }
            return r[0];
        }
    }
}
