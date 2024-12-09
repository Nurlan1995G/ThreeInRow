using Assets._Project.Scripts.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._project.CodeBase
{
    public class MenuControllerUI : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private GameObject _mainMenu; // Главное меню
        [SerializeField] private GameObject _recordsPanel; // Панель с рекордами
        [SerializeField] private GameObject _gameOverPanel; // Панель "Игра окончена"
        [SerializeField] private TextMeshProUGUI _scoreText; // Текст для отображения очков
        [SerializeField] private Button _newGameButton; // Кнопка "Новая игра"
        [SerializeField] private Button _recordsButton; // Кнопка "Рекорды"
        [SerializeField] private Button _exitButton; // Кнопка "Выход"
        [SerializeField] private Button _openMenuButton; // Кнопка "Открыть меню"

        [Header("Game Controllers")]
        [SerializeField] private RecordsController _recordsController; // Контроллер рекордов
        [SerializeField] private GameController _gameController; // Контроллер игры

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

