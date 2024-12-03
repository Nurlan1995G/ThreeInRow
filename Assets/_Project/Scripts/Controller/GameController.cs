using Assets._project.CodeBase;
using Assets._project.Config;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Scripts.Controller
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private List<Point> _cells; 
        [SerializeField] private List<Item> _items;   
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
            _gameView.InitializeGrid(_cells, _itemManager);
            _playerInput.OnItemClicked += HandleItemClick; 
        }

        private void HandleItemClick(Item clickedItem)
        {
            foreach (var item in _items)
            {
                ItemModel itemModel = new ItemModel(item);
            }

            _playerModel.SubstractScore(_gameConfig.LogicData.SubstractScore);

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

        private List<Item> GetConnectedItems(Item clickedItem, Direction direction, HashSet<Item> visitedItems = null)
        {
            if (visitedItems == null)
                visitedItems = new HashSet<Item>();

            if (visitedItems.Contains(clickedItem))
                return new List<Item>();

            visitedItems.Add(clickedItem);

            List<Item> connectedItems = new List<Item> { clickedItem };

            List<Item> neighbors = (direction == Direction.Vertical)
                ? _itemManager.GetItemsOnSameY(clickedItem)
                    .Where(item => !visitedItems.Contains(item) &&
                                   Vector3.Distance(item.ItemPosition, clickedItem.ItemPosition) <= _gameConfig.ManagerData.CellSize &&
                                   item.TypeItem == clickedItem.TypeItem).ToList()
                : _itemManager.GetItemsOnSameX(clickedItem)
                    .Where(item => !visitedItems.Contains(item) &&
                                   Vector3.Distance(item.ItemPosition, clickedItem.ItemPosition) <= _gameConfig.ManagerData.CellSize &&
                                   item.TypeItem == clickedItem.TypeItem).ToList();

            foreach (var neighbor in neighbors)
                connectedItems.AddRange(GetConnectedItems(neighbor, direction, visitedItems));

            return connectedItems.Distinct().ToList();
        }

        private void DeactivateItems(List<Item> items)
        {
            foreach (var item in items)
            {
                _itemManager.ReplaceItem(item);
                _gameView.RemoveItem(item);
                _playerModel.UpdateScore(_gameConfig.LogicData.Reward);
            }

            var getAllItems = _itemManager.OnItemsMatched();
            StartCoroutine(HandleFallingItems(getAllItems));
        }

        private IEnumerator HandleFallingItems(List<Item> getAllItems)
        {
            // Сброс ограничений для всех объектов
            foreach (var item in getAllItems)
            {
                item.Rigidbody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            }

            yield return new WaitForSeconds(_gameConfig.LogicData.DropDuration);

            // Оптимизация поиска ближайших ячеек
            var freeCells = _gridManagerModel.GetFreeCells();

            foreach (var item in getAllItems)
            {
                item.Rigidbody2D.constraints |= RigidbodyConstraints2D.FreezePositionY;

                Point nearestCell = _gridManagerModel.FindNearestFreeCell(item.transform.position, freeCells);

                if (nearestCell != null)
                {
                    item.SetCurrentPoint(nearestCell);
                    item.SetPosition(nearestCell.transform.position);
                    nearestCell.MarkAsBusy();
                }
            }
        }
    }

    public enum Direction
    {
        Vertical,
        Horizontal
    }
}
