using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

// ABSTRACTION STARTgame() method
namespace DungeonExplorer
{
    internal class Game
    {
        private Player player;
        private Room currentRoom;
        private List<Enemy> enemies;
        private int wins = 0; // When wins = 5 -> player wins
        private int roomsToWin; // Random number of wins needed to win the game - from 4 to 10
        private Statistics stats;


        public Game()
        {
            // Statistics initialisation 
            stats = new Statistics();

            // Player initialisation
            player = new Player("CrazyFrog", 100);
            player.PlayerInventory = new Inventory();

            // Random number of wins needed to win the game - from 4 to 10
            roomsToWin = new Random().Next(4, 11);
            
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
            if (player.SkipBattle)
            {
                Console.WriteLine("\nYou skipped the battle and went straight to the next room.");
                player.SkipBattle = false; // Reset
                NextRoomMenu();
            }
        }

        internal List<Enemy> GenerateEnemies()
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
                        if (player.SkipBattle) // Check if player used invisible potion and skip
                        {
                            Console.WriteLine("\nYou sneak past the room without a sound!");
                            player.SkipBattle = false;
                            GoToNextRoom();
                        }
                        else
                        {
                            Fight();
                        }

                        break;
                    case "3":
                        ViewStats(); // Stats
                        break;
                    case "4":
                        stats.DisplayStatistics(); // Stats
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
                    
                    stats.AddDamage(damage); // Stats
                    Console.WriteLine(
                        $"You hit {enemy.Name} with {damage} damage. {enemy.Name} has {enemy.Health} HP left.");
                    if (!enemy.IsAlive())
                    {
                        stats.AddEnemyKilled();
                    }
                }

                    // All enemies dead
                    if (!enemies.Any(e => e.IsAlive()))
                    {
                        Console.WriteLine("\nYou have defeated all enemies!");
                        wins++; // Increment wins
                        if (wins >= roomsToWin)
                        {
                            Console.WriteLine($"\nCongratulations! You went through {roomsToWin} rooms and won the game!");
                            stats.DisplayStatistics();
                            Environment.Exit(0); // End the game
                        }
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
                        Console.WriteLine("\nYou have been defeated by enemies :(\nYou lost the game!");
                        stats.DisplayStatistics(); // Stats
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
            bool knife = player.PlayerInventory.UsedWeapon("Knife");

            
            // Give one weapon per whole game
            if (!sword && !knife)
            {
                Random rnd = new Random();
                int chance = rnd.Next(100) + 1;
                
                stats.AddItemFound(); // Stats
                if (chance <= 50) // 50% equal change for both weapons
                {
                    var knife2 = new Knife("Knife", 5);
                    player.AddItem(knife2);
                }
                else 
                {
                    var sword2 = new Weapon("Sword", 10);
                    player.AddItem(sword2);
                }
            }
            else
            {
                Console.WriteLine("You already have a weapon. You don’t pick up another one.");
            }
            
            // Potion drop randomised
            Random rnd2 = new Random();
            int chance2 = rnd2.Next(100) + 1;

            if (chance2 <= 43) // 43%
            {
                stats.AddItemFound(); // Stats
                var potion = new Potion("Health Potion", 30, 0);
                player.AddItem(potion);
            }
            else if (chance2 <= 80) // 37%
            {
                stats.AddItemFound(); // Stats
                var potion = new AttackPotion("Attack Potion", 5);
                player.AddItem(potion);
            }
            else // 20%
            {
                stats.AddItemFound(); // Stats
                var potion = new InvisiblePotion("Invisibility Potion");
                player.AddItem(potion);
            }
        }


        private void ViewStats()
        {
            Console.WriteLine($"\nPlayer's health: {player.Health}");
            player.PlayerInventory.InventoryContent();

        }
    }
}
    