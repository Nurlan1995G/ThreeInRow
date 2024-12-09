using System;

namespace Assets._project.CodeBase
{
    [Serializable]
    public class RecordModel
    {
        public string PlayerName;
        public int Score;

        public RecordModel(string playerName, int score)
        {
            PlayerName = playerName;
            Score = score;
        }
    }
}
