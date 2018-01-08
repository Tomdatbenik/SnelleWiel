using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snelle_Wiel.Objects
{
    class PlanningItem
    {
        public string OrderBeschrijving { get; set; }
        public string OAText { get; set; }
        public string Tijd { get; set; }
        public int OrderId { get; set; }
        public int ChauffeurId;
}
}
