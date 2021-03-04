using UnityEngine;

namespace GGJ21
{
    public class Leech : Item
    {
        [SerializeField] private ItemData blood_3;
        [SerializeField] private ItemData blood_4;
        [SerializeField] private AudioClip enableClip;
        [SerializeField] private Transform playerHand; // Never do this! Bad programmer!

        public bool IsSilver { get; set; } = false;
        public bool CanUse { get; set; } = false;

        private void OnEnable()
        {
            if (!enableClip) return;
            if(audioSource)
                audioSource.PlayOneShot(enableClip);
            else
                AudioPlayer.PlaySound(enableClip);
            
            Events.Instance.resetLeechPosition += ResetPosition;
            Events.Instance.useLeech += Use;
        }

        public override void Clicked()
        {
            GetToPlayer();
        }
        
        private void GetToPlayer()
        {
            GameState.HasLeech = true;
            transform.position = playerHand.position;
            transform.rotation = Quaternion.Euler(playerHand.eulerAngles + Vector3.up * 90);
        }

        private void ResetPosition()
        {
            GameState.HasLeech = false;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

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
            Events.Instance.gotTheBlood?.Invoke();
            ResetPosition();
        }
    }
}