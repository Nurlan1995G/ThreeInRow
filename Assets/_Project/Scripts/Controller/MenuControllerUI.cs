using Assets._Project.Scripts.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._project.CodeBase
{
    public class MenuControllerUI : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private GameObject _mainMenu; 
        [SerializeField] private GameObject _recordsPanel; 
        [SerializeField] private GameObject _gameOverPanel; 
        [SerializeField] private TextMeshProUGUI _scoreText; 
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _recordsButton;
        [SerializeField] private Button _exitButton; 
        [SerializeField] private Button _openMenuButton; 

        [Header("Game Controllers")]
        [SerializeField] private RecordsController _recordsController; 
        [SerializeField] private GameController _gameController;

        private bool _isGamePaused;

        private void Start()
        {
            _newGameButton.onClick.AddListener(StartNewGame);
            _recordsButton.onClick.AddListener(ShowRecords);
            _exitButton.onClick.AddListener(ExitGame);
            _openMenuButton.onClick.AddListener(OpenMenu);

            _mainMenu.SetActive(true);
            _recordsPanel.SetActive(false);
            _isGamePaused = false;
        }

        private void StartNewGame()
        {
            _mainMenu.SetActive(false);
            _gameController.ResetGame();
            ResumeGame();
        }

        private void ShowRecords()
        {
            _recordsController.LoadHighScores();
            _recordsPanel.SetActive(true);
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; 
#else
        Application.Quit(); // Для билда игры
#endif
        }

        private void OpenMenu()
        {
            if (!_isGamePaused)
            {
                PauseGame();
                _mainMenu.SetActive(true);
            }
            else
            {
                ResumeGame();
                _mainMenu.SetActive(false);
            }
        }

        private void PauseGame()
        {
            Time.timeScale = 0f; 
            _isGamePaused = true;
        }

        private void ResumeGame()
        {
            Time.timeScale = 1f; 
            _isGamePaused = false;
        }

        public void EndGame(int score)
        {
            PauseGame();
            _mainMenu.SetActive(false);
            _gameOverPanel.SetActive(true);

            _scoreText.text = $"Ваши очки: {score}";
        }
    }
}

