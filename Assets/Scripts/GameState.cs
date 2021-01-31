using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace GGJ21
{
    public class GameState : MonoBehaviour
    {
        public static bool IsQuitting { get; private set; } = false;

        private static bool _isPaused = false;
        public static bool IsPaused
        {
            get => _isPaused;
            set
            {
                _isPaused = value;
                Cursor.visible = value;
                Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
                Controller.enabled = !value;
                Clicker.enabled = !value;
                Events.Instance.pause?.Invoke(value);
            }
        }

        private static FirstPersonController _controller;
        private static FirstPersonController Controller
        {
            get
            {
                if (!_controller) _controller = FindObjectOfType<FirstPersonController>();
                return _controller;
            }
        }

        private static Clicker _clicker;
        private static Clicker Clicker
        {
            get
            {
                if (!_clicker) _clicker = FindObjectOfType<Clicker>();
                return _clicker;
            }
        }
        
        #region Unity Methods

        private void OnEnable()
        {
            Events.Instance.end += OnEnd;
        }

        private void OnDisable()
        {
            if(IsQuitting) return;
            Events.Instance.end -= OnEnd;
        }

        private void Start()
        {
            IsPaused = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                IsPaused = !IsPaused;
        }
        
        private void OnApplicationQuit()
        {
            IsQuitting = true;
        }

        #endregion

        private static void OnEnd(Endings end)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Controller.enabled = false;
            Clicker.enabled = false;
        }
    }
}