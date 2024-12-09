﻿using Assets._project.CodeBase;
using Assets._project.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Scripts.Controller
{
    public class ItemClickHandler
    {
        private readonly ItemManagerModel _itemManager;
        private readonly GameConfig _gameConfig;
        private readonly PlayerModel _playerModel;
        private readonly Action<List<ItemModel>> _onMatchFound;
        private readonly Action _onGameEnd;

        public ItemClickHandler(ItemManagerModel itemManager, GameConfig gameConfig, PlayerModel playerModel, Action<List<ItemModel>> onMatchFound, Action onGameEnd)
        {
            _itemManager = itemManager;
            _gameConfig = gameConfig;
            _playerModel = playerModel;
            _onMatchFound = onMatchFound;
            _onGameEnd = onGameEnd;
        }

        //Обрабатывает щелчки по предметам, проверяя, достаточно ли у игрока очков, чтобы сделать ход. Вычитает баллы при нажатии на элемент.Проверяет наличие связанных элементов в вертикальном и горизонтальном направлениях (используя GetConnectedItems).Если найдено совпадение из 3 или более элементов, это запускает onMatchFoundдействие.
        public void HandleClick(ItemModel clickedItem)
        {
            Debug.Log("Totalscore - HandleClick - " + _playerModel.TotalScore);

            if (_playerModel.TotalScore <= 0)
            {
                _onGameEnd?.Invoke();
                return;
            }

            _playerModel.SubtractCurrentScore(3);

            List<ItemModel> connectedItemsY = GetConnectedItems(clickedItem, Direction.Vertical);

            if (connectedItemsY.Count >= 3)
            {
                _onMatchFound.Invoke(connectedItemsY);
                return;
            }

            List<ItemModel> connectedItemsX = GetConnectedItems(clickedItem, Direction.Horizontal);

            if (connectedItemsX.Count >= 3)
                _onMatchFound.Invoke(connectedItemsX);
        }

        //Рекурсивно находит все связанные элементы одного типа в указанном направлении (вертикальном или горизонтальном). Использует поиск в глубину для нахождения всех соответствующих элементов, гарантируя, что ни один элемент не будет повторно просмотрен во время поиска.
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
    }
}
