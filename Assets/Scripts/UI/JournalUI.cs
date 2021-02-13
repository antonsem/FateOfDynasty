using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ21
{
    public class JournalUI : MonoBehaviour
    {
        [SerializeField] private Button resume;
        [SerializeField] private Button next;
        [SerializeField] private Button previous;
        [SerializeField] private TMP_Text entry;
        [SerializeField] private TMP_Text label;
        [SerializeField] private AudioClip pageSound;
        
        private List<string> _entries = new List<string>();
        private int _index = 0;

        private void Awake()
        {
            Events.Instance.addLog += OnNewMessage;
            next.onClick.AddListener(OnNext);
            previous.onClick.AddListener(OnPrevious);
            resume.onClick.AddListener(OnResume);
        }

        private void OnEnable()
        {
            UpdateLabel();
        }

        private void OnDestroy()
        {
            if (GameState.IsQuitting) return;
            Events.Instance.addLog -= OnNewMessage;
        }

        private void UpdateLabel()
        {
            label.text = $"Entry {(_entries.Count == 0 ? 0 : _index + 1).ToString()}/{_entries.Count.ToString()}";
        }
        
        private void OnNewMessage(string msg)
        {
            _index = _entries.IndexOf(msg);
            if (_index == -1)
            {
                _entries.Add(msg);
                _index = _entries.Count - 1;
            }

            ShowMessage(_entries[_index]);
            GameState.IsPaused = true;
        }

        private void ShowMessage(in string msg)
        {
            entry.text = msg;
            UpdateLabel();
            AudioPlayer.PlaySound(pageSound);
        }

        private void OnNext()
        {
            if (_entries.Count == 0)
            {
                ShowMessage("");
                return;
            }

            _index = (_index + 1) % _entries.Count;
            ShowMessage(_entries[_index]);
        }

        private void OnPrevious()
        {
            if (_entries.Count == 0)
            {
                ShowMessage("");
                return;
            }
            
            _index--;
            if (_index < 0)
                _index = _entries.Count - 1;
            
            ShowMessage(_entries[_index]);
        }

        private static void OnResume()
        {
            GameState.IsPaused = false;
        }
    }
}