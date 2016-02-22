using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_Of_Doom
{
    class Armor:Item
    {
        public int Value { get; set; }
        public Armor(string name, string type,int weight,bool consumable,int armorPoints):base(name,type,weight,consumable)
        {
            Value = armorPoints;

        }
    }
}
