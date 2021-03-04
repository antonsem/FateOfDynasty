using UnityEngine;
using UnityEngine.UI;

namespace GGJ21
{
    public class DaggerUI : MonoBehaviour
    {
        [SerializeField] private Button pickUp;
        [SerializeField] private Button drawBlood;
        [SerializeField] private Button putBack;

        private Dagger _dagger;

        private void Awake()
        {
            _dagger = FindObjectOfType<Dagger>(); //yeah sure, awesome idea...

            pickUp.onClick.AddListener(OnPickUp);
            drawBlood.onClick.AddListener(OnDrawBlood);
            putBack.onClick.AddListener(OnPutBack);
        }

        private void OnEnable()
        {
            pickUp.gameObject.SetActive(!_dagger.IsSilver);
        }

        private static void OnPickUp()
        {
            Events.Instance.pickUpDagger?.Invoke();
        }

        private static void OnDrawBlood()
        {
            Events.Instance.useDagger?.Invoke();
        }

        private static void OnPutBack()
        {
            Events.Instance.resetDaggerPosition?.Invoke();
        }
    }
}