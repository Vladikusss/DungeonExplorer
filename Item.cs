using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;


namespace DungeonExplorer
{
    public abstract class Item
    { /* Base class to inherit from */
        public string Name { get; set; }

        public Item(string name)
        {
            Name = name;
        }
        
        public abstract void UseItem(Player player); // Different effects on diffetent items
    }
}