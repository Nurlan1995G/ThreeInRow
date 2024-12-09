using Assets._project.CodeBase;
using Assets._project.Config;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.Controller
{
    public class ItemDropHandler
    {
        private readonly GridManagerModel _gridManagerModel;
        private readonly GameConfig _gameConfig;

        public ItemDropHandler(GridManagerModel gridManagerModel, GameConfig gameConfig)
        {
            _gridManagerModel = gridManagerModel;
            _gameConfig = gameConfig;
        }

        //Устраняет падение предметов после того, как они сопоставлены и удалены. Делает предметы динамическими(некинематическими), позволяя им падать. Ожидает в течение указанного времени(от DropDuration) gameConfig, а затем перемещает элементы в ближайшую доступную свободную ячейку. Возвращает элементы в кинематический режим, чтобы остановить их движение после того, как они попадут в ячейку.
        public IEnumerator HandleFalling(List<ItemModel> items)
        {
            foreach (var item in items)
                item.SetDynamic();

            yield return new WaitForSeconds(_gameConfig.LogicData.DropDuration);

            var freeCells = _gridManagerModel.GetFreeCells();

            foreach (var item in items)
            {
                Point nearestCell = _gridManagerModel.FindNearestFreeCell(item.ItemPosition, freeCells);

                if (nearestCell != null)
                {
                    item.SetCurrentPoint(nearestCell);
                    item.SetPosition(nearestCell.transform.position);
                    nearestCell.MarkAsBusy();
                }

                item.SetKinematic();
            }
        }
    }
}
