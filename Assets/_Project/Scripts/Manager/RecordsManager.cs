using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class RecordsManager
    {
        private string _filePath;

        public RecordsManager()
        {
            _filePath = Path.Combine(Application.persistentDataPath, "records.json");

            if (!File.Exists(_filePath))
            {
                var initialRecords = new List<RecordModel>
            {
                new RecordModel("Player1", 30),
            };
                SaveRecords(initialRecords);
            }
        }

        public List<RecordModel> LoadRecords()
        {
            var json = File.ReadAllText(_filePath);
            return JsonUtility.FromJson<HighScores>(json).Records;
        }

        public void SaveRecords(List<RecordModel> records)
        {
            var data = new HighScores { Records = records };
            var json = JsonUtility.ToJson(data, true);
            File.WriteAllText(_filePath, json);
        }
    }
}
