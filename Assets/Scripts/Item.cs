using UnityEngine;

namespace GGJ21
{
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
                    if (Inventory.Items.Find(i => i.itemId == requiredItem)) 
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

            Events.Instance.displayMessage?.Invoke(data.used);
            if(data.oneTimeUse)
                gameObject.SetActive(false);

            if (!data.discardRequiredItems || data.requiredItems == null) return;
            
            foreach (ItemID id in data.requiredItems)
                Inventory.RemoveItem(id);
        }

        protected virtual void CantUse()
        {
            Events.Instance.displayMessage?.Invoke(data.cantUse);
        }
    }
}