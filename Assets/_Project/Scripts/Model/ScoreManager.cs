namespace Assets._Project.Scripts
{
    public class ScoreManager
    {
        private readonly int moveCost;

        public ScoreManager(int moveCost)
        {
            this.moveCost = moveCost;
        }

        public int CalculateScore(int matches, int moves)
        {
            return (matches > 0) ? matches - (moveCost + moves) : -moveCost;
        }

        public bool IsGameOver(int currentScore)
        {
            return currentScore <= 0;
        }
    }
}
