using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets._project.CodeBase
{
    [Serializable]
    public class HighScores
    {
        public List<RecordModel> Records = new List<RecordModel>();

        public void AddRecord(string playerName, int score)
        {
            Records.Add(new RecordModel(playerName, score));
            Records = Records.OrderByDescending(r => r.Score).Take(3).ToList(); // Сортируем по убыванию и сохраняем только 3 лучших
        }
    }
}
