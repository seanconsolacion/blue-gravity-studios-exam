using HUD;
using Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InventoryButton : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _isEquippedMark;
        [SerializeField] private GameObject _usePanel;

        private InventoryManager _inventoryManager;
        private Item _currentItem;

        public void Setup(InventoryManager inventoryManager, Item currentItem, bool isEquipped)
        {
            _inventoryManager = inventoryManager;
            _currentItem = currentItem;

            _icon.sprite = currentItem.inventoryIcon;
            _isEquippedMark.SetActive(isEquipped);
        }

        public void TriggerCurrentUse()
        {
            // This normally would have many uses but we just have equip for now
            _inventoryManager.EquipItem(_currentItem.itemType);
            HUDManager.Singleton.ShowToast($"{_currentItem.itemType} has been equipped");
        }
    }
}