﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_Of_Doom
{
    abstract class GameObject
    {
        public GameObject(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
