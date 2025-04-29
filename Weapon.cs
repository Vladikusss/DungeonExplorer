using System;

namespace DungeonExplorer
{
    public class Weapon : Item
    {
        
        public int Damage {get; set;}
        public bool IsEquipped { get; set; }

        public Weapon(string name, int damage) : base(name)
        {
            Damage = damage;
            IsEquipped = false;
        }

        public override void UseItem(Player player)
        {
            if (IsEquipped)
            {
               // DEBUG CONSOLE MESSAGE ABOUT SWORD EQUIPMENT 
                return;
            }
            Console.WriteLine($"{player.Name} equips {Name} and prepares to deal {Damage} damage.");
            player.AttackPower += Damage;
            IsEquipped = true;
        }
        
        public override string ToString()
        {
            string status = IsEquipped ? "Equipped" : "Unequipped";
            return $"{Name} (Damage: {Damage}, {status})";
        }

    }
}