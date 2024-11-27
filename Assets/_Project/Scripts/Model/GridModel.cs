using System;
using System.Collections.Generic;

namespace Assets._Project.Scripts
{
    public class GridModel
    {
        private readonly int[,] gridData;
        private readonly int gridWidth;
        private readonly int gridHeight;

        public GridModel(int gridWidth, int gridHeight)
        {
            this.gridWidth = gridWidth;
            this.gridHeight = gridHeight;
            gridData = new int[gridWidth, gridHeight];
        }

        public int[,] GetGridData() => gridData;

        public void InitializeGrid(Func<int> randomElementGenerator)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    gridData[x, y] = randomElementGenerator();
                }
            }
        }

        public List<(int x, int y)> GetMatches(int startX, int startY)
        {
            var matches = new List<(int, int)>();
            int targetColor = gridData[startX, startY];

            if (targetColor == 0) return matches;

            var visited = new bool[gridWidth, gridHeight];
            var queue = new Queue<(int x, int y)>();
            queue.Enqueue((startX, startY));

            while (queue.Count > 0)
            {
                var (x, y) = queue.Dequeue();
                if (x < 0 || x >= gridWidth || y < 0 || y >= gridHeight || visited[x, y] || gridData[x, y] != targetColor)
                    continue;

                matches.Add((x, y));
                visited[x, y] = true;

                queue.Enqueue((x + 1, y));
                queue.Enqueue((x - 1, y));
                queue.Enqueue((x, y + 1));
                queue.Enqueue((x, y - 1));
            }

            return matches;
        }

        public void RemoveMatches(List<(int x, int y)> matches)
        {
            foreach (var (x, y) in matches)
            {
                gridData[x, y] = 0;
            }
        }

        public void CollapseGrid(Func<int> randomElementGenerator)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = gridHeight - 1; y >= 0; y--)
                {
                    if (gridData[x, y] == 0)
                    {
                        for (int k = y - 1; k >= 0; k--)
                        {
                            if (gridData[x, k] != 0)
                            {
                                gridData[x, y] = gridData[x, k];
                                gridData[x, k] = 0;
                                break;
                            }
                        }

                        if (gridData[x, y] == 0)
                        {
                            gridData[x, y] = randomElementGenerator();
                        }
                    }
                }
            }
        }
    }
}
