using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "New Clothe", menuName = "Create new Clothe")]
    public class Item_Wearable : Item
    {
        public WearableSlot wearableSlot;
        public Sprite clothSprite;
    }

    public enum WearableSlot
    {
        Head,
        Torso,
        Pelvis
    }
}
