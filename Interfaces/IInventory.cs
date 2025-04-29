namespace DungeonExplorer.Interfaces
{
    public interface IInventory
    {
        void PickUpItem(Item item); // Function to add item to inventory
        bool InventoryContent(); // Function to output inventory's content
        void UseItem(Player player); // Function to use an item from an inventory
    }
}