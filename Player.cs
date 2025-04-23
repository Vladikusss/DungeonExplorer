using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player : Creature 
    { /* Inherit from the base class */
        public Inventory PlayerInventory {get; set;}

        public Player(string name, int health) : base(name, health) // Inherits properties from parent class    
        {
            PlayerInventory = new Inventory(); // Initialisation
        }

        public override void Attack(Creature target)
        {
            int damage = 25; // Testing figure
            Console.WriteLine($"{Name} attacks {target.Name} dealing {damage} damage!");
            target.TakeDamage(damage);
        }

        public void AddItem(Item item)
        {
            PlayerInventory.PickUpItem(item);
        }
        
        public void InventoryContent()
        {
            PlayerInventory.InventoryContent();
        }
    }
}