using UnityEngine;

namespace GGJ21
{
    public class Altar : Item
    {
        [SerializeField] private ItemData ending_4;
        [SerializeField] private ItemData ending_3;
        [SerializeField] private ItemData ending_2;
        [SerializeField] private ItemData ending_1;

        protected override void Use()
        {
            if (Inventory.Items.ContainsKey(ItemID.Blood_4))
                data = ending_4;
            else if (Inventory.Items.ContainsKey(ItemID.Blood_3))
                data = ending_3;
            else if (Inventory.Items.ContainsKey(ItemID.Blood_2))
                data = ending_2;
            else if (Inventory.Items.ContainsKey(ItemID.Blood_1))
                data = ending_1;
            
            base.Use();
        }
    }
}