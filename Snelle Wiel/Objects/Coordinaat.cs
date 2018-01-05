using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snelle_Wiel.Objects
{
    public class Coordinaat
    {
        public double NB{ get; private set; }
        public double OL { get; private set; }

        public Coordinaat(double nb, double ol)
        {
            this.NB = nb;
            this.OL = ol;
        }
    }
}
