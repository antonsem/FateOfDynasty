using System.Collections.Generic;
using UnityEngine;

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
    }
}