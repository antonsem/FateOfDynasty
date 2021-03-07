using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GGJ21
{
    public class QuitUI : MonoBehaviour
    {
        [SerializeField] private Button yes;
        [SerializeField] private Button no;

        private void Awake()
        {
            yes.onClick.AddListener(OnYes);
            no.onClick.AddListener(OnNo);
        }

        private static void OnYes()
        {
            SceneManager.LoadScene("3DMenu");
        }

        private void OnNo()
        {
            gameObject.SetActive(false);
        }
    }
}
