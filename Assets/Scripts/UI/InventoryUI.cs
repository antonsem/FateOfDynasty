using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ21
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private GameObject itemIconPrefab;
        [SerializeField] private float fadeTime = 0.5f;

        private Dictionary<ItemID, Image> itemImageDict = new Dictionary<ItemID, Image>();

        private IEnumerator _removeCoroutine;
        private ItemID _removingItem = ItemID.None;
        
        #region Pool
        
        private List<Image> _items = new List<Image>();

        private Image GetIcon()
        {
            foreach (Image item in _items)
            {
                if (item.gameObject.activeSelf) continue;
                item.gameObject.SetActive(true);
                return item;
            }
            
            _items.Add(Instantiate(itemIconPrefab, transform).GetComponent<Image>());
            return _items[_items.Count - 1];
        }
        
        #endregion

        #region Unity Methods

        private void Awake()
        {
            Events.Instance.pickedUp += AddItem;
            Events.Instance.removeItem += RemoveItem;
        }

        private void OnDestroy()
        {
            if (GameState.IsQuitting) return;
            Events.Instance.pickedUp -= AddItem;
            Events.Instance.removeItem -= RemoveItem;
        }

        private void OnDisable()
        {
            if (GameState.IsQuitting) return;
            if(_removingItem == ItemID.None) return;
            itemImageDict[_removingItem].gameObject.SetActive(false);
            itemImageDict.Remove(_removingItem);
            _removingItem = ItemID.None;
            _removeCoroutine = null;
        }

        #endregion

        private void AddItem(ItemData data)
        {
            Image item = GetIcon();
            item.color = Color.white;
            item.sprite = data.icon;
            item.CrossFadeAlpha(0.5f, fadeTime, true);
            itemImageDict.Add(data.itemId, item);
        }

        private void RemoveItem(ItemData data)
        {
            if (_removeCoroutine != null)
            {
                StopCoroutine(_removeCoroutine);
                itemImageDict[_removingItem].gameObject.SetActive(false);
                itemImageDict.Remove(_removingItem);
                _removingItem = ItemID.None;
            }

            _removeCoroutine = RemoveItemCoroutine(data.itemId);
            StartCoroutine(_removeCoroutine);
        }
        
        private IEnumerator RemoveItemCoroutine(ItemID itemId)
        {
            if (!itemImageDict.ContainsKey(itemId)) yield break;
            _removingItem = itemId;
            itemImageDict[itemId].color = Color.white;
            itemImageDict[itemId].CrossFadeAlpha(0, fadeTime, true);

            WaitForSeconds wait = new WaitForSeconds(fadeTime);
            yield return wait;
            
            itemImageDict[itemId].gameObject.SetActive(false);
            itemImageDict.Remove(itemId);
            _removingItem = ItemID.None;
            _removeCoroutine = null;
        }
    }
}