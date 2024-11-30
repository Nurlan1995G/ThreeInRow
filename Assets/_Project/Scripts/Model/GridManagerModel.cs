using Assets._project.Config;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class GridManagerModel
    {
        private List<Point> _cells;

        private ManagerData _managerData;
        private ItemManagerModel _itemManager;
        private GridZoneModel _gridZone;

        public GridManagerModel(ItemManagerModel itemManager, ManagerData managerData, List<Point> cells)
        {
            _itemManager = itemManager;
            _managerData = managerData;
            _cells = cells;
            _gridZone = new GridZoneModel(_managerData);

            InitializeCells();
            //FillGridWithItems();
        }

        public void PlaceBallAtNearestFreePoint(Item item)  //Поместите Мяч в Ближайшую Свободную Точку
        {
            Point nearestFreePoint = FindNearestFreePoint(item.transform.position);

            /*if (nearestFreePoint != null)
                nearestFreePoint.PlaceItem(item);  */
        }

        private void InitializeCells()
        {
            int index = 0;

            for (int row = 0; row < _managerData.TotalRows; row++)
            {
                for (int col = 0; col < _managerData.TotalColumns; col++)
                {
                    if (index >= _cells.Count)
                        break;

                    Vector3 position = _gridZone.GetCellPosition(row, col);
                    _cells[index].transform.position = position;
                    index++;
                }
            }
        }

        public void FillGridWithItems()  //Заполните Сетку Элементами
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
        }

        private Point FindNearestFreePoint(Vector3 position)  //Найдите ближайшую свободную точку
        {
            Point nearestPoint = null;
            float minDistance = float.MaxValue;

            foreach (Point point in _cells)
            {
                if (!point.IsBusy)
                {
                    float distance = Vector3.Distance(position, point.transform.position);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestPoint = point;
                    }
                }
            }

            return nearestPoint;
        }
    }
}
