using System.Collections;
using TMPro;
using UnityEngine;

namespace GGJ21
{
    public class DialogUI : MonoBehaviour
    {
        #region Fields

        [SerializeField] private CanvasGroup group;
        [SerializeField] private TMP_Text message;
        [SerializeField] private float timeOutCoefficient = 5;
        [SerializeField] private float minTimeOut = 3;
        [SerializeField] private float fadeTime = 4f;
        
        private IEnumerator _message;

        #endregion
        
        #region Unity Methods

        private void OnEnable()
        {
            Events.Instance.displayMessage += OnMessage;
        }

        private void OnDisable()
        {
            if (_message != null)
            {
                StopCoroutine(_message);
                _message = null;
                group.alpha = 0;
            }
            
            if (GameState.IsQuitting) return;
            Events.Instance.displayMessage -= OnMessage;
        }

        #endregion
        
        private void OnMessage(string msg)
        {
            if(_message != null)
                StopCoroutine(_message);

            _message = ShowMessageCoroutine(msg);
            StartCoroutine(_message);
        }

        private IEnumerator ShowMessageCoroutine(string msg)
        {
            message.text = msg;
            float t = 0;
            while (t < 1)
            {
                group.alpha = Mathf.Lerp(0, 1, t);
                t += Time.deltaTime * fadeTime;
                yield return null;
            }

            float timer = Mathf.Max(minTimeOut, timeOutCoefficient * msg.Length);

            while (timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null;
            }
            
            t = 0;
            while (t < 1)
            {
                group.alpha = Mathf.Lerp(1, 0, t);
                t += Time.deltaTime * fadeTime;
                yield return null;
            }

            group.alpha = 0;
            _message = null;
        }
    }
}