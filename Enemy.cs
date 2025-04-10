using System;


namespace DungeonExplorer
{
    public class Enemy : Creature
    { /* Inherit from base class */
        public Enemy(string name, int health) : base(name, health) {}

        public override void Attack(Creature target)
        {
            int damage = 5; // Test figure
            Console.WriteLine($"{Name} attacks {target.Name} dealing {damage} damage!");
            target.TakeDamage(damage);
        }
    }
}