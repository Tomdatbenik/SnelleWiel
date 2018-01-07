using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snelle_Wiel.Objects
{
    class Planning
    {
        //foreacht rit in ritten kijken welke het snelste is van rit start en rit eind maar rit start moet eerst komen
        List<Rit> Ritten = new List<Rit>();
        //Ritvolgorde worden de ritten gestopt op volgorde van start en van eind
        ObservableCollection<Order> RitVolgorde = new ObservableCollection<Order>();

        //Controlleer op wageninhoud in liters en houd dan altijd wat over zodat er makkelijk dingen ingeladen kunnen worden
        //Chauffeurs hebben 8 uur werktijd per dag
        //rit moet efficient mogelijk zijn
        //
        public void BerekenRitten()
        {

        }

    }
}
