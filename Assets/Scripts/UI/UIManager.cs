using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ21
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject inGame;
        [SerializeField] private GameObject pause;
        [SerializeField] private GameObject ending;
        [SerializeField] private Image cursor;

        [SerializeField] private Color defaultColor;
        [SerializeField] private Color canClickColor;
        
        
        #region Unity Methods

        private void OnEnable()
        {
            Events.Instance.pause += OnPause;
            Events.Instance.end += OnEnd;
        }

        private void OnDisable()
        {
            if(GameState.IsQuitting) return;
            Events.Instance.pause -= OnPause;
            Events.Instance.end -= OnEnd;
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
        }

        private void OnEnd(Endings end)
        {
            inGame.SetActive(false);
            pause.SetActive(false);
        }
    }
}
