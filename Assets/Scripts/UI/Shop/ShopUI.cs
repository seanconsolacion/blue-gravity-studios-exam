using Inventory;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

namespace ShopSystem
{
    public class ShopUI : MonoBehaviour
    {
        public static ShopUI Singleton;
        [SerializeField] private GameObject _shopPanel;
        [SerializeField] private GameObject _shopButtonPrefab;
        [SerializeField] private Transform _shopButtonsParent;
        [SerializeField] private InventoryManager _inventoryManager;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _sellButton;

        private Item[] _currentShopItems;

        private void Awake()
        {
            Singleton = this;
        }

        public void ShowShop(Item[] shopItems, bool forSelling = false)
        {
            ClearShopButtons();

            for (int i = 0; i < shopItems.Length; i++)
                Instantiate(_shopButtonPrefab, _shopButtonsParent).GetComponent<ShopButton>().Setup(_inventoryManager, shopItems[i], forSelling);

            if (!forSelling)
                _currentShopItems = shopItems;

            _shopPanel.SetActive(true);
            _sellButton.interactable = !forSelling;
            _buyButton.interactable = forSelling;
        }

        public void ClearShopButtons()
        {
            for (int i = 0; i < _shopButtonsParent.childCount; i++)
                Destroy(_shopButtonsParent.GetChild(i).gameObject);
        }

        public void ShowSellPanel()
        {
            ShowShop(_inventoryManager.CurrentItems.ToArray(), true);
            _sellButton.interactable = false;
            _buyButton.interactable = true;
        }

        public void ReshowShop()
        {
            ShowShop(_currentShopItems);
        }
    }
}