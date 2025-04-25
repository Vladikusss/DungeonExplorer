using System;

namespace DungeonExplorer
{
    public class Weapon : Item
    {
        
        public int Damage {get; set;}

        public Weapon(string name, int damage) : base(name)
        {
            Damage = damage;
        }

        public override void UseItem(Player player)
        {
            Console.WriteLine($"{player.Name} equips {Name} and prepares to deal {Damage} damage.");
            player.AttackPower += Damage;
        }
        
        public override string ToString()
        {
            return $"{Name} (Damage: {Damage})";
        }

    }
}