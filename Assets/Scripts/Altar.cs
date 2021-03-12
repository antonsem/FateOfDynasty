using System;
using System.Collections;
using UnityEngine;

namespace GGJ21
{
    public enum Endings
    {
        End_1,
        End_2,
        End_3,
        End_4
    }

    public class Altar : Item
    {
        [SerializeField] private ItemData ending_4;
        [SerializeField] private ItemData ending_3;
        [SerializeField] private ItemData ending_2;
        [SerializeField] private ItemData ending_1;
        [SerializeField] private Color color_1;
        [SerializeField] private Color color_2;
        [SerializeField] private Color color_3;
        [SerializeField] private Color color_4;

        [SerializeField] private ParticleSystem particles;

        private ParticleSystem.MainModule _main;
        private Endings ending = Endings.End_1;
        
        private void Awake()
        {
            _main = particles.main;
        }

        protected override void Use()
        {
            if (Inventory.Items.ContainsKey(ItemID.Blood_4))
            {
                data = ending_4;
                _main.startColor = color_4;
                ending = Endings.End_4;
            }
            else if (Inventory.Items.ContainsKey(ItemID.Blood_3))
            {
                data = ending_3;
                _main.startColor = color_3;
                ending = Endings.End_3;
            }
            else if (Inventory.Items.ContainsKey(ItemID.Blood_2))
            {
                data = ending_2;
                _main.startColor = color_2;
                ending = Endings.End_2;
            }
            else if (Inventory.Items.ContainsKey(ItemID.Blood_1))
            {
                data = ending_1;
                _main.startColor = color_1;
                ending = Endings.End_1;
            }

            particles.Play(true);
            Events.Instance.end?.Invoke(ending);
            base.Use();
        }
    }
}