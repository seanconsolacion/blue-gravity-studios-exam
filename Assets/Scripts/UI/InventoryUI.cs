using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private GameObject _inventoryButtonPrefab;
        [SerializeField] private Transform _inventoryContentParent;
        [SerializeField]  private InventoryManager _inventoryManager;

        public void PopulateInventoryUI()
        {
            ClearInventoryButtons();

            foreach (var item in _inventoryManager.CurrentlyEquippedItems)
                Instantiate(_inventoryButtonPrefab, _inventoryContentParent).GetComponent<InventoryButton>().Setup(_inventoryManager, item, true);

            foreach (var item in _inventoryManager.CurrentItems)
                Instantiate(_inventoryButtonPrefab, _inventoryContentParent).GetComponent<InventoryButton>().Setup(_inventoryManager, item, false);
        }

        public void ClearInventoryButtons()
        {
            for (int i = 0; i < _inventoryContentParent.childCount; i++)
                Destroy(_inventoryContentParent.GetChild(i).gameObject);
        }
    }
}