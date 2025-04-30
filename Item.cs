using System;
using DungeonExplorer.Interfaces;

namespace DungeonExplorer
{
    public abstract class Item : ICollectible
    { /* Base class to inherit from */
        public string Name { get; set; }

        public Item(string name)
            
        {
            Name = name;
        }
        
        public abstract void UseItem(Player player); // Different effects on diffetent items
    }
}