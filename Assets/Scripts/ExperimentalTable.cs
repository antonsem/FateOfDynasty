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

        private void OnEnable()
        {
            Events.Instance.gotTheBlood += OnGotTheBlood;
        }

        private void OnDisable()
        {
            if(GameState.IsQuitting) return;
            Events.Instance.gotTheBlood += OnGotTheBlood;
        }
        
        private void OnGotTheBlood()
        {
            dagger.gameObject.SetActive(false);
            leech.gameObject.SetActive(false);
            Events.Instance.displayMessage?.Invoke("I've got all I need. I should be able to perform the ritual on the altar!");
            _used = true;
        }

        protected override void Use()
        {
            if(_used)
                Events.Instance.displayMessage?.Invoke("I've got all I need. I should be able to perform the ritual on the altar!");

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