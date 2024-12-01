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

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _itemManager = new ItemManagerModel(_items, _gameConfig.ManagerData.StartPosition);
            _gridManagerModel = new GridManagerModel(_itemManager, _gameConfig.ManagerData, _cells);

            _gameView.Initialize(_gameConfig.ManagerData);
            _gameView.InitializeGrid(_cells, _itemManager, _gameConfig.ManagerData.CellSize);
            _playerInput.OnItemClicked += HandleItemClick; 
        }

        private void HandleItemClick(Item clickedItem)
        {
            var matchingItemsX = _itemManager.GetItemsInRow(clickedItem);
            var matchingItemsY = _itemManager.GetItemsInColumn(clickedItem);

            List<Item> matchedItems = new List<Item>();

            // Сравнение по строке
            matchedItems.AddRange(matchingItemsX.FindAll(item => item.TypeItem == clickedItem.TypeItem));

            // Если совпадения не найдены по строке, проверяем по столбцу
            if (matchedItems.Count < 3)
            {
                matchedItems.Clear(); // Очищаем предыдущий список
                matchedItems.AddRange(matchingItemsY.FindAll(item => item.TypeItem == clickedItem.TypeItem));
            }

            // Если найдены совпадения (больше или равно 3), деактивируем предметы
            if (matchedItems.Count >= 3)
            {
                DeactivateItems(matchedItems);
            }
            else
            {
                Debug.Log("Недостаточно совпадений.");
            }
        }


        private List<Item> FindMatchingItems(List<Item> items, TypeItem type)
        {
            List<Item> matchingItems = new List<Item>();

            foreach (var item in items)
            {
                if (item.TypeItem == type)
                {
                    matchingItems.Add(item);
                }
            }

            return matchingItems;
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
