using Assets._project.CodeBase;
using Assets._project.Config;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.Controller
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private List<Point> _cells; // Загружаются с уровня
        [SerializeField] private List<Item> _items;   // Загружаются с уровня
        [SerializeField] private GameView _gameView;
        [SerializeField] private PlayerInput _playerInput;

        private ItemManagerModel _itemManager;
        private GridManagerModel _gridManagerModel;
        private PlayerModel _playerModel;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _itemManager = new ItemManagerModel(_items, _gameConfig.ManagerData.StartPosition);
            _gridManagerModel = new GridManagerModel(_itemManager, _gameConfig.ManagerData, _cells);
            _playerModel = new PlayerModel(10, _gameView);

            _gameView.Initialize(_gameConfig.ManagerData);
            _gameView.InitializeGrid(_cells, _itemManager, _gameConfig.ManagerData.CellSize);
            _playerInput.OnItemClicked += HandleItemClick; 
        }

        private void HandleItemClick(Item clickedItem)
        {
            Debug.Log("HandleItemClick");

            clickedItem.ActivateCollisionDetection();

            // Подписываемся на событие для получения соприкосновений
            clickedItem.OnCollisionDetected += items =>
            {
                List<Item> matchingItems = _itemManager.FilterMatchingItems(clickedItem, items);

                if (matchingItems.Count >= 1) // Если есть хотя бы одно совпадение
                {
                    matchingItems.Add(clickedItem);
                    DeactivateItems(matchingItems);
                    _playerModel.UpdateScore(matchingItems.Count);

                    Debug.Log($"Удалено {matchingItems.Count} предметов!");
                }
                else
                {
                    Debug.Log("Совпадений не найдено.");
                }

                clickedItem.DeactivateCollisionDetection(); // Отключаем проверку после обработки
            };
    }

        private void DeactivateItems(List<Item> items)
        {
            foreach (var item in items)
            {
                _itemManager.ReplaceItem(item);
                _gameView.RemoveItem(item);
            }
        }
    }
}
