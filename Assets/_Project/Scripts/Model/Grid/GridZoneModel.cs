using Assets._project.Config;
using UnityEngine;

namespace Assets._project.CodeBase
{
    //Рассчитывает положение ячеек сетки на основе конфигурации сетки (начальное положение, размер ячейки, количество строк/столбцов). Поддерживает менеджер сетки, предоставляя позиции ячеек для макета сетки.
    public class GridZoneModel
    {
        private ManagerData _managerData;
        private Vector3 _startPosition;

        public GridZoneModel(ManagerData managerData)
        {
            _managerData = managerData;
            _startPosition = managerData.StartPosition;
        }

        //Вычисляет положение определенной ячейки в сетке на основе ее строки и столбца.Он начинается с определения начального положения сетки(_startPosition), а затем вычисляет положение каждой ячейки на основе строки и столбца. Формула вычисляет позиции xи yпутем корректировки начальной позиции(_startPosition) с соответствующим смещением на основе строки и столбца ячейки.
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
