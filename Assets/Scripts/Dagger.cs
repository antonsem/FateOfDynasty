using UnityEngine;

namespace GGJ21
{
    public class Dagger : Item
    {
        [SerializeField] private ItemData blood_1;
        [SerializeField] private ItemData blood_2;
        
        public bool IsSilver { get; set; }
        
        public override void Clicked()
        {
            if (Inventory.Items.ContainsKey(ItemID.Blood_4) || Inventory.Items.ContainsKey(ItemID.Blood_3))
            {
                CantUse();
                return;
            }
            
            base.Clicked();
        }

        protected override void Use()
        {
            data.addItemOnUse = IsSilver ? blood_2 : blood_1;
            base.Use();
        }
    }
}