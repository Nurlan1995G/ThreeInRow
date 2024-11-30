using System;

namespace Assets._project.CodeBase
{
    [Serializable]
    public class Record
    {
        public string Name;
        public int Score;

        public Record(string name, int score)
        {
            Name = name;
            Score = score;
        }
    }
}
