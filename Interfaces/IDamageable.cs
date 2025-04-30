namespace DungeonExplorer.Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(int damage); // Function to take damage
        bool IsAlive(); // Function to check if creature is alive
    }
}