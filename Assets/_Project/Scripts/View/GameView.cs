using Assets._project.Config;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _movesText;

        private List<ItemModel> _itemsPool = new List<ItemModel>();
        private ManagerData _managerData;

        public void Initialize(ManagerData managerData)
        {
            _managerData = managerData;
        }

        /// Расставляет предметы на указанных ячейках
        public void InitializeGrid(List<Point> cells, ItemManagerModel itemManager)
        {
            foreach (var cell in cells)
            {
                if (!cell.IsBusy)
                {
                    ItemModel item = itemManager.GetRandomItem();

                    if (item != null)
                        ActivateAndPlaceItem(item, cell);
                }
            }
        }

        public void RemoveItem(ItemModel item)
        {
            item.Deactivate();
            _itemsPool.Remove(item);
            item.SetPosition(_managerData.StartPosition);
        }

        public void UpdateScore(int score)
        {
            _scoreText.text = $"Score: {score}";
        }

        public void UpdateMoves(int moves)
        {
            moves /= 3;

            _movesText.text = $"Ходы: {moves}";
        }

        private void ActivateAndPlaceItem(ItemModel item, Point cell)
        {
            Vector3 itemPosition = cell.GetPlaceItem(item);

            item.Activate();
            item.SetPosition(itemPosition);
            cell.MarkAsBusy();
            _itemsPool.Add(item);
        }
    }
}
