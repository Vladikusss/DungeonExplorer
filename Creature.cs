using System;

namespace DungeonExplorer
{
    public abstract class Creature
    /* Base clase for player & enemies to implement shared logic */
    {
        // Shared attributes:
        public string Name {get; set;}
        public int Health {get; set;}
        public int MaxHealth {get; set;}

        public Creature(string name, int health)
        { // Class constructor
            Name = name;
            Health = health;
            MaxHealth = health;
        }
        
        public abstract void Attack(Creature target); // Implemented differently by each subclass

        public void TakeDamage(int damage)
        { // Function to decrease health after taking damage
            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
                Console.WriteLine($"{Name} takes {damage} damage. Remaining Health: {Health}");
            }
        }

        public bool IsAlive()
        { // Function to check if creature is alive
            return Health > 0;
        }
        
        
    }
}