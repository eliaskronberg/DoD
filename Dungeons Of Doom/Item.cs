using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_Of_Doom
{
    class Item:GameObject
    {
        public Item(string name, string type,int weight, bool consumable):base(name)
        {
            Weight = weight;
            Consumable = consumable;
            Type = type;

        }
        public int Weight { get; set; }
        public bool Consumable { get; set; }
        public string Type { get; set; }

    }
}
