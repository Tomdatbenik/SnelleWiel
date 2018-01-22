using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snelle_Wiel.Objects
{
    class XMLRootObject
    {
        public List<XMLItem> Items = new List<XMLItem>();

        public void Add(XMLItem Item)
        {
            Items.Add(Item);
        }

        public XMLItem Get(string Name)
        {
            XMLItem item = null;
            foreach(XMLItem i in Items)
            {
                if(i.Name == Name)
                {
                    item = i;
                }
            }

            return item;
        }
    }
}
