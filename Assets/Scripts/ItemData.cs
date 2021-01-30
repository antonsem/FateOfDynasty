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
        CleanDagger,
        Leech,
        CoagulatedBlood,
        Blood,
        Door
    }
    
    [CreateAssetMenu(fileName = "ItemData", menuName = "GGJ21/Item Data", order = 0)]
    public class ItemData : ScriptableObject
    {
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
    }
}