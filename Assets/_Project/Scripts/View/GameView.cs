using Assets._project.Config;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _gameOverText;

        private List<Item> _itemsPool = new List<Item>();
        private ManagerData _managerData;

        public void Initialize(ManagerData managerData)
        {
            _managerData = managerData;
        }

        /// Расставляет предметы на указанных ячейках
        public void InitializeGrid(List<Point> cells, ItemManagerModel itemManager, float cellSize)
        {
            foreach (var cell in cells)
            {
                if (!cell.IsBusy)
                {
                    Item item = itemManager.GetRandomItem();

                    if (item != null)
                        ActivateAndPlaceItem(item, cell);
                }
            }
        }

        public void RemoveItem(Item item)
        {
            item.Deactivate();
            item.SetPosition(_managerData.StartPosition);
        }

        public void UpdateScore(PlayerModel itemModel)
        {
            _scoreText.text = $"Score: {itemModel.Score}";
        }

        public void ShowGameOver(int finalScore)
        {
            _gameOverText.text = $"Game Over! Final Score: {finalScore}";
            _gameOverText.gameObject.SetActive(true);
        }

        public Item GetItemFromPool()
        {
            foreach (var item in _itemsPool)
            {
                if (!item.gameObject.activeSelf)
                {
                    item.Activate();
                    return item;
                }
            }
            return null;
        }

        private void ActivateAndPlaceItem(Item item, Point cell)
        {
            Vector3 itemPosition = cell.GetPlaceItem(item);

            item.Activate();
            item.SetCurrentPoint(cell);
            item.SetPosition(itemPosition);
            cell.MarkAsBusy();
            _itemsPool.Add(item);
        }

        public void RemoveItems(List<Item> matchingItems)
        {
            foreach (var item in matchingItems)
            {
                item.Deactivate();
                item.transform.position = _managerData.StartPosition;
            }
        }

        /* public void FillGridWithItems()  //Заполните Сетку Элементами
         {
             for (int i = 0; i < _cells.Count; i++)
             {
                 if (i < _managerData.TotalItemsToLoad && !_cells[i].IsBusy)
                 {
                     Item item = _itemManager.GetRandomItem();

                     if (item != null)
                         _cells[i].GetPlaceItem(item);
                 }
             }
         }*/
    }
}
