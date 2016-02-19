using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_Of_Doom
{
    abstract class Character:GameObject
    {
        public Character(string name, int health, int attackDamage, int armor, string status):base(name)
        {
            
            Health = health;
            AttackDamage = attackDamage;
            Armor = armor;
            Status = status;
        }
        public string Status { get; set; }

        public int Health { get; set; }
        public int AttackDamage { get; set; }
        public int Armor { get; set; }
        internal virtual void Fight(Character oponent)
        {
            while(Health>0 && oponent.Health > 0)
            {
                oponent.Health -= AttackDamage;
                Health -= oponent.AttackDamage;
            }
            if (Health < 0)
            {
                Status = "Dead";
            }
                

        }

    }
}
