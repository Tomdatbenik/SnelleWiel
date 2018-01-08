using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snelle_Wiel.Objects
{
    public class Wagen
    {
        public int WagenId { get; set; }
        public string Chauffeur { get; set; }
        public string Status { get; set; }
        public string Kenteken { get; set; }
        public string Merk { get; set; }
        public string Type { get; set; }
        public string Bouwjaar { get; set; }
        public string Brandstof { get; set; }
        public string Vermogen { get; set; }
        public string APK { get; set; }
        public string VoertuigHoogte { get; set; }
        public string MassaRijKlaar { get; set; }
        public string ToegestaandeMaximaleMassa { get; set; }
        public string ToegestaandeLaadVermogen { get; set; }
        public string Laadruimte { get; set; }

        public Wagen() { }

        public Wagen(int wagenid, string chauffeur, string status, string kenteken, string merk, string type, string bouwjaar,string brandstof, string vermogen, string apk, string voertuighoogte,string massarijklaar, string toegestaandemaximalemassa, string toegestaandelaadvermogen, string laadruimteinhoud)
        {
            this.WagenId = wagenid;
            this.Chauffeur = chauffeur;
            this.Status = status;
            this.Kenteken = kenteken;
            this.Merk = merk;
            this.Type = type;
            this.Bouwjaar = bouwjaar;
            this.Brandstof = brandstof;
            this.Vermogen = vermogen;
            this.APK = apk;
            this.VoertuigHoogte = voertuighoogte;
            this.MassaRijKlaar = massarijklaar;
            this.ToegestaandeMaximaleMassa = toegestaandemaximalemassa;
            this.ToegestaandeLaadVermogen = toegestaandelaadvermogen;
            this.Laadruimte = laadruimteinhoud;
        }
    }
}
