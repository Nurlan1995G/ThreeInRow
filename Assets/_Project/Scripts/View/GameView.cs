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

        /// Расставляет предметы на указанных ячейках
        public void InitializeGrid(List<Point> cells, ItemManagerModel itemManager, float cellSize)
        {
            foreach (var cell in cells)
            {
                if (!cell.IsBusy)
                {
                    Item item = itemManager.GetRandomItem();

                    if (item != null)
                        // Активируем и размещаем предмет
                        ActivateAndPlaceItem(item, cell);
                }
            }
        }

        public void UpdateScore(int score)
        {
            _scoreText.text = $"Score: {score}";
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
                    item.gameObject.SetActive(true);
                    return item;
                }
            }
            return null;
        }

        public void ReturnItemToPool(Item item)
        {
            item.gameObject.SetActive(false);
        }

        private void ActivateAndPlaceItem(Item item, Point cell)
        {
            item.gameObject.SetActive(true); 
            Vector3 itemPosition = cell.GetPlaceItem(item); 

            if (itemPosition == Vector3.zero)
            {
                Debug.LogWarning($"Не удалось разместить предмет '{item.name}' в ячейке '{cell.name}': ячейка занята или некорректное состояние.");
                return; 
            }

            item.transform.localPosition = itemPosition; // Ставим на позицию ячейки
            cell.MarkAsBusy(); // Помечаем ячейку как занятую
            _itemsPool.Add(item); // Добавляем предмет в пул
        }

    }
}
