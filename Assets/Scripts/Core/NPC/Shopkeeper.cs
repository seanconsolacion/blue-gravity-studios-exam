using Inventory;
using Player;
using ShopSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShopSystem
{
    public class Shopkeeper : MonoBehaviour, IInteractable
    {
        [SerializeField] private Item[] _shopItems;

        public void Interact()
        {
            ShopUI.Singleton.ShowShop(_shopItems);
            GameManager.Singleton.CurrentPlayer.PlayerController.ToggleActivation(false);
        }
    }
}