using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_Of_Doom
{
    class Item:GameObject
    {
        public Item(string name, int weight,int damage, int armor, int health, bool consumable):base(name)
        {
            Weight = weight;
            Consumable = consumable;

        }
        public int Weight { get; set; }
        public bool Consumable { get; set; }

    }
}
