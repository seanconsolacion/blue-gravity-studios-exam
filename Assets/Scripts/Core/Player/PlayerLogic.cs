using Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerController), typeof(PlayerAnimator), typeof(Rigidbody2D))]
    public class PlayerLogic : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 5;
        [SerializeField] private PlayerAnimator _playerAnimator;
        private PlayerController _playerController;
        private Rigidbody2D _rigidBody;

        private Vector3 _currentDirection;
        private IInteractable _currentInteractable;

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
            _rigidBody.MovePosition(transform.position + (_movementSpeed * Time.fixedDeltaTime * direction));
            _playerAnimator.UpdateMoveAnimation(direction);

            if (direction.magnitude > 0)
            {
                _currentDirection = direction;
                _playerAnimator.UpdatePlayerXDirection(direction.x > 0);
            }
        }

        public void Interact()
        {
            if (_currentInteractable == null)
                return;

            _currentInteractable.Interact();
        }

        private void CheckForInteractables()
        {
            Debug.DrawRay(transform.position, _currentDirection * 5f, Color.yellow);

            RaycastHit2D hit;
            if (hit = Physics2D.Raycast(transform.position, _currentDirection, 5f))
            {
                Debug.Log("Found someone!");

                if (hit.transform.TryGetComponent(out IInteractable interactable) && _currentInteractable != interactable)
                    _currentInteractable = interactable;
            }
            else
            {
                _currentInteractable = null;
            }
        }

    }
}