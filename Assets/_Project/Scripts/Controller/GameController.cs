using Assets._project.CodeBase;
using Assets._project.Config;
using System.Collections.Generic;
using System.Linq;
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
            _itemManager = new ItemManagerModel(_items);
            _gridManagerModel = new GridManagerModel(_itemManager, _gameConfig.ManagerData, _cells);
            _playerModel = new PlayerModel(_gameConfig.PlayerData.StartCountScore, _gameView);

            _gameView.Initialize(_gameConfig.ManagerData);
            _gameView.InitializeGrid(_cells, _itemManager, _gameConfig.ManagerData.CellSize);
            _playerInput.OnItemClicked += HandleItemClick; 
        }

        private void HandleItemClick(Item clickedItem)
        {
            List<Item> connectedItemsY = GetConnectedItems(clickedItem, Direction.Vertical);

            if (connectedItemsY.Count >= 3)
            {
                DeactivateItems(connectedItemsY);
                return; 
            }

            List<Item> connectedItemsX = GetConnectedItems(clickedItem, Direction.Horizontal);

            if (connectedItemsX.Count >= 3)
                DeactivateItems(connectedItemsX);
        }

        private void HandleItemClicks(Item clickedItem)
        {
            List<Item> itemsOnSameY = _itemManager.GetItemsOnSameY(clickedItem);

            List<Item> itemsOnSameX = _itemManager.GetItemsOnSameX(clickedItem);

            List<Item> matchingItemsY = FilterNeighboringItems(clickedItem, itemsOnSameY);
            List<Item> matchingItemsX = FilterNeighboringItems(clickedItem, itemsOnSameX);

            if (matchingItemsY.Count > 0)
                DeactivateItems(matchingItemsY);
            
            if (matchingItemsX.Count > 0)
                DeactivateItems(matchingItemsX);
        }

        private List<Item> FilterNeighboringItems(Item clickedItem, List<Item> itemsOnSame)
        {
            List<Item> matchingItems = new List<Item>();

            foreach (var item in itemsOnSame)
            {
                if (item.TypeItem == clickedItem.TypeItem)
                {
                    if (Vector3.Distance(item.ItemPosition, clickedItem.ItemPosition) <= _gameConfig.ManagerData.CellSize)
                        matchingItems.Add(item);
                }
            }

            if (matchingItems.Count < 3)
                return new List<Item>(); 

            return matchingItems;
        }

        private List<Item> GetConnectedItems(Item clickedItem, Direction direction, HashSet<Item> visitedItems = null)
        {
            if (visitedItems == null)
                visitedItems = new HashSet<Item>();

            if (visitedItems.Contains(clickedItem))
                return new List<Item>();

            visitedItems.Add(clickedItem);

            List<Item> connectedItems = new List<Item> { clickedItem }; // Текущий предмет

            // Получаем соседей только по указанному направлению
            List<Item> neighbors = (direction == Direction.Vertical)
                ? _itemManager.GetItemsOnSameY(clickedItem)
                    .Where(item => !visitedItems.Contains(item) &&
                                   Vector3.Distance(item.ItemPosition, clickedItem.ItemPosition) <= _gameConfig.ManagerData.CellSize &&
                                   item.TypeItem == clickedItem.TypeItem).ToList()
                : _itemManager.GetItemsOnSameX(clickedItem)
                    .Where(item => !visitedItems.Contains(item) &&
                                   Vector3.Distance(item.ItemPosition, clickedItem.ItemPosition) <= _gameConfig.ManagerData.CellSize &&
                                   item.TypeItem == clickedItem.TypeItem).ToList();

            // Рекурсивно добавляем соседей
            foreach (var neighbor in neighbors)
            {
                connectedItems.AddRange(GetConnectedItems(neighbor, direction, visitedItems));
            }

            return connectedItems.Distinct().ToList(); // Убираем дубли
        }

        private void DeactivateItems(List<Item> items)
        {
            foreach (var item in items)
            {
                _itemManager.ReplaceItem(item);
                _gameView.RemoveItem(item);
                _playerModel.UpdateScore(1);
            }
        }
    }

    public enum Direction
    {
        Vertical,
        Horizontal
    }
}
