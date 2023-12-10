using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerLogic _player;
        private InputAction _moveAction;
        private InputAction _interactAction;

        public void IntitializeController(PlayerLogic player)
        {
            _player = player;
            var playerInput = PlayerInput.all[0];

            if (playerInput != null)
            {
                _moveAction = playerInput.actions["MovePlayer"];
                _interactAction = playerInput.actions["Interact"];
            }
            else
                Debug.LogError("No player input component found.");
        }

        public void Tick()
        {
            HandleMovement();
            HandleInteraction();
        }

        private void HandleMovement()
        {
            _player.Move(_moveAction.ReadValue<Vector2>().normalized);
        }

        private void HandleInteraction()
        {
            if (_interactAction.WasPerformedThisFrame())
                _player.Interact();
        }
    }
}