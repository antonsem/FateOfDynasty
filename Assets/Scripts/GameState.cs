using UnityEngine;

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
                if(_isPaused == value) return;
                _isPaused = value;
                Cursor.visible = value;
                Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
                Events.Instance.pause?.Invoke(value);
            }
        }

        #region Unity Methods

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
        
    }
}