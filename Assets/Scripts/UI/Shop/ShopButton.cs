using Inventory;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

namespace ShopSystem
{
    public class ShopButton : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _itemNameText;
        [SerializeField] private TextMeshProUGUI _interactButtonText;

        private InventoryManager _inventoryManager;
        private Item _currentItem;
        private int _currentItemIndex;

        public void Setup(InventoryManager inventoryManager, Item currentItem, int index, bool forSelling)
        {
            _inventoryManager = inventoryManager;
            _currentItem = currentItem;

            _icon.sprite = currentItem.inventoryIcon;
            _priceText.text = forSelling ? (currentItem.price / 2).ToString() : currentItem.price.ToString();
            _itemNameText.text = currentItem.itemType.ToString();
            _interactButtonText.text = forSelling ? "Sell" : "Buy";
            _currentItemIndex = index;
        }

        public void Interact()
        {
            if (_interactButtonText.text == "Sell")
            {
                _inventoryManager.RemoveFromItems(_currentItemIndex);
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(_inventoryManager.AddToInventory(_currentItem.itemType));
            }
        }
    }
}