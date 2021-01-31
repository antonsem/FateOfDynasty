﻿using UnityEngine;
using UnityEngine.Serialization;

namespace GGJ21
{
    public class Leech : Item
    {
        [SerializeField] private ItemData blood_3;
        [SerializeField] private ItemData blood_4;
        
        public bool IsSilver { get; set; } = false;
        public bool CanUse { get; set; } = false;

        protected override void Use()
        {
            if (!CanUse)
            {
                CantUse();
                return;
            }
            
            if (Inventory.Items.ContainsKey(ItemID.Blood_4) || Inventory.Items.ContainsKey(ItemID.Blood_3))
            {
                Events.Instance.displayMessage?.Invoke(data.used);
                return;
            }
            base.Use();
            Inventory.AddItem(IsSilver ? blood_4 : blood_3);
        }
    }
}