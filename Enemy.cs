using System;


namespace DungeonExplorer
{
    public class Enemy : Creature
    { /* Inherit from base class */
        
        public int Damage {get; set;}
        
        private static Random rnd = new Random();

        public Enemy(string name, int minHealth, int maxHealth, int damage) : base(name, RandomHealth(minHealth, maxHealth))
        {
            Damage = damage;
        }

        private static int RandomHealth(int min, int max)
        {
            return rnd.Next(min, max + 1);
        }
        public override void Attack(Creature target)
        {
            int damage = 5; // Test figure
            Console.WriteLine($"{Name} attacks {target.Name} dealing {damage} damage!");
            target.TakeDamage(damage);
        }
    }
}