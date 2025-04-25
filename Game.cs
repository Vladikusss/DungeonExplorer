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
            
            // Room initialisation
            currentRoom = Room.rooms[new Random().Next(Room.rooms.Count)];

            // Enemy initialisation
            enemies = new List<Enemy>
            {
                new Enemy("Goblin", 1, 30, 5),
                new Enemy("Skeleton", 30, 50, 15),
                new Enemy("Zombie", 50, 75, 20)
            };

            // Output player and enemy information
            Console.WriteLine($"Player: {player.Name} - Health: {player.Health}");

            foreach (var enemy in enemies)
            {
                Console.WriteLine($"Enemy: {enemy.Name} - Health: {enemy.Health} - Damage: {enemy.Damage}");
            }
            
            // Output room description
            Console.WriteLine($"\nRoom Description: {currentRoom.GetDescription()}");
        }

        public void Start()
        {
            Fight();
            if (player.IsAlive())
            {
                PickUpItems();
                ShowMenu();
            }
        }
    

        private void Fight()
        {
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

                    // Enemies attack
                    if (!enemies.Any(e => e.IsAlive()))
                    {
                        Console.WriteLine("\nYou have defeated all enemies!");
                        break;
                    }

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
            var weapon = new Weapon("Sword", 20);
            var potion = new Potion("Health Potion", 30);

            player.AddItem(weapon);
            player.AddItem(potion);

            Console.WriteLine("\nYou picked up a Sword and a Health Potion.");
        }

        private void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Go to the next room");
                Console.WriteLine("2. Use item from inventory");
                Console.WriteLine("3. Fight straightaway");
                Console.WriteLine("4. Exit game");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        GoToNextRoom();
                        break;
                    case "2":
                        player.PlayerInventory.UseItemFromInventory(player);
                        break;
                    case "3":
                        Fight();
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
        private void GoToNextRoom()
        {
            currentRoom = Room.GetNewRoom(currentRoom);
            Console.WriteLine($"\nYou went to the next room.\nRoom Description: {currentRoom.GetDescription()}");
            enemies = new List<Enemy>
            {
                new Enemy("Goblin", 0, 30, 5),
                new Enemy("Orc", 25, 50, 10),
                new Enemy("Troll", 50, 100, 15)
            };
        }
    }
}
    