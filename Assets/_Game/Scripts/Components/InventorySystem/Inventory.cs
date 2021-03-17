using System.Collections.Generic;
using UnityEngine.Events;

namespace Components.InventorySystem
{
    public class Inventory : IItemTransfer<IItem>
    {
        readonly List<IItem> items;
        public OnInventoryChangedEvent OnInventoryChanged;

        public Inventory(int capacity)
        {
            items = new List<IItem>();
            items.Capacity = capacity;
            OnInventoryChanged = new OnInventoryChangedEvent();
        }

        public List<IItem> Items => new List<IItem>(items);

        public bool Receive(IItem item)
        {
            return Add(item);
        }

        public bool Transfer(IItemTransfer<IItem> storage, IItem item)
        {
            if (!items.Contains(item)) return false;
            if (!storage.Receive(item)) return false;
            items.Remove(item);
            OnInventoryChanged.Invoke(Items);
            return true;
        }

        bool Add(IItem item)
        {
            if (items.Capacity <= items.Count) return false;
            items.Add(item);
            OnInventoryChanged.Invoke(items);
            return true;
        }

        bool Remove(IItem item)
        {
            if (!items.Remove(item) && !item.Disposable) return false;
            OnInventoryChanged.Invoke(Items);
            return true;
        }

        public bool ChangeCapacity(int capacity)
        {
            if (items.Count > capacity) return false;
            items.Capacity = capacity;
            OnInventoryChanged.Invoke(Items);
            return true;
        }
    }

    public class OnInventoryChangedEvent : UnityEvent<List<IItem>> { }
}