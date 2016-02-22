using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_Of_Doom
{
    class Goblin : Monster
    {
        public Goblin(string name, int health, int attackDamage, int armor, string status) : base(name, health, attackDamage, armor, status)
        {

        }

        internal override void Fight(Character oponent)
        {
            if (oponent.Health > 0.5 * Health)
                Console.WriteLine("Goblin died of fear");
            else
                oponent.Health -= AttackDamage;
        }

    }
}
