using Assets._project.CodeBase;
using System.IO;
using UnityEngine;

namespace Assets._Project.Scripts.Controller
{
    public class RecordsController : MonoBehaviour
    {
        [SerializeField] private RecordView _recordView;
        private string _filePath;

        public HighScores HighScores { get; private set; }

        private void Start()
        {
            _filePath = Path.Combine(Application.persistentDataPath, "highscores.json");
            LoadHighScores();
        }

        public void LoadHighScores()
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                HighScores = JsonUtility.FromJson<HighScores>(json);
            }
            else
            {
                // Создаем файл с начальными данными
                HighScores = new HighScores();
                HighScores.AddRecord("Игрок 1", 10);
                HighScores.AddRecord("Игрок 2", 20);
                HighScores.AddRecord("Игрок 3", 30);
                SaveHighScores();
            }
        }

        public void SaveHighScores()
        {
            string json = JsonUtility.ToJson(HighScores, true);
            File.WriteAllText(_filePath, json);
        }

        public void SaveCurrentScore(int currentScore)
        {
            HighScores.CurrentScore = currentScore;
            SaveHighScores();
        }

        public void AddNewRecord(string playerName, int score)
        {
            HighScores.AddRecord(playerName, score);
            SaveHighScores();
            _recordView.UpdateRecords(HighScores); 
        }
    }
}
