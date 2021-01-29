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
    
    [CreateAssetMenu(fileName = "ItemData", menuName = "GGJ21/Item Data", order = 0)]
    public class ItemData : ScriptableObject
    {
        public Sprite icon;
        public string cantUse;
        public bool addToInventory;
        public string used;
        public string description;
        public ItemID itemId;
        public ItemID[] requiredItems;
        public bool discardRequiredItems;
        public bool oneTimeUse;
    }
}