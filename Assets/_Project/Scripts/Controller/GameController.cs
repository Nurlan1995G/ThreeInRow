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
        [SerializeField] private PlayerInputHandler _playerInput;

        private ItemManagerModel _itemManager;
        private GridManagerModel _gridManagerModel;
        private PlayerModel _playerModel;
        private List<ItemModel> _itemModels;
        private ItemAnimatorModel _animatorModel;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            InitializeItemModels();

            _itemManager = new ItemManagerModel(_itemModels);
            _gridManagerModel = new GridManagerModel(_itemManager, _gameConfig.ManagerData, _cells);
            _playerModel = new PlayerModel(_gameConfig.PlayerData.StartCountScore, _gameView);

            _gameView.Initialize(_gameConfig.ManagerData);
            _gameView.InitializeGrid(_cells, _itemManager);
            _playerInput.OnItemClicked += HandleItemClick;
        }

        private void InitializeItemModels()
        {
            _itemModels = new List<ItemModel>();

            foreach (Item item in _items)
            {
                ItemModel itemModel = new ItemModel(item);
                item.Initialize(itemModel);
                _itemModels.Add(itemModel);
            }
        }

        private void HandleItemClick(ItemModel clickedItem)
        {
            _playerModel.SubstractScore(_gameConfig.LogicData.SubstractScore);

            var (row, column) = clickedItem.GetCurrentPointData();

            List<ItemModel> connectedItemsY = GetConnectedItems(clickedItem, Direction.Vertical);

            if (connectedItemsY.Count >= 3)
            {
                DeactivateItems(connectedItemsY, row, column); 
                return;
            }

            List<ItemModel> connectedItemsX = GetConnectedItems(clickedItem, Direction.Horizontal);

            if (connectedItemsX.Count >= 3)
                DeactivateItems(connectedItemsX, row, column); 
        }


        private List<ItemModel> GetConnectedItems(ItemModel clickedItem, Direction direction, HashSet<ItemModel> visitedItems = null)
        {
            if (visitedItems == null)
                visitedItems = new HashSet<ItemModel>();

            if (visitedItems.Contains(clickedItem))
                return new List<ItemModel>();

            visitedItems.Add(clickedItem);

            List<ItemModel> connectedItems = new List<ItemModel> { clickedItem };

            List<ItemModel> neighbors = (direction == Direction.Vertical)
                ? _itemManager.GetItemsOnSameY(clickedItem)
                    .Where(itemModel => !visitedItems.Contains(itemModel) &&
                                   Vector3.Distance(itemModel.Position, clickedItem.Position) <= _gameConfig.ManagerData.CellSize &&
                                   itemModel.Item.TypeItem == clickedItem.Item.TypeItem).ToList()
                : _itemManager.GetItemsOnSameX(clickedItem)
                    .Where(item => !visitedItems.Contains(item) &&
                                   Vector3.Distance(item.Position, clickedItem.Position) <= _gameConfig.ManagerData.CellSize &&
                                   item.Item.TypeItem == clickedItem.Item.TypeItem).ToList();

            foreach (var neighbor in neighbors)
                connectedItems.AddRange(GetConnectedItems(neighbor, direction, visitedItems));

            return connectedItems.Distinct().ToList();
        }

        private void DeactivateItems(List<ItemModel> items, int row, int column)
        {
            foreach (var item in items)
                DeactivateItemWithAnimation(item);

            MoveItemsInColumn(row, column);

            List<ItemModel> getAllItems = _itemManager.OnItemsMatched();
            StartCoroutine(HandleFallingItems(getAllItems));
        }

        private void MoveItemsInColumn(int row, int column)
        {
            var itemsInColumn = _itemManager.GetAllItems()
                .Where(item => item.GetCurrentPoint() != null && item.GetCurrentPoint().GetInfoPositionPoint().column == column)
                .OrderBy(item => item.GetCurrentPoint().GetInfoPositionPoint().row)
                .ToList();

            for (int i = 0; i < itemsInColumn.Count - 1; i++)
            {
                var currentItem = itemsInColumn[i];
                var nextItem = itemsInColumn[i + 1];

                currentItem.SetPosition(nextItem.Position);
                currentItem.SetCurrentPoint(nextItem.GetCurrentPoint());

                nextItem.RemoveFromCurrentPoint();
            }

            var lastItem = itemsInColumn.Last();
            lastItem.RemoveFromCurrentPoint();
        }

        private IEnumerator HandleFallingItems(List<ItemModel> matchedItems)
        {
            float minY = matchedItems.Min(item => item.Position.y);

            List<ItemModel> itemsAbove = _itemManager.GetAllItems()
                .Where(item => item.Position.y > minY)
                .ToList();

            foreach (ItemModel item in itemsAbove)
                item.Item.Rigidbody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionY;

            yield return new WaitForSeconds(_gameConfig.LogicData.DropDuration);

            List<Point> freeCells = _gridManagerModel.GetFreeCells();

            foreach (var item in itemsAbove)
            {
                item.Item.Rigidbody2D.constraints |= RigidbodyConstraints2D.FreezePositionY;

                Point nearestCell = _gridManagerModel.FindNearestFreeCell(item.Position, freeCells);

                if (nearestCell != null)
                {
                    item.RemoveFromCurrentPoint();
                    item.SetCurrentPoint(nearestCell);
                    item.SetPosition(nearestCell.transform.position);
                    nearestCell.MarkAsBusy();
                }
            }
        }

        private void DeactivateItemWithAnimation(ItemModel item)
        {
            ItemAnimatorModel animatorModel = new ItemAnimatorModel(item);

            animatorModel.AnimateShrink(_gameConfig.LogicData.ShrinkDuration, () =>
            {
                _itemManager.ReplaceItem(item);
                _gameView.RemoveItem(item);
                _playerModel.UpdateScore(_gameConfig.LogicData.Reward);
            });
        }

        public enum Direction
        {
            Vertical,
            Horizontal
        }
    }
}
