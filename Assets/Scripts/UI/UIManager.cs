using UnityEngine;
using UnityEngine.UI;

namespace GGJ21
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject inGame;
        [SerializeField] private GameObject pause;
        [SerializeField] private GameObject ending;
        [SerializeField] private GameObject dagger;
        [SerializeField] private GameObject leech;
        [SerializeField] private Image cursor;

        [SerializeField] private Color defaultColor;
        [SerializeField] private Color canClickColor;


        #region Unity Methods

        private void OnEnable()
        {
            Events.Instance.pause += OnPause;
            Events.Instance.end += OnEnd;
            Events.Instance.gotDagger += OnDagger;
            Events.Instance.gotLeech += OnLeech;
        }

        private void OnDisable()
        {
            if (GameState.IsQuitting) return;
            Events.Instance.pause -= OnPause;
            Events.Instance.end -= OnEnd;
            Events.Instance.gotDagger -= OnDagger;
            Events.Instance.gotLeech -= OnLeech;
        }

        private void Update()
        {
            cursor.color = Clicker.CanClick ? canClickColor : defaultColor;
        }

        #endregion

        private void OnPause(bool state)
        {
            inGame.SetActive(!state);
            pause.SetActive(state);
            ending.SetActive(false);
            dagger.SetActive(false);
            leech.SetActive(false);
        }

        private void OnEnd(Endings end)
        {
            inGame.SetActive(false);
            pause.SetActive(false);
        }

        private void OnDagger(bool state)
        {
            dagger.SetActive(state);
            inGame.SetActive(!state);
            if (!state) return;
            pause.SetActive(false);
            leech.SetActive(false);
        }

        private void OnLeech(bool state)
        {
            leech.SetActive(state);
            inGame.SetActive(!state);
            if (!state) return;
            pause.SetActive(false);
            dagger.SetActive(false);
        }
    }
}