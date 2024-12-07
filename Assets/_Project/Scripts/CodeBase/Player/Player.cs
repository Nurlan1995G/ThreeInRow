using Assets._project.Config;
using TMPro;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        private PlayerUI _playerUI;
        private PlayerInputHandler _input;

        private int _score;

        public PlayerData PlayerData { get; private set; }

        public void Construct(PlayerData playerData, PlayerInputHandler input, ParticleSystem effectNextBall, ItemManagerModel ballManager)
        {
            PlayerData = playerData;
            _input = input;

            InitializeComponents();
        }

        private void Update()
        {
        }

        public void AddScore(int score)
        {
            _score += score;
            _playerUI.UpdateScoreTextUI(_score);
        }

        private void InitializeComponents()
        {
            _playerUI = new PlayerUI(_scoreText);
        }
    }
}
