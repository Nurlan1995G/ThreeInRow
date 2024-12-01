using Assets._project.Config;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class GridZoneModel
    {
        private ManagerData _managerData;
        private Vector3 _startPosition;

        public GridZoneModel(ManagerData managerData)
        {
            _managerData = managerData;
            _startPosition = managerData.StartPosition;
        }

        public Vector3 GetCellPosition(int row, int col)
        {
            // Определяем начальную позицию для сетки
            Vector3 startPos = new Vector3(
                _startPosition.x - (_managerData.TotalColumns - 1) / 2f * _managerData.CellSize,
                _startPosition.y + (_managerData.TotalRows - 1) / 2f * _managerData.CellSize,
                _startPosition.z);

            // Вычисляем позиции ячеек
            float xPos = startPos.x + col * _managerData.CellSize;
            float yPos = startPos.y - row * _managerData.CellSize;

            return new Vector3(xPos, yPos, _startPosition.z);
        }
    }

}
