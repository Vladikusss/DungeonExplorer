using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;


namespace DungeonExplorer
{
    internal class Game
    {
        private Player player;
        private Room currentRoom;
        private List<Enemy> enemies;

        public Game()
        {
            // Player initialisation
            player = new Player("CrazyFrog", 100);
            player.PlayerInventory = new Inventory();

            
            // Room initialisation
            currentRoom = Room.rooms[new Random().Next(Room.rooms.Count)];

            // Output player information
            Console.WriteLine($"Player: {player.Name} - Health: {player.Health}");
            
            // Output room description
            Console.WriteLine($"\nRoom Description: {currentRoom.GetDescription()}");
        }

        public void Start()
        {
            while (player.IsAlive())
            {
                ShowMenu();
                if (!player.IsAlive())
                {
                    break;
                }
            }
        }
        
        private void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Go to the next room");
                Console.WriteLine("2. View inventory and health");
                Console.WriteLine("3. Exit game");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        GoToNextRoom();
                        break;
                    case "2":
                        ViewStats();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    
        
        private void GoToNextRoom()
        {
            currentRoom = Room.GetNewRoom(currentRoom);
            Console.WriteLine($"\nYou went to the next room. \nRoom Description: {currentRoom.GetDescription()}");
            NextRoomMenu();
        }
        
        
        private List<Enemy> GenerateEnemies()
        {
            Random rnd = new Random();
            List<Enemy> newEnemies = new List<Enemy>();

            newEnemies.Add(new Enemy("Goblin", 0, 31, 5));
            newEnemies.Add(new Enemy("Skeleton", 26, 51, 10));
            newEnemies.Add(new Enemy("Zombie", 51, 101, 15));

            return newEnemies;
        }

        
        private void NextRoomMenu()
        {
            while (true)
            {
                Console.WriteLine("\nYour options are:");
                Console.WriteLine("1. Use item from inventory");
                Console.WriteLine("2. Fight straightaway");
                Console.WriteLine("3. View inventory and health");
                Console.WriteLine("4. Exit game");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        player.PlayerInventory.UseItem(player);
                        break;
                    case "2":
                        Fight();
                        break;
                    case "3":
                        ViewStats();
                        break;
                    case "4":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        private void Fight()
        {
            enemies = GenerateEnemies();
            while (player.IsAlive() && enemies.Any(e => e.IsAlive()))
            {
                Console.WriteLine("\nPlayer's turn:");
                foreach (var enemy in enemies.Where(e => e.IsAlive()))
                {
                    // Player attacks
                    int damage = CalculatePlayerDamage();
                    enemy.TakeDamage(damage);
                    Console.WriteLine(
                        $"You hit {enemy.Name} with {damage} damage. {enemy.Name} has {enemy.Health} HP left.");
                }

                    // All enemies dead
                    if (!enemies.Any(e => e.IsAlive()))
                    {
                        Console.WriteLine("\nYou have defeated all enemies!");
                        break;
                    }
                    
                    // Enemies attack
                    Console.WriteLine("\nEnemies' turn:");
                    foreach (var enemy2 in enemies.Where(e => e.IsAlive()))
                    {
                        player.TakeDamage(enemy2.Damage);
                        Console.WriteLine(
                            $"{enemy2.Name} attacks you dealing {enemy2.Damage} damage. You have {player.Health} HP left.");
                    }

                    // Player is dead
                    if (!player.IsAlive())
                    {
                        Console.WriteLine("\nYou have been defeated by enemies :(");
                        Environment.Exit(0);
                    }
            }
            
            // Progress with the game
            PickUpItems();
            NextRoomMenu();
        }

        private int CalculatePlayerDamage()
        {
            Random rnd = new Random();
            int baseDamage = player.AttackPower;
            int damage = baseDamage;

            if (rnd.Next(100) < 37) // 37% chance for critical hit
            {
                damage = 50;
                Console.WriteLine("\nCritical hit!");
            }

            return damage;
        }

        private void PickUpItems()
        {
            bool sword = player.PlayerInventory.UsedWeapon("Sword");
            
            if (player.PlayerInventory.UsedWeapon("Sword"))
            {
                Console.WriteLine("You already have a sword. You don't pick up another one.");
            }
            else
            {
                var weapon = new Weapon("Sword", 10);
                player.AddItem(weapon);
            }
            
            var potion = new Potion("Health Potion", 30);

            
            player.AddItem(potion);
        }


        private void ViewStats()
        {
            Console.WriteLine($"\nPlayer's health: {player.Health}");
            player.PlayerInventory.InventoryContent();
        }
    }
}
    