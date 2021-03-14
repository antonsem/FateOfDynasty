using System.Collections;
using UnityEngine;

namespace GGJ21
{
    public class FadeOutUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup group;
        [SerializeField] private float screenTime = 5;

        private void Awake()
        {
            group.alpha = 0;
        }

        private void OnEnable()
        {
            Events.Instance.showInstructions += ShowInstructions;
        }

        private void OnDisable()
        {
            if (GameState.IsQuitting) return;
            Events.Instance.showInstructions -= ShowInstructions;
        }

        private IEnumerator FadeOutCoroutine()
        {
            group.alpha = 1;
            WaitForSeconds wait = new WaitForSeconds(screenTime);
            yield return wait;

            float t = 1;
            while (t > 0)
            {
                group.alpha = Mathf.Lerp(0, 1, t);
                t -= Time.deltaTime * 0.5f;
                yield return null;
            }

            gameObject.SetActive(false);
        }

        private void ShowInstructions()
        {
            StartCoroutine(FadeOutCoroutine());
        }
    }
}