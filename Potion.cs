using System;

namespace DungeonExplorer
{
    public class Potion : Item
    {
        public int HealAmount { get; set; }

        public Potion(string name, int healAmount) : base(name)
        {
            HealAmount = healAmount;
        }

        public override void UseItem(Player player)
        {
            player.Health += HealAmount;
            Console.WriteLine($"{player.Name} uses {Name} and gets {HealAmount} hp. Current Health: {player.Health}");
            if (player.Health > player.MaxHealth)
            {
                player.Health = player.MaxHealth;
            }
        }

        public override string ToString()
        {
            return $"{Name} (Heals: {HealAmount})";
        }
    }
}