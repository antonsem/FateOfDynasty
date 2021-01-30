using UnityEngine;

namespace GGJ21
{
    public class ExperimentalTable : Item
    {
        [SerializeField] private GameObject dagger;
        [SerializeField] private GameObject leech;

        private void Awake()
        {
            dagger.SetActive(false);
            leech.SetActive(false);
        }

        public override void Clicked()
        {
            if (Inventory.Items.ContainsKey(ItemID.Dagger))
            {
                Inventory.RemoveItem(ItemID.Dagger);
                dagger.SetActive(true);
            }
            else if (Inventory.Items.ContainsKey(ItemID.CleanDagger))
            {
                Inventory.RemoveItem(ItemID.CleanDagger);
                dagger.SetActive(true);
            }
            
            if(Inventory.Items.ContainsKey(ItemID.Leech))
                leech.SetActive(true);
        }
    }
}
