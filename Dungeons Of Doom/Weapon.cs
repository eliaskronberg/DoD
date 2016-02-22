using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_Of_Doom
{
    class Weapon:Item
    {
        public Weapon(string name, string type, int weight, bool consumable, int damage):base(name,type,weight,consumable)
        {
            Damage = damage;
        }

        public int Damage { get; set; }

        public override void Pickup()
        {
            
        }
    }
}
