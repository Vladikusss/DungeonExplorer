using DungeonExplorer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DungeonExplorer
{
    public class Inventory : IInventory

    {
        private List<Item> items; // List to store items

        public Inventory()
        {
            items = new List<Item>(); // List initialisation
        }

        public void PickUpItem(Item item)
        {
            items.Add(item);
            Console.WriteLine($"You Picked up: {item.Name}");
        }

        public void InventoryContent()
        { 
            // Print inventory content to the user
            Console.WriteLine("\nInventory content:");

            var weapons = items.OfType<Weapon>().OrderBy(w => w.Name).ToList(); // Using LINQ & lambda for filtering
            var potions = items.OfType<Potion>().OrderBy(p => p.Name).ToList(); // Using LINQ & lambda for filtering

            if (weapons.Count == 0 && potions.Count == 0)
            {
                Console.WriteLine("\nYour inventory is empty.");
            }
            else
            {
                Console.WriteLine("\nWeapons:");
                foreach (var item in weapons)
                {
                    Console.WriteLine($"|---> {item}.");
                }
                
                Console.WriteLine("\nPotions:");
                foreach (var item in potions)
                {
                    Console.WriteLine($"|---> {item}.");
                }
            }
        }

        public void UseItem(string itemName, Player player)
        {
            // lambda to find specific item.
            var item = items.FirstOrDefault(x => x.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if (item == null)
            {
                Console.WriteLine($"You don't have {itemName} in your inventory.");
            }
            else
            {
                // Use an item and remove from dictionary
                item.UseItem(player);
                items.Remove(item);
                Console.WriteLine($"{item.Name} was used and removed from your inventory.");
            }
        }
        
        public void UseItemFromInventory(Player player)
        {
            Console.WriteLine("\nInventory:");
            InventoryContent();

            Console.WriteLine("\nChoose an item to use:");
            foreach (var item in items)
            {
                Console.WriteLine($"{items.IndexOf(item) + 1}. {item}");
            }

            string choice = Console.ReadLine();
            if (int.TryParse(choice, out int index) && index > 0 && index <= items.Count)
            {
                UseItem(items[index - 1].Name, player);
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }
}