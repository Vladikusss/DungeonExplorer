using System;

namespace DungeonExplorer
{
    public class AttackPotion : Potion
    {
        public int DamageIncrease { get; set; }

        public AttackPotion(string name, int damageIncrease) : base(name, healAmount: 0, attackAmount: 5)
        {
            DamageIncrease = damageIncrease;
        }

        public override void UseItem(Player player)
        {
            Console.WriteLine($"{player.Name} uses {Name} and increases attack damage by {DamageIncrease}.");
            player.AttackPower += DamageIncrease; // Increase player's attack power
        }
    }
}