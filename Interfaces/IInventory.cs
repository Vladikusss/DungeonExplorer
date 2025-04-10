namespace DungeonExplorer.Interfaces
{
    public interface IInventory
    {
        void PickUpItem(Item item); // Function to add item to inventory
        void InventoryContent(); // Function to output inventory's content
        void UseItem(string itemName, Player player); // Function to use an item from an inventory
    }
}