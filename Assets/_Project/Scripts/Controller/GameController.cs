﻿using Assets._project.CodeBase;
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

            List<ItemModel> connectedItemsY = GetConnectedItems(clickedItem, Direction.Vertical);

            if (connectedItemsY.Count >= 3)
            {
                DeactivateItems(connectedItemsY);
                return;
            }

            List<ItemModel> connectedItemsX = GetConnectedItems(clickedItem, Direction.Horizontal);

            if (connectedItemsX.Count >= 3)
                DeactivateItems(connectedItemsX);
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
                    .Where(item => !visitedItems.Contains(item) &&
                                   Vector3.Distance(item.ItemPosition, clickedItem.ItemPosition) <= _gameConfig.ManagerData.CellSize &&
                                   item.Item.TypeItem == clickedItem.Item.TypeItem).ToList()
                : _itemManager.GetItemsOnSameX(clickedItem)
                    .Where(item => !visitedItems.Contains(item) &&
                                   Vector3.Distance(item.ItemPosition, clickedItem.ItemPosition) <= _gameConfig.ManagerData.CellSize &&
                                   item.Item.TypeItem == clickedItem.Item.TypeItem).ToList();

            foreach (var neighbor in neighbors)
                connectedItems.AddRange(GetConnectedItems(neighbor, direction, visitedItems));

            return connectedItems.Distinct().ToList();
        }

        private void DeactivateItems(List<ItemModel> items)
        {
            foreach (ItemModel item in items)
            {
                _itemManager.ReplaceItem(item);
                _gameView.RemoveItem(item);
                _playerModel.UpdateScore(_gameConfig.LogicData.Reward);
            }

            var getAllItems = _itemManager.OnItemsMatched();
            StartCoroutine(HandleFallingItems(getAllItems));
        }

        private IEnumerator HandleFallingItems(List<ItemModel> getAllItems)
        {
            // Сброс ограничений для всех объектов
            foreach (var item in getAllItems)
            {
                item.Item.Rigidbody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            }

            yield return new WaitForSeconds(_gameConfig.LogicData.DropDuration);

            // Оптимизация поиска ближайших ячеек
            var freeCells = _gridManagerModel.GetFreeCells();

            foreach (var item in getAllItems)
            {
                item.Item.Rigidbody2D.constraints |= RigidbodyConstraints2D.FreezePositionY;

                Point nearestCell = _gridManagerModel.FindNearestFreeCell(item.ItemPosition, freeCells);

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
