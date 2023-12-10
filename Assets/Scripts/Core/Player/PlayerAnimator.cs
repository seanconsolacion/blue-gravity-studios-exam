using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _modelAnimator;

        public void UpdateMoveAnimation(Vector3 direction)
        {
            _modelAnimator.SetBool("isRunning", direction.magnitude > 0);
        }

        public void UpdatePlayerXDirection(bool isRight)
        {
            _modelAnimator.transform.localScale = (Vector3.right * (isRight ? 1 : -1)) + Vector3.up + Vector3.forward;
        }
    }
}