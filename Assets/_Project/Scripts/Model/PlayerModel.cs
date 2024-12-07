namespace Assets._project.CodeBase
{
    public class PlayerModel
    {
        private GameView _gameView;
        private int _score;

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
            _gameView.UpdateScore(_score);
        }

        public void SubstractScore(int score)
        {
            _score -= score;
            _gameView.UpdateScore(_score);
        }
    }
}
