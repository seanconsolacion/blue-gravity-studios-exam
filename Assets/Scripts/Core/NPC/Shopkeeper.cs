using Inventory;
using ShopSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : MonoBehaviour, IInteractable
{
    [SerializeField] private Item[] _shopItems;

    public void Interact()
    {
        ShopUI.Singleton.ShowShop(_shopItems);
    }
}
