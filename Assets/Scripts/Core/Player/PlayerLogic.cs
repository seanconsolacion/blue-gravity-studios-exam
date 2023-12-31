using HUD;
using Inventory;
using Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerController), typeof(PlayerVisual), typeof(Rigidbody2D))]
    public class PlayerLogic : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 5;
        [SerializeField] private PlayerVisual _playerVisual;
        private PlayerController _playerController;
        private Rigidbody2D _rigidBody;
        private Vector3 _currentDirection;
        private IInteractable _currentInteractable;

        public PlayerVisual PlayerVisual => _playerVisual;
        public PlayerController PlayerController => _playerController;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            HandleInput();
        }

        private void FixedUpdate()
        {
            CheckForInteractables();
        }

        private void Initialize()
        {
            _playerController = GetComponent<PlayerController>();
            _rigidBody = GetComponent<Rigidbody2D>();

            _playerController.IntitializeController(this);
        }

        private void HandleInput()
        {
            _playerController.Tick();
        }

        public void Move(Vector3 direction)
        {
            if (direction.magnitude > 0)
                _currentDirection = direction;

            if (direction.x != 0)
                _playerVisual.UpdatePlayerXDirection(_currentDirection.x > 0);

            _rigidBody.MovePosition(transform.position + (_movementSpeed * Time.fixedDeltaTime * direction));
            _playerVisual.UpdateMoveAnimation(direction);
        }

        public void Interact()
        {
            if (_currentInteractable == null)
                return;

            _currentInteractable.Interact();
        }

        public void OpenInventory()
        {
            InventoryManager.Singleton.OpenInventoryUI();
        }

        private void CheckForInteractables()
        {
            Debug.DrawRay(transform.position, _currentDirection * 5f, Color.yellow);

            RaycastHit2D hit;
            if (hit = Physics2D.Raycast(transform.position, _currentDirection, 5f))
            {
                if (hit.transform.TryGetComponent(out IInteractable interactable))
                    _currentInteractable = interactable;
                else
                    _currentInteractable = null;
            }
            else
                _currentInteractable = null;

            HUDManager.Singleton.ToggleInteractPrompt(_currentInteractable != null && PlayerController.IsActivated);

        }
    }
}