using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets._project.CodeBase
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private Camera _mainCamera;

        public event Action<ItemModel> OnItemClicked;

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _mainCamera = Camera.main;

            _playerInput.Clicked.MouseTap.performed -= OnClick;
            _playerInput.Clicked.MouseTap.performed += OnClick;
        }

        private void OnEnable() => 
            _playerInput.Enable();

        private void OnDisable()
        {
            _playerInput.Clicked.MouseTap.performed -= OnClick; 
            _playerInput.Disable();
        }

        private void OnClick(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            Collider2D hit = Physics2D.OverlapPoint(mousePosition);

            if (hit != null && hit.TryGetComponent(out Item item))
                OnItemClicked?.Invoke(item.ItemModel);
        }
    }
}
