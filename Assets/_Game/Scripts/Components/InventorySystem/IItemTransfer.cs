namespace Components.InventorySystem
{
    public interface IItemTransfer<T>
    {
        bool Transfer(IItemTransfer<T> storage, T item);
        bool Receive(T item);
    }
}