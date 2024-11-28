using TMPro;

namespace Assets._project.CodeBase
{
    public class PlayerUI
    {
        private TextMeshProUGUI _scoreText;

        public PlayerUI(TextMeshProUGUI scoreText)
        {
            _scoreText = scoreText;
        }

        public void UpdateScoreTextUI(int score) =>
            _scoreText.text = score.ToString();
    }
}
