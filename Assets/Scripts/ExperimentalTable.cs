using UnityEngine;

namespace GGJ21
{
    public class ExperimentalTable : Item
    {
        [SerializeField] private Dagger dagger;
        [SerializeField] private Leech leech;

        private void Awake()
        {
            dagger.gameObject.SetActive(false);
            leech.gameObject.SetActive(false);
        }

        protected override void Use()
        {
            if (Inventory.Items.ContainsKey(ItemID.Dagger))
            {
                Inventory.RemoveItem(ItemID.Dagger);
                leech.IsSilver = false;
                leech.CanUse = true;
                dagger.IsSilver = false;
                dagger.gameObject.SetActive(true);
            }
            else if (Inventory.Items.ContainsKey(ItemID.SilverDagger))
            {
                Inventory.RemoveItem(ItemID.SilverDagger);
                leech.IsSilver = true;
                leech.CanUse = true;
                dagger.IsSilver = true;
                dagger.gameObject.SetActive(true);
            }

            if (Inventory.Items.ContainsKey(ItemID.Leech))
            {
                Inventory.RemoveItem(ItemID.Leech);
                leech.gameObject.SetActive(true);
            }
        }
    }
}