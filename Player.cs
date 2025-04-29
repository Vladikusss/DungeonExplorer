using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player : Creature 
    { /* Inherit from the base class */
        public Inventory PlayerInventory {get; set;}
        public int AttackPower { get; set; } = 25; // Default damage

        public Player(string name, int health) : base(name, health) // Inherits properties from parent class    
        {
            PlayerInventory = new Inventory(); // Initialisation
        }

        public override void Attack(Creature target)
        {
            int damage = AttackPower; // Testing figure
            Console.WriteLine($"{Name} attacks {target.Name} dealing {damage} damage!");
            target.TakeDamage(damage);
        }

        public void AddItem(Item item)
        {
            PlayerInventory.PickUpItem(item);
        }
        
    }
}