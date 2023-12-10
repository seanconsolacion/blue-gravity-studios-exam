using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Singleton;
        private List<Item> _currentItems = new List<Item>();

        private void Awake()
        {
            Singleton = this;
            SetupInventory();
        }

        private void SetupInventory()
        {
            // This should be connected to a save manager of some sort
            // Setup with default clothes for now
            StartCoroutine(AddToInventory(new string[] { "DefaultHead", "DefaultTorso", "DefaultPelvis" }));
        }

        public IEnumerator AddToInventory(string itemName, Action onTaskFinished = null)
        {
            var task = Addressables.LoadAssetAsync<Item>(itemName);
            yield return task;

            if (task.Result != null)
                _currentItems.Add(task.Result);

            onTaskFinished?.Invoke();
        }

        public IEnumerator AddToInventory(string[] itemNames, Action onTaskFinished = null)
        {
            foreach (var itemName in itemNames)
            {
                var task = Addressables.LoadAssetAsync<Item>(itemName);
                yield return task;

                if (task.Result != null)
                    _currentItems.Add(task.Result);
                else
                    Debug.LogError("Item not found.");
            }

            onTaskFinished?.Invoke();
        }
    }
}