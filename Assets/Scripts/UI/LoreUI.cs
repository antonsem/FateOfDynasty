using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ21
{
    public class LoreUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text entryCount;
        [SerializeField] private TMP_Text header;
        [SerializeField] private TMP_Text entry;
        [SerializeField] private Button next;
        [SerializeField] private Button previous;
        [SerializeField] private Button clearLoreButton;
        [SerializeField] private Button doClearLoreButton;
        [SerializeField] private Button cancelClearLoreButton;
        [SerializeField] private GameObject clearLoreQuestion;
        
        private int _currentIndex = 0;

        private void Awake()
        {
            next.onClick.AddListener(OnNext);
            previous.onClick.AddListener(OnPrevious);
            clearLoreButton.onClick.AddListener(OnClearLore);
            doClearLoreButton.onClick.AddListener(DoClearLore);
            cancelClearLoreButton.onClick.AddListener(OnCancelClearLore);
        }

        private void Start()
        {
            _currentIndex = 0;
            SetEntry(_currentIndex);
        }

        private void SetEntry(int index)
        {
            entryCount.text =
                $"{(index + 1).ToString()} / {GameData.Instance.LoreItemDictionary.Count.ToString()}";

            ItemData data = GameData.Instance.Get(index);
            header.text = GameData.Instance.LoreItemDictionary[data.name] ? data.name : "[LOCKED]";;
            entry.text = GameData.Instance.LoreItemDictionary[data.name] ? GameData.Instance.Get(index).use : "[LOCKED]";
        }

        private void OnNext()
        {
            _currentIndex = ++_currentIndex % GameData.Instance.LoreItemDictionary.Count;
            SetEntry(_currentIndex);
        }

        private void OnPrevious()
        {
            if (--_currentIndex < 0)
                _currentIndex = GameData.Instance.LoreItemDictionary.Count - 1;
            
            SetEntry(_currentIndex);
        }

        private void OnClearLore()
        {
            clearLoreQuestion.SetActive(true);
        }

        private void OnCancelClearLore()
        {
            clearLoreQuestion.SetActive(false);
        }

        private void DoClearLore()
        {
            GameData.Instance.ClearLore();
            _currentIndex = 0;
            SetEntry(_currentIndex);
            clearLoreQuestion.SetActive(false);
        }
    }
}