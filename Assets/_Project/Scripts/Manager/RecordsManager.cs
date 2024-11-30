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
                var initialRecords = new List<Record>
            {
                new Record("Player1", 30),
            };
                SaveRecords(initialRecords);
            }
        }

        public List<Record> LoadRecords()
        {
            var json = File.ReadAllText(_filePath);
            return JsonUtility.FromJson<RecordsData>(json).Records;
        }

        public void SaveRecords(List<Record> records)
        {
            var data = new RecordsData { Records = records };
            var json = JsonUtility.ToJson(data, true);
            File.WriteAllText(_filePath, json);
        }
    }
}
