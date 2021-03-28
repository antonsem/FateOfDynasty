using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GGJ21
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private Button newGame;
        [SerializeField] private Button credits;
        [SerializeField] private Button lore;
        [SerializeField] private Button quit;
        [SerializeField] private CanvasGroup creditsGroup;
        [SerializeField] private CanvasGroup loreGroup;
        [SerializeField] private Image fadeOut;
        [SerializeField] private float fadeTime = 1;

        private IEnumerator _workingCoroutine;
        private bool _isLoading = false;

        private void Awake()
        {
            newGame.onClick.AddListener(OnNewGame);
            credits.onClick.AddListener(OnCredits);
            lore.onClick.AddListener(OnLore);
            quit.onClick.AddListener(OnQuit);
            _isLoading = true;
            AudioPlayer.Instance.SetMusic(true, () => { _isLoading = false; });
        }

        private void OnNewGame()
        {
            if (_workingCoroutine != null)
                return;

            AudioPlayer.Instance.SetMusic(false, () => { _isLoading = false; });
            _workingCoroutine = NewGameCoroutine();
            StartCoroutine(_workingCoroutine);
        }

        private void OnLore()
        {
            if (_workingCoroutine != null)
                return;

            float target = 1 - loreGroup.alpha;
            if (target > 0 && creditsGroup.alpha > 0)
                _workingCoroutine = SwitchPanels(creditsGroup, loreGroup);
            else
                _workingCoroutine = SetCanvasGroupCoroutine(loreGroup, target);
            
            StartCoroutine(_workingCoroutine);
        }
        
        private void OnCredits()
        {
            if (_workingCoroutine != null)
                return;

            float target = 1 - creditsGroup.alpha;
            if (target > 0 && loreGroup.alpha > 0)
                _workingCoroutine = SwitchPanels(loreGroup, creditsGroup);
            else
                _workingCoroutine = SetCanvasGroupCoroutine(creditsGroup, target);
            
            StartCoroutine(_workingCoroutine);
        }

        private static void OnQuit()
        {
            Application.Quit();
        }

        private IEnumerator SwitchPanels(CanvasGroup from, CanvasGroup to)
        {
            if (from.alpha > 0)
                yield return StartCoroutine(SetCanvasGroupCoroutine(from, 0));

            StartCoroutine(SetCanvasGroupCoroutine(to));
        }

        private IEnumerator SetCanvasGroupCoroutine(CanvasGroup group, float alpha = 1)
        {
            float t = 1 - alpha;
            float dir = Mathf.Sign(alpha - t);
            while (Mathf.Abs(t - alpha) > 0.01f)
            {
                group.alpha = t;
                t = Mathf.Clamp01(t + Time.deltaTime * 3 * dir);
                yield return null;
            }

            group.alpha = alpha;
            _workingCoroutine = null;
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
            while (_isLoading)
                yield return null;

            SceneManager.LoadScene("Main");
        }
    }
}