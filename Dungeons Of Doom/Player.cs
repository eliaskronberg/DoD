using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_Of_Doom
{
    class Player:Character
    {
        public Player(string name,int health,int attackDamage,int armor, string status, int capacity):base(name,health,attackDamage,armor,status)
        {
            //Name = name;
            //Health = health;
            //AttackDamage = attackDamage;
            BackPack = new List<Item>();
            Capacity = capacity;
            Level = 1;
            Experience = 0;
        }
       
        public int X { get; set; }
        public int Y { get; set; }
        public List<Item> BackPack { get; set; }
        public int Capacity { get; set; }
        public int Experience { get; set; }
        public int Level { get; set; }


    }
}
