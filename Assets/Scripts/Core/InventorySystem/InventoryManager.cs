using Camera;
using HUD;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;


namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Singleton;
        [SerializeField] private InventoryUI _inventoryUI;

        public int CurrentCoins { private set; get; }
        public List<Item> CurrentItems { private set; get; } = new();
        public List<Item> CurrentlyEquippedItems { private set; get; } = new();

        private void Awake()
        {
            Singleton = this;
            SetupInventory();
        }

        private void SetupInventory()
        {
            // This should be connected to a save manager of some sort
            // Setup with default clothes for now
            StartCoroutine(AddToInventory(new ItemType[] { ItemType.DefaultHead, ItemType.DefaultTorso, ItemType.DefaultPelvis}, EquipDefaultItems));
        }

        private void EquipDefaultItems()
        {
            EquipItem(ItemType.DefaultHead);
            EquipItem(ItemType.DefaultTorso);
            EquipItem(ItemType.DefaultPelvis);
            UpdateCoins(1000);
        }
        
        private Item GetItemFromInventory(ItemType itemType)
        {
            foreach (var item in CurrentItems)
                if (item.itemType == itemType)
                    return item;

            return null;
        }

        private Item GetEquippedItemOnWearableSlot(WearableSlot slot)
        {
            foreach (Item_Wearable item in CurrentlyEquippedItems)
            {
                if (item.wearableSlot == slot)
                    return item;
            }

            return null;
        }

        public IEnumerator AddToInventory(ItemType itemType, Action onTaskFinished = null)
        {
            var task = Addressables.LoadAssetAsync<Item>(itemType.ToString());
            yield return task;

            if (task.Result != null)
                CurrentItems.Add(task.Result);

            onTaskFinished?.Invoke();
        }

        public IEnumerator AddToInventory(ItemType[] itemTypes, Action onTaskFinished = null)
        {
            foreach (var itemType in itemTypes)
            {
                var task = Addressables.LoadAssetAsync<Item>(itemType.ToString());
                yield return task;

                if (task.Result != null)
                    CurrentItems.Add(task.Result);
                else
                    Debug.LogError("Item not found.");
            }

            onTaskFinished?.Invoke();
        }

        public void RemoveFromItems(ItemType itemType)
        {
            CurrentItems.Remove(GetItemFromInventory(itemType));
        }

        public void EquipItem(ItemType itemType)
        {
            Item itemToEquip = GetItemFromInventory(itemType);
            
            if (itemToEquip is not Item_Wearable)
            {
                Debug.Log("Item that wants to be equippied is not a wearable item");
                HUDManager.Singleton.ShowToast($"Already equipped");
                return;
            }

            if (itemToEquip != null)
            {
                Item_Wearable wearable = (Item_Wearable)itemToEquip;
                var currentlyEquippedItem = GetEquippedItemOnWearableSlot(wearable.wearableSlot);

                if (currentlyEquippedItem)
                {
                    CurrentItems.Add(currentlyEquippedItem);
                    CurrentlyEquippedItems.Remove(currentlyEquippedItem);
                }


                CurrentlyEquippedItems.Add(itemToEquip);
                CurrentItems.Remove(itemToEquip);
                GameManager.Singleton.CurrentPlayer.PlayerVisual.ChangeWearable(wearable.wearableSlot, wearable.clothSprite);
                _inventoryUI.PopulateInventoryUI();
                HUDManager.Singleton.ShowToast($"{itemType} has been equipped");
            }
            else
                Debug.LogError($"Can't equip {itemType}. Please check if the player really has this item");
        }

        public void OpenInventoryUI()
        {
            _inventoryUI.gameObject.SetActive(true);
            _inventoryUI.PopulateInventoryUI();
            CameraManager.Singleton.ChangeToCloseupCamera();
            GameManager.Singleton.CurrentPlayer.PlayerController.ToggleActivation(false);
        }

        public void CloseInventoryUI()
        {
            _inventoryUI.gameObject.SetActive(false);
            CameraManager.Singleton.ChangeToNormalCamera();
            GameManager.Singleton.CurrentPlayer.PlayerController.ToggleActivation(true);
        }

        public void UpdateCoins(int delta)
        {
            CurrentCoins += delta;
            HUDManager.Singleton.UpdateCoinHUD(CurrentCoins);
        }
    }
}