using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snelle_Wiel.Objects
{
    class Wagen
    {
        int WagenId;
        string Chauffeur;
        string Status;
        string Kenteken;
        string Merk;
        string Type;
        string Bouwjaar;
        string Brandstof;
        string Vermogen;
        string APK;
        string VoertuigHoogte;
        string MassaRijKlaar;
        string ToegestaandeMaximaleMassa;
        string ToegestaandeLaadVermogen;
        string Laadruimte;

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
