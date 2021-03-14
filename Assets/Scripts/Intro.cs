using System.Collections;
using UnityEngine;

namespace GGJ21
{
    public class Intro : MonoBehaviour
    {
        [SerializeField] private ItemData data;

        private IEnumerator Start()
        {
            yield return null;
            Events.Instance.addLog?.Invoke(data.use);
            yield return null;
            Events.Instance.pause += StartGame;
        }

        private static void StartGame(bool val)
        {
            if(val) return;
            Events.Instance.pause -= StartGame;
            Events.Instance.showInstructions.Invoke();
        }
    }
}
