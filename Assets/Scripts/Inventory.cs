using System.Collections.Generic;
using NUnit.Framework;

namespace GGJ21
{
    public static class Inventory
    {
        public static List<ItemData> Items { get; } = new List<ItemData>();

        public static void AddItem(in ItemData item)
        {
            Items.Add(item);
            Events.Instance.pickedUp?.Invoke(item);
        }

        private static void RemoveItem(in ItemData item)
        {
            Items.Remove(item);
            Events.Instance.removeItem?.Invoke(item);
        }

        public static void RemoveItem(ItemID itemID)
        {
            RemoveItem(Items.Find(i => i.itemId == itemID));
        }
    }
}