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

        //Он принимает параметр score, который добавляется как к текущему счёту (_currentScore), так и к общему счёту (_totalScore). После изменения очков, метод вызывает два метода обновления: _gameView.UpdateScore(_currentScore) — обновляется отображение текущих очков. _gameView.UpdateMoves(_totalScore) — обновляется отображение общего счёта(можно предположить, что это связано с количеством ходов, если деление на 3 используется для отображения).
        public void UpdateCurrentScore(int score)
        {
            _currentScore += score;
            _totalScore += score;
            _gameView.UpdateScore(_currentScore);
            _gameView.UpdateMoves(_totalScore);
        }

        //Он принимает параметр score, который вычитается из _totalScore.После изменения общего счёта, метод снова вызывает _gameView.UpdateMoves(_totalScore), чтобы обновить отображение общего счёта.
        public void SubtractCurrentScore(int score)
        {
            _totalScore -= score;
            _gameView.UpdateMoves(_totalScore);
        }
    }
}
