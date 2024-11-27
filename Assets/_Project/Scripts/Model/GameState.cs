namespace Assets._Project.Scripts
{
    public class GameState
    {
        public int PlayerScore { get; private set; }
        public int PlayerMoves { get; private set; }
        public int[,] GridData { get; private set; } // Данные игрового поля.

        public GameState(int gridWidth, int gridHeight, int initialScore)
        {
            GridData = new int[gridWidth, gridHeight];
            PlayerScore = initialScore;
            PlayerMoves = 0;
        }

        public void UpdateScore(int points)
        {
            PlayerScore += points;
        }

        public void IncrementMoves()
        {
            PlayerMoves++;
        }

        public void ResetState(int initialScore)
        {
            PlayerScore = initialScore;
            PlayerMoves = 0;
        }
    }
}
