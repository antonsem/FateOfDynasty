using System;
using UnityEngine;

namespace GGJ21
{
    public enum ItemID
    {
        Book,
        Key,
        Glass,
        Door
    }

    [Serializable]
    public class ItemData
    {
        public string cantUse;
        public bool addToInventory;
        public string used;
        public string description;
        public ItemID itemId;
        public ItemID[] requiredItems;
    }

    public class Item : MonoBehaviour, IClickable
    {
        [SerializeField] private ItemData data;

        public string Description => data.description;

        public void Clicked()
        {
            bool canUse = true;
            if (data.requiredItems != null)
            {
                canUse = false;
                int count = data.requiredItems.Length;
                foreach (ItemID requiredItem in data.requiredItems)
                {
                    ItemData item = Inventory.Items.Find(i => i.itemId == requiredItem);
                    if (item != null)
                        count--;
                }

                canUse = count == 0;
            }

            if (canUse)
                Use();
            else
                CantUse();
        }

        protected virtual void Use()
        {
            if (data.addToInventory)
                Inventory.AddItem(data);

            Debug.Log(data.used);
            gameObject.SetActive(false);
        }

        protected virtual void CantUse()
        {
            Debug.Log(data.cantUse);
        }
    }
}