using ExtraTools;
using UnityEngine;

namespace GGJ21
{
    public class Item : MonoBehaviour, IClickable
    {
        [SerializeField] protected ItemData data;

        private bool _used = false;
        public string Description => data.description;

        public virtual void Clicked()
        {
            if (_used && data.oneTimeUse)
            {
                if(data.used.IsValid())
                    Events.Instance.displayMessage?.Invoke(data.used);
                
                return;
            }
            
            bool canUse = true;
            if (data.requiredItems != null)
            {
                canUse = false;
                int count = data.requiredItems.Length;
                foreach (ItemID requiredItem in data.requiredItems)
                {
                    if (Inventory.Items.ContainsKey(requiredItem)) 
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
            _used = true;
            if (data.addToInventory)
                Inventory.AddItem(data);

            Events.Instance.displayMessage?.Invoke(data.use);
            if(data.disableAfterUse)
                gameObject.SetActive(false);

            if (!data.discardRequiredItems || data.requiredItems == null) return;
            
            foreach (ItemID id in data.requiredItems)
                Inventory.RemoveItem(id);
            
            if(data.addItemOnUse)
                Inventory.AddItem(data.addItemOnUse);
        }

        protected virtual void CantUse()
        {
            Events.Instance.displayMessage?.Invoke(data.cantUse);
        }
    }
}