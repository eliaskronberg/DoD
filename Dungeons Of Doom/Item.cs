using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_Of_Doom
{
    abstract class Item:GameObject
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
    
        //public virtual void Pickup(Character character)
        //{
        //    Console.WriteLine($"You have found {this.Name}!");
        //    Console.Write($"Do you want to pick up {this.Name}?");
        //    string key = Console.ReadLine();
        //    switch (key)
        //    {
        //        case"Y": break;
        //    }
        //}


    }
}
