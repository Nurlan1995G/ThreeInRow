using Assets._project.CodeBase;
using System.IO;
using UnityEngine;

namespace Assets._Project.Scripts.Controller
{
    //Управляет загрузкой, сохранением и обновлением рекордов в файле JSON. Предоставляет методы для обработки текущего счета и списка лучших результатов.
    public class RecordsController : MonoBehaviour
    {
        [SerializeField] private RecordView _recordView;
        private string _filePath;

        public HighScores HighScores { get; private set; }

        //Инициализирует путь к файлу для хранения рекордов. Вызывает LoadHighScores()загрузку рекордов при начале игры.
        private void Start()
        {
            _filePath = Path.Combine(Application.persistentDataPath, "highscores.json");
            LoadHighScores();
        }

        //Проверяет, существует ли файл с рекордами. Если он существует, считывает JSON-файл и десериализует его в HighScoresобъект. Если файл не существует, создает рекорды по умолчанию, сохраняет их в новом файле JSON и инициализирует HighScoresобъект.
        public void LoadHighScores()
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                HighScores = JsonUtility.FromJson<HighScores>(json);
            }
            else
            {
                HighScores = new HighScores();
                HighScores.AddRecord("Игрок 1", 10);
                HighScores.AddRecord("Игрок 2", 20);
                HighScores.AddRecord("Игрок 3", 30);
                SaveHighScores();
            }
        }

        //Сериализует HighScoresобъект в формате JSON и записывает его в файл, указанный в _filePath.
        public void SaveHighScores()
        {
            string json = JsonUtility.ToJson(HighScores, true);
            File.WriteAllText(_filePath, json);
        }

        //Устанавливает текущий счет в HighScoresобъекте и сохраняет обновленные рекорды в файл.
        public void SaveCurrentScore(int currentScore)
        {
            HighScores.CurrentScore = currentScore;
            SaveHighScores();
        }

        //Добавляет новую запись (имя игрока и счет) к HighScoresобъекту. Сохраняет обновленные рекорды. Обновляет представление последними рекордами, вызывая UpdateRecords()объект _recordView.
        public void AddNewRecord(string playerName, int score)
        {
            HighScores.AddRecord(playerName, score);
            SaveHighScores();
            _recordView.UpdateRecords(HighScores); 
        }
    }
}
