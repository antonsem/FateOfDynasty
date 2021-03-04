using UnityEngine;
using UnityEngine.UI;

namespace GGJ21
{
    public class LeechUI : MonoBehaviour
    {
        [SerializeField] private Button drawBlood;
        [SerializeField] private Button putBack;

        private void Awake()
        {
            drawBlood.onClick.AddListener(OnDrawBlood);
            putBack.onClick.AddListener(OnPutBack);
        }

        private static void OnDrawBlood()
        {
            Events.Instance.useLeech?.Invoke();
        }

        private static void OnPutBack()
        {
            Events.Instance.resetLeechPosition?.Invoke();
        }
    }
}