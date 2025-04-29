using System;

namespace DungeonExplorer 
{
    public class Statistics
    {
        public int TotalDamageDealt { get; set; }
        public int TotalItemsFound {get; set;}
        public int TotalEnemiesKilled {get; set;}

        public Statistics()
        {
            TotalDamageDealt = 0;
            TotalItemsFound = 0;
            TotalEnemiesKilled = 0;
        }
        
        public void AddDamage(int amount)
        {
            TotalDamageDealt += amount;
        }

        public void AddItemFound()
        {
            TotalItemsFound++;
        }

        public void AddEnemyKilled()
        {
            TotalEnemiesKilled++;
        }
        
        
        public void DisplayStatistics()
        {
            Console.WriteLine("\n--- Game Statistics ---");
            Console.WriteLine($"Total Damage Dealt: {TotalDamageDealt}");
            Console.WriteLine($"Total Items Found: {TotalItemsFound}");
            Console.WriteLine($"Total Enemies Killed: {TotalEnemiesKilled}");
        }
    }
}