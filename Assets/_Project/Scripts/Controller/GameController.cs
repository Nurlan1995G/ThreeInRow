using UnityEngine;

namespace Assets._Project.Scripts
{
    public class GameController
    {
        private readonly GameState gameState;
        private readonly GridModel gridModel;
        private readonly GameView view;
        private readonly ScoreManager scoreManager;

        public GameController(GameState gameState, GridModel gridModel, GameView view, ScoreManager scoreManager)
        {
            this.gameState = gameState;
            this.gridModel = gridModel;
            this.view = view;
            this.scoreManager = scoreManager;

            view.OnCellClicked += HandleCellClick;
            InitializeGame();
        }

        private void InitializeGame()
        {
            gridModel.InitializeGrid(() => Random.Range(1, 5));
            UpdateView();
        }

        private void HandleCellClick(int x, int y)
        {
            var matches = gridModel.GetMatches(x, y);
            if (matches.Count > 0)
            {
                gridModel.RemoveMatches(matches);
                gridModel.CollapseGrid(() => Random.Range(1, 5));

                int gainedScore = scoreManager.CalculateScore(matches.Count, gameState.PlayerMoves);
                gameState.UpdateScore(gainedScore);
                gameState.IncrementMoves();

                UpdateView();
            }

            if (scoreManager.IsGameOver(gameState.PlayerScore))
            {
                EndGame();
            }
        }

        private void UpdateView()
        {
            var gridData = gridModel.GetGridData();
            for (int x = 0; x < gridData.GetLength(0); x++)
            {
                for (int y = 0; y < gridData.GetLength(1); y++)
                {
                    view.UpdateCell(x, y, GetSpriteForValue(gridData[x, y]));
                }
            }
        }

        private Sprite GetSpriteForValue(int value) => /* Заглушка для получения спрайта по значению */ null;

        private void EndGame()
        {
            // Обработка завершения игры.
        }
    }
}
