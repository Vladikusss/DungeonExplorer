using System;

namespace DungeonExplorer
{
    public class Testing
    {
        public void PlayerTakesDamage()
        {
            var player = new Player("Test", 100);
            
            int initialHealth = player.Health;
            player.TakeDamage(25);

            if (player.Health == initialHealth - 25)
            {
                Console.WriteLine("Test passed: Health decreased after taking damage.");
            }
            else
            {
                Console.WriteLine("Test failed: Health decreased incorrectly after taking damage.");
            }
        }

        public void HealingPotion()
        {
            var player = new Player("Testing PLayer", 60);
            
            var potion = new Potion("Healing Potion Test", 15, 0);
            
            int initialHealth = player.Health;
            potion.UseItem(player);

            if (player.Health == initialHealth + 15)
            {
                Console.WriteLine("Test passed: Health increased after using Healing Potion.");
            }
            else
            {
                Console.WriteLine("Test failed: Health Potion increased player's health incorrectly.");
            }
        }
    }
}