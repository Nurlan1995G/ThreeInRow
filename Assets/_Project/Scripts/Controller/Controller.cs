using Assets._project.CodeBase;
using Assets._project.Config;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets._Project.Scripts.Controller
{
    public abstract class Controller : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private List<Point> _cells;
        [SerializeField] private List<Item> _items;
        [SerializeField] private GameView _gameView;
        [SerializeField] private PlayerInputHandler _playerInput;

        private ItemManagerModel _itemManager;
        private GridManagerModel _gridManagerModel;
        private PlayerModel _playerModel;
        private List<ItemModel> _itemModels;
    }

    public class AnimatorController : Controller
    {
        private ItemAnimatorModel _animatorModel;
    }

    public class PlayerInputController : Controller
    {
        private PlayerInput _playerInput;
        private Camera _mainCamera;

        public event Action<ItemModel> OnItemClicked;

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _mainCamera = Camera.main;

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
