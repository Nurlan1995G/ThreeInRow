using Assets._project.Config;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        private PlayerUI _playerUI;
        private PlayerInput _input;

        private int _score;

        public PlayerData PlayerData { get; private set; }

        public void Construct(PlayerData playerData, PlayerInput input, ParticleSystem effectNextBall, ItemManagerModel ballManager)
        {
            PlayerData = playerData;
            _input = input;

            InitializeComponents();
        }

        private void Update()
        {
            _input.Update();
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
