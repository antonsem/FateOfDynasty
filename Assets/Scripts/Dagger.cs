using UnityEngine;

namespace GGJ21
{
    public class Dagger : Item
    {
        [SerializeField] private ItemData blood_1;
        [SerializeField] private ItemData blood_2;
        [SerializeField] private ItemData rustyDaggerData;
        [SerializeField] private GameObject silverDagger;
        [SerializeField] private GameObject rustyDagger;
        [SerializeField] private AudioClip enableClip;
        [SerializeField] private Transform playerHand; // Never do this! Bad programmer!
        [SerializeField] private ExperimentalTable table; // Never do this! Bad programmer!
        
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

        private void OnEnable()
        {
            if (!enableClip) return;
            if(audioSource)
                audioSource.PlayOneShot(enableClip);
            else
                AudioPlayer.PlaySound(enableClip);

            Events.Instance.resetDaggerPosition += ResetPosition;
            Events.Instance.useDagger += Use;
            Events.Instance.pickUpDagger += PickUpDagger;
        }

        private void OnDisable()
        {
            if (GameState.IsQuitting) return;
            Events.Instance.resetDaggerPosition -= ResetPosition;
            Events.Instance.useDagger -= Use;
            Events.Instance.pickUpDagger -= PickUpDagger;
        }

        public override void Clicked()
        {
            if (Inventory.Items.ContainsKey(ItemID.Blood_4) || Inventory.Items.ContainsKey(ItemID.Blood_3))
            {
                CantUse();
                return;
            }
            
            GetToPlayer();
        }

        private void GetToPlayer()
        {
            GameState.HasDagger = true;
            transform.position = playerHand.position;
            transform.rotation = playerHand.rotation;
        }

        private void ResetPosition()
        {
            GameState.HasDagger = false;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        protected override void Use()
        {
            data.addItemOnUse = IsSilver ? blood_2 : blood_1;
            base.Use();
            Events.Instance.gotTheBlood?.Invoke();
            ResetPosition();
        }

        private void PickUpDagger()
        {
            Inventory.AddItem(rustyDaggerData);
            table.GetBackDagger();
            ResetPosition();
        }
    }
}