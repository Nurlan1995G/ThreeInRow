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

            FindCells();
        }

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
                    index++;
                }
            }
        }
    }
}
