using System;
using System.Linq;

namespace DungeonExplorer
{
    public class Knife : Weapon
    {
        public Knife(string name, int damage) : base(name, damage)
        {
        }

        public override void UseItem(Player player)
        {
            if (IsEquipped)
            {
                return;
            }

            // If a sword is already equipped, unequip it
            var sword = player.PlayerInventory.Items
                .OfType<Weapon>()
                .FirstOrDefault(w => w.Name == "Sword" && w.IsEquipped);

            if (sword != null)
            {
                Console.WriteLine($"{player.Name} unequips {sword.Name} to use {Name}.");
                sword.IsEquipped = false;
                player.AttackPower -= sword.Damage;
            }

            Console.WriteLine($"{player.Name} equips {Name} and prepares to deal {Damage} damage.");
            player.AttackPower += Damage;
            IsEquipped = true;
        }
    }
}