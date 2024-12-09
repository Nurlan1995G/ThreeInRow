using UnityEngine;

namespace Assets._project.CodeBase
{
    public class PlayerModel
    {
        private GameView _gameView;
        private int _currentScore; 
        private int _totalScore;   

        public int CurrentScore => _currentScore;
        public int TotalScore => _totalScore;

        public PlayerModel(int startScore, GameView gameView)
        {
            _currentScore = startScore;
            _totalScore = startScore;
            _gameView = gameView;

            UpdateCurrentScore(0);
        }

        public void UpdateCurrentScore(int score)
        {
            _currentScore += score;
            _totalScore += score;
            _gameView.UpdateScore(_currentScore);
            _gameView.UpdateMoves(_totalScore);
        }

        public void SubtractCurrentScore(int score)
        {
            _totalScore -= score;
            Debug.Log(_totalScore + " - totalScores");
            _gameView.UpdateMoves(_totalScore);
        }
    }
}
