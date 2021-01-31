using System;
using System.Collections;
using NUnit.Framework;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GGJ21
{
    public class EndingUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup backgroundGroup;
        [SerializeField] private CanvasGroup entry;
        [SerializeField] private TMP_Text endingText;
        [SerializeField] private Color ending_1;
        [SerializeField] private Color ending_2;
        [SerializeField] private Color ending_3;
        [SerializeField] private Color ending_4;
        [SerializeField] private Button menu;
        [SerializeField] private Image background;
        [SerializeField, TextArea(5, 20)] private string endingText_1;
        [SerializeField, TextArea(5, 20)] private string endingText_2;
        [SerializeField, TextArea(5, 20)] private string endingText_3;
        [SerializeField, TextArea(5, 20)] private string endingText_4;
        
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
                    background.CrossFadeColor(ending_1, 3, true, false);
                    endingText.text = endingText_1;
                    break;
                case Endings.End_2:
                    background.CrossFadeColor(ending_2, 3, true, false);
                    endingText.text = endingText_2;
                    break;
                case Endings.End_3:
                    background.CrossFadeColor(ending_3, 3, true, false);
                    endingText.text = endingText_3;
                    break;
                case Endings.End_4:
                    background.CrossFadeColor(ending_4, 3, true, false);
                    endingText.text = endingText_4;
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

        private static void OnMenu()
        {
            SceneManager.LoadScene("Main");
        }
    }
}