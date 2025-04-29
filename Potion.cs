using System;

namespace DungeonExplorer
{
    public class Potion : Item
    {
        public int HealAmount { get; set; }
        public int AttackAmount { get; set; }

        public Potion(string name, int healAmount, int attackAmount) : base(name)
        {
            HealAmount = healAmount;
            AttackAmount = attackAmount;
        }

        public override void UseItem(Player player)
        {
            player.Health += HealAmount;
            if (player.Health > player.MaxHealth)
            {
                player.Health = player.MaxHealth;
                Console.WriteLine($"{player.Name} uses {Name} and gets {HealAmount} hp. Current Health: {player.Health}");
            }
        }

        public override string ToString()
        {
            return $"{Name} (Heals: {HealAmount}, Attacks: {AttackAmount})";
        }
    }
}