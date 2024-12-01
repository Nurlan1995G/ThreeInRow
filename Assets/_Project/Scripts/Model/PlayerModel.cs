namespace Assets._project.CodeBase
{
    public class PlayerModel
    {
        private int _score;
        private GameView _gameView;

        public int Score => _score;

        public PlayerModel(int score, GameView gameView)
        {
            _score = score;
            _gameView = gameView;

            UpdateScore(score);
        }

        public void UpdateScore(int score)
        {
            _score += score;
            _gameView.UpdateScore(this);
        }
    }
}
