using UnityEngine;
using UnityEngine.Serialization;

namespace GGJ21
{
    public enum ItemID
    {
        None,
        Ashes,
        FireSource,
        Matches,
        Dagger,
        SilverDagger,
        Leech,
        Blood_3,
        Blood_4,
        Door,
        Log,
        FlammableLog,
        Silver,
        Blood_2,
        Blood_1
    }
    
    [CreateAssetMenu(fileName = "ItemData", menuName = "GGJ21/Item Data", order = 0)]
    public class ItemData : ScriptableObject
    {
        public AudioClip sound;
        public bool stopToggle = false;
        public Sprite icon;
        public string cantUse;
        public bool addToInventory;
        [FormerlySerializedAs("used")] [TextArea(2, 10)] public string use;
        [TextArea(2, 10)] public string used;
        [TextArea(2, 10)] public string description;
        public ItemID itemId;
        public ItemID[] requiredItems;
        public bool discardRequiredItems;
        public bool oneTimeUse;
        public bool disableAfterUse;
        public ItemData addItemOnUse;
        public int requiredItemCount = -1;
        public bool sendLog = false;
    }
}