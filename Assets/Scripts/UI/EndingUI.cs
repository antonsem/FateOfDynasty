using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ21
{
    public class EndingUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup backgroundGroup;
        [SerializeField] private CanvasGroup entry;
        [SerializeField] private TMP_Text endingText;
        [SerializeField] private Color endingColor_1;
        [SerializeField] private Color endingColor_2;
        [SerializeField] private Color endingColor_3;
        [SerializeField] private Color endingColor_4;
        [SerializeField] private Button menu;
        [SerializeField] private Image background;
        [SerializeField] private ItemData ending_1;
        [SerializeField] private ItemData ending_2;
        [SerializeField] private ItemData ending_3;
        [SerializeField] private ItemData ending_4;
        [SerializeField] private Image fadeOut;
        [SerializeField] private float fadeTime = 1;
        
        private void Awake()
        {
            menu.onClick.AddListener(OnMenu);
            Events.Instance.end += OnEnd;
        }

        private void OnEnable()
        {
            menu.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            if (GameState.IsQuitting) return;
            Events.Instance.end -= OnEnd;
        }

        private void OnEnd(Endings end)
        {
            gameObject.SetActive(true);
            StartCoroutine(EndCoroutine(end));
        }

        private IEnumerator EndCoroutine(Endings end)
        {
            background.color = Color.white;
            float t = 0;
            while (t < 1)
            {
                backgroundGroup.alpha = Mathf.Lerp(0, 0.1f, t);
                t += Time.deltaTime * 0.375f;
                yield return null;
            }

            backgroundGroup.alpha = 1;
            switch (end)
            {
                case Endings.End_1:
                    background.CrossFadeColor(endingColor_1, 3, true, false);
                    GameData.Instance.Unlock(ending_1.name);
                    endingText.text = ending_1.use;
                    break;
                case Endings.End_2:
                    background.CrossFadeColor(endingColor_2, 3, true, false);
                    GameData.Instance.Unlock(ending_2.name);
                    endingText.text = ending_2.use;
                    break;
                case Endings.End_3:
                    background.CrossFadeColor(endingColor_3, 3, true, false);
                    GameData.Instance.Unlock(ending_3.name);
                    endingText.text = ending_3.use;
                    break;
                case Endings.End_4:
                    background.CrossFadeColor(endingColor_4, 3, true, false);
                    GameData.Instance.Unlock(ending_4.name);
                    endingText.text = ending_4.use;
                    break;
            }

            WaitForSeconds wait = new WaitForSeconds(3);
            yield return wait;

            t = 0;
            while (t < 1)
            {
                entry.alpha = Mathf.Lerp(0, 1, t);
                t += Time.deltaTime;
                yield return null;
            }
            
            entry.alpha = Mathf.Lerp(0, 1, t);
            menu.gameObject.SetActive(true);
        }

        private void OnMenu()
        {
            AudioPlayer.Instance.SetMusic(false);
            StartCoroutine(NewGameCoroutine());
        }
        
        private IEnumerator NewGameCoroutine()
        {
            float t = 0;
            while (t < 1)
            {
                fadeOut.color = Color.Lerp(Color.clear, Color.black, t);
                t += Time.deltaTime / fadeTime;
                yield return null;
            }

            fadeOut.color = Color.black;
            yield return null;
            
            GameState.QuitToMenu();
        }
    }
}