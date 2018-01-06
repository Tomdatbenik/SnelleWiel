using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snelle_Wiel.Objects
{
    public class Coordinaat
    {
        public string NB{ get; private set; }
        public string OL { get; private set; }

        public Coordinaat(string nb, string ol)
        {
            this.NB = nb;
            this.OL = ol;
        }
    }
}
