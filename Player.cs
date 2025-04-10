using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player : Creature 
    { /* Inherit from the base class */
        public List<Item> Inventory {get; set;}

        public Player(string name, int health) : base(name, health) // Inherits properties from parent class    
        {
            Inventory = new List<Item>(); // Initialisation
        }

        public override void Attack(Creature target)
        {
            int damage = 15; // Testing figure
            Console.WriteLine($"{Name} attacks {target.Name} dealing {damage} damage!");
            target.TakeDamage(damage);
        }

        public void AddItem(Item item)
        {
            Inventory.Add(item);
            Console.WriteLine($"{Name} picked up {item.Name}.");
        }
    }
}