using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GGJ21
{
    public class Credits : MonoBehaviour
    {
        [SerializeField] private Button menu;

        private void Awake()
        {
            menu.onClick.AddListener(OnMenu);
        }

        private void OnMenu()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
