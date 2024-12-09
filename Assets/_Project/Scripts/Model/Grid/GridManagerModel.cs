using Assets._project.Config;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._project.CodeBase
{
    //Управляет ячейками сетки и их доступностью (свободны или заняты). Может находить ближайшую свободную ячейку и управлять размещением элементов в сетке. Координирует работу с другими менеджерами по заполнению сетки.
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

            FindCells();
        }

        //Принимает текущую позицию и список свободных ячеек. Находит ближайшую свободную ячейку, сравнивая квадрат расстояния до каждой свободной ячейки. Возвращает ближайшую свободную ячейку.
        public Point FindNearestFreeCell(Vector3 position, List<Point> freeCells)
        {
            float minDistance = float.MaxValue;
            Point nearestCell = null;

            foreach (var cell in freeCells)
            {
                float distance = Vector3.SqrMagnitude(position - cell.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestCell = cell;
                }
            }

            return nearestCell;
        }

        //Проходит по всем ячейкам и проверяет, «свободны» ли они (т.е. не заняты). Возвращает список всех свободных ячеек.
        public List<Point> GetFreeCells()
        {
            List<Point> freeCells = new List<Point>();

            foreach (var cell in _cells)
            {
                if (!cell.IsBusy)
                {
                    freeCells.Add(cell);
                }
            }

            return freeCells;
        }

        //Инициализирует сетку, задавая положение каждой ячейки в сетке. Он проходит по строкам и столбцам, помещая ячейки в вычисленные позиции и назначая индексы строк и столбцов.
        private void FindCells()
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
                    _cells[index].SetInfoPositionPoint(row, col);
                    index++;
                }
            }
        }
    }
}
