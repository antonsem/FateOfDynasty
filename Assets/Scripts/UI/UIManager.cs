using UnityEngine;

namespace GGJ21
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject inGame;
        [SerializeField] private GameObject pause;

        #region Unity Methods

        private void OnEnable()
        {
            Events.Instance.pause += OnPause;
        }

        private void OnDisable()
        {
            if(GameState.IsQuitting) return;
            Events.Instance.pause -= OnPause;
        }

        #endregion
        
        private void OnPause(bool state)
        {
            inGame.SetActive(!state);
            pause.SetActive(state);
        }
    }
}
