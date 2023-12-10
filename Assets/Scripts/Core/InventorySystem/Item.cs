using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class Item : ScriptableObject
    {
        public ItemType itemType;
        public int price;
        public Sprite inventoryIcon;
    }

    public enum ItemType
    {
        DefaultHead,
        DefaultTorso,
        DefaultPelvis,
        Head1,
        Head2,
        Head3,
        Torso1,
        Torso2,
        Torso3,
        Pelvis1,
        Pelvis2,
        Pelvis3
    }
}