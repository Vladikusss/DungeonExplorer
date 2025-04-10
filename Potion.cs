using System;

namespace DungeonExplorer
{
    public class Potion : Item
    {
        public int HealAmount {get; set;}

        public Potion(string name, int healAmount) : base(name)
        {
            HealAmount = healAmount;
        }

        public override void UseItem(Player player)
        {
            Console.WriteLine($"{player.Name} uses {Name} and gets {HealAmount} hp. Current Health: {player.Health}");
        }
    }
}