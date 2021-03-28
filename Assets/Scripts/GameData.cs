using System.Collections.Generic;
using System.IO;
using ExtraTools;
using UnityEngine;
using Newtonsoft.Json;

namespace GGJ21
{
    // This whole class is a very very bad idea! Don't do it! It works in this particular instance
    // because this is a very small project with a limited scope, and I'm not planning to work on
    // this project any further. All I needed was a quick and dirty way to save the progress, and
    // this class is exactly that: a quick dirty solution which works in this particular instance.
    public class GameData : Singleton<GameData>
    {
        [SerializeField] private ItemData[] loreItems;
        private Dictionary<string, bool> _loreItemDictionary = new Dictionary<string, bool>();
        public Dictionary<string, bool> LoreItemDictionary
        {
            get
            {
                if (_loreItemDictionary.Count == loreItems.Length) return _loreItemDictionary;
                
                foreach (ItemData item in loreItems)
                    _loreItemDictionary.Add(item.name, false);

                return _loreItemDictionary;
            }
            private set => _loreItemDictionary = value;
        }

        private void Awake()
        {
            if (FindObjectsOfType<GameData>().Length > 1)
            {
                Destroy(gameObject);
                return;
            }
            
            DontDestroyOnLoad(gameObject);
            
            LoadProgress();
        }

        public ItemData Get(int index)
        {
            return Get(loreItems[index].name);
        }

        private ItemData Get(in string itemName)
        {
            foreach (ItemData item in loreItems)
            {
                if (item.name == itemName)
                    return item;
            }
            
            Debug.LogError($"Cannot find item with name {itemName}!");

            return null;
        }

        public void Unlock(in string itemName)
        {
            if (!LoreItemDictionary.ContainsKey(itemName))
            {
                Debug.LogError($"Cannot find item with name {itemName} in lore dictionary!", this);
                return;
            }

            LoreItemDictionary[itemName] = true;
            SaveProgress();
        }

        private void SaveProgress()
        {
            string json = JsonConvert.SerializeObject(LoreItemDictionary, Formatting.Indented);
            File.WriteAllText($"{Application.persistentDataPath}/save.json", json);
        }

        private void LoadProgress()
        {
            if(!File.Exists($"{Application.persistentDataPath}/save.json")) return;
            string json = File.ReadAllText($"{Application.persistentDataPath}/save.json");
            LoreItemDictionary = JsonConvert.DeserializeObject<Dictionary<string, bool>>(json);
        }

        public void ClearLore()
        {
            foreach (ItemData key in loreItems)
                LoreItemDictionary[key.name] = false;
            
            SaveProgress();
        }
    }
}