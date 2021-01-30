using System.Collections.Generic;

namespace GGJ21
{
    public static class Inventory
    {
        public static Dictionary<ItemID, ItemData> Items { get; } = new Dictionary<ItemID, ItemData>();

        public static void AddItem(in ItemData item)
        {
            Items.Add(item.itemId, item);
            Events.Instance.pickedUp?.Invoke(item);
        }

        public static void RemoveItem(ItemID itemID)
        {
            Events.Instance.removeItem?.Invoke(Items[itemID]);
            Items.Remove(itemID);
        }
    }
}