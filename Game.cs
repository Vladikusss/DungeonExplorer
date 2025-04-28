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
            while (player.IsAlive())
            {
                ShowMenu();
                if (!player.IsAlive())
                {
                    break;
                }
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
                Console.WriteLine("2. View inventory and health");
                Console.WriteLine("3. Exit game");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        GoToNextRoom();
                        break;
                    case "2":
                        ViewInventoryAndHealth();
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
            Console.WriteLine($"\nYou went to the next room. Room Description: {currentRoom.GetDescription()}");
            enemies = GenerateEnemies();
            ShowMenuAfterRoomTransition();
        }
        
        private List<Enemy> GenerateEnemies()
        {
            Random rnd = new Random();
            List<Enemy> newEnemies = new List<Enemy>();

            newEnemies.Add(new Enemy("Goblin", rnd.Next(0, 31), 5));
            newEnemies.Add(new Enemy("Orc", rnd.Next(26, 51), 10));
            newEnemies.Add(new Enemy("Troll", rnd.Next(51, 101), 15));

            return newEnemies;
        }

    }
}
    