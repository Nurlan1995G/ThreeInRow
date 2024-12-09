using Assets._project.Config;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets._project.CodeBase
{
    //управляет пользовательским интерфейсом (UI), связанным с видом игры, включая отображение счета, ходов и активацию элементов на сетке
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

        //Инициализирует сетку, проверяя каждую ячейку и заполняя ее случайным элементом из менеджера элементов, если ячейка еще не занята
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

        //Деактивирует элемент, удаляет его из пула элементов и сбрасывает его положение в начальное положение
        public void RemoveItem(ItemModel item)
        {
            item.Deactivate();
            _itemsPool.Remove(item);
            item.SetPosition(_managerData.StartPosition);
        }

        //Обновляет отображение результатов в пользовательском интерфейсе
        public void UpdateScore(int score)
        {
            _scoreText.text = $"Score: {score}";
        }

        //Обновляет отображение ходов в пользовательском интерфейсе, деля количество ходов 
        public void UpdateMoves(int moves)
        {
            moves /= 3;

            _movesText.text = $"Ходы: {moves}";
        }

        //Активирует указанный элемент, помещает его в указанную ячейку и отмечает ячейку как занятую. Затем элемент добавляется в пул элементов
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
