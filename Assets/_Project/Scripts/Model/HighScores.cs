using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets._project.CodeBase
{
    [Serializable]
    // используемый класс для отслеживания рекордов в игре
    public class HighScores
    {
        public List<RecordModel> Records = new List<RecordModel>();
        public int CurrentScore;

        //Добавляет новую запись счета с именем игрока и счетом. Это гарантирует, что список записей будет отсортирован по убыванию и сохранит только 3 лучших счета
        public void AddRecord(string playerName, int score)
        {
            Records.Add(new RecordModel(playerName, score));
            Records = Records.OrderByDescending(r => r.Score).Take(3).ToList(); 
        }
    }
}
