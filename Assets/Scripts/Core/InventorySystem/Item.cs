using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class Item : ScriptableObject
    {
        public string itemName;
        public int price;
        public Sprite inventoryIcon;
    }
}