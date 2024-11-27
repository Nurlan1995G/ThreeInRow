using System;
using UnityEngine;

namespace Assets._Project.Scripts
{
    public class GameView : MonoBehaviour
    {
        public Action<int, int> OnCellClicked;
        public Transform GridParent;
        public GridCellView[,] CellViews;

        public void InitializeGrid(int width, int height, Func<GridCellView> createCell)
        {
            CellViews = new GridCellView[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var cell = createCell();
                    cell.transform.SetParent(GridParent);
                    CellViews[x, y] = cell;
                }
            }
        }

        public void UpdateCell(int x, int y, Sprite content)
        {
            CellViews[x, y].SetContent(content);
        }
    }
}
