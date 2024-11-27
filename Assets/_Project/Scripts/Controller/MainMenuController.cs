using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Project.Scripts
{
    public class MainMenuController
    {
        private readonly MainMenuView view;

        public MainMenuController(MainMenuView view)
        {
            this.view = view;

            view.NewGameButton.onClick.AddListener(StartNewGame);
            view.RecordsButton.onClick.AddListener(OpenRecords);
            view.ExitButton.onClick.AddListener(ExitGame);
        }

        private void StartNewGame() => SceneManager.LoadScene("GameScene");
        private void OpenRecords() => SceneManager.LoadScene("RecordsScene");
        private void ExitGame() => Application.Quit();
    }
}
