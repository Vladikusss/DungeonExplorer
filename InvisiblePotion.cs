using System;

namespace DungeonExplorer
{
    public class InvisiblePotion : Potion
    {
        public InvisiblePotion(string name) : base(name, healAmount: 0, attackAmount: 0)
        {
        }

        public override void UseItem(Player player)
        {
            Console.WriteLine($"{player.Name} uses {Name} and becomes invisible.");
            player.SkipBattle = true;
        }
    }
}