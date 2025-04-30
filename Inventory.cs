using DungeonExplorer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DungeonExplorer
{
    public class Inventory : IInventory

    {
        private List<Item> items; // List to store items
        public List<Item> Items => items; // Read-only


        public Inventory()
        {
            items = new List<Item>(); // List initialisation
        }

        public void PickUpItem(Item item)
        {
            items.Add(item);
            Console.WriteLine($"You Picked up: {item.Name}");
        }
        public bool InventoryContent()
        { 
            // Print inventory content to the user
            Console.WriteLine("\nInventory content:");

            var weapons = items.OfType<Weapon>().OrderBy(w => w.Name).ToList(); // Using LINQ & lambda for filtering
            var potions = items.OfType<Potion>().OrderBy(p => p.Name).ToList(); // Using LINQ & lambda for filtering

            if (weapons.Count == 0 && potions.Count == 0)
            {
                Console.WriteLine("\nYour inventory is empty.");
                return false;
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
                
                return true;
            }
        }

        public void UseItem(Player player)
        {
            
            bool control = true;
            while (control)
            {

                if (InventoryContent() == false)
                {
                    Console.WriteLine($"You don't have any items in your inventory.");
                    control = false;
                    break;
                }
                else
                {
                    // lambda to find specific item.
                    Console.WriteLine("\nEnter the exact name of an item you wish to use.");
                    string itemName = Console.ReadLine().ToLower();
                    var item = items.FirstOrDefault(x => x.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
                    
                    // Use an item and remove from dictionary
                    if (item == null)
                    {
                        Console.WriteLine("Item not found.");
                        continue; // Ask again
                    }

                    item.UseItem(player);

                    // Remove an item if it's a Potion
                    if (item is Potion)
                    {
                        items.Remove(item);
                        Console.WriteLine($"{item.Name} was used and removed from your inventory.");
                    }
                    else
                    {
                        Console.WriteLine($"{item.Name} is now equipped and stays in your inventory.");
                    }
                    control = false; 
                    break;
                }
            }
        }
        
        public bool UsedWeapon(string weaponName)
        {
            return items.OfType<Weapon>().Any(w => w.Name.Equals(weaponName, StringComparison.OrdinalIgnoreCase));
        }

    }
}