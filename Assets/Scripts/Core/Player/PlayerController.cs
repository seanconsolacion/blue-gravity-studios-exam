using Mono.Cecil.Cil;
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
        private bool _isActivated;

        public bool IsActivated => _isActivated;

        public void IntitializeController(PlayerLogic player)
        {
            _player = player;
            var playerInput = PlayerInput.all[0];
            _isActivated = true;

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
            if (!_isActivated)
                return;

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

        public void ToggleActivation(bool isActivated)
        {
            _isActivated = isActivated;
        }
    }
}