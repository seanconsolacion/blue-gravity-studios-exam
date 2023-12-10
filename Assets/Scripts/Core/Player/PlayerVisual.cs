using Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField] private Animator _modelAnimator;
        [SerializeField] private PlayerWearableSlots[] _playerWearableSlots;

        public void UpdateMoveAnimation(Vector3 direction)
        {
            _modelAnimator.SetBool("isRunning", direction.magnitude > 0);
        }

        public void UpdatePlayerXDirection(bool isRight)
        {
            _modelAnimator.transform.localScale = (Vector3.right * (isRight ? 1 : -1)) + Vector3.up + Vector3.forward;
        }

        public void ChangeWearable(WearableSlot wearableSlot, Sprite newSprite)
        {
            foreach (var slot in _playerWearableSlots)
            {
                if (slot.wearableSlot == wearableSlot)
                    slot.spriteRenderer.sprite = newSprite;
            }
        }
    }

    [System.Serializable]
    public struct PlayerWearableSlots
    {
        public WearableSlot wearableSlot;
        public SpriteRenderer spriteRenderer;
    }
}