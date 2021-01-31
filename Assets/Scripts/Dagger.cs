﻿using UnityEngine;

namespace GGJ21
{
    public class Dagger : Item
    {
        [SerializeField] private ItemData blood_1;
        [SerializeField] private ItemData blood_2;
        [SerializeField] private GameObject silverDagger;
        [SerializeField] private GameObject rustyDagger;

        private bool _isSilver = false;
        public bool IsSilver
        {
            get => _isSilver;
            set
            {
                _isSilver = value;
                silverDagger.gameObject.SetActive(value);
                rustyDagger.gameObject.SetActive(!value);
            }
        }
        
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