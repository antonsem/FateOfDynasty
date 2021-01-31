using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GGJ21
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private Button newGame;
        [SerializeField] private Button credits;
        [SerializeField] private Button quit;

        private void Awake()
        {
            newGame.onClick.AddListener(OnNewGame);
            credits.onClick.AddListener(OnCredits);
            quit.onClick.AddListener(OnQuit);
        }

        private void OnNewGame()
        {
            SceneManager.LoadScene("Main");
        }

        private void OnCredits()
        {
            SceneManager.LoadScene("Credits");
        }

        private void OnQuit()
        {
            Application.Quit();
        }
    }
}