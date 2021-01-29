using System;
using UnityEngine;

namespace GGJ21
{
    public class GameState : MonoBehaviour
    {
        public static bool IsQuitting { get; private set; } = false;

        private void OnApplicationQuit()
        {
            IsQuitting = true;
        }
    }
}