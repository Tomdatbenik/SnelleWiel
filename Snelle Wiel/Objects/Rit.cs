﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snelle_Wiel.Objects
{
    public class Rit
    {
        public double Distance;
        public double Duration;

        public Locatie Locatie;

        public int OrderId;
        public int ophalenafleveren = 0;

        public Rit(double distance, double duration)
        {
            this.Distance = distance;
            this.Duration = duration;
        }
    }
}
