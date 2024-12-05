using System;
using UnityEngine;

namespace Assets._project.Config
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Config")]
    public class GameConfig : ScriptableObject
    {
        public ManagerData ManagerData;
        public PlayerData PlayerData;
        public LogicData LogicData;
    }

    [Serializable]
    public class PlayerData
    {
        public int StartCountScore = 10;
    }

    [Serializable]
    public class ManagerData
    {
        public int TotalRows = 12;
        public int TotalColumns = 10;
        public int RowsToFill = 5;
        public float CellSize = 1.0f;
        public int TotalItemsToLoad = 50;
        public Vector3 StartPosition = new Vector3(-2.5f,2,0);
    }

    [Serializable]
    public class LogicData
    {
        public int SubstractScore = 3;
        public int Reward = 1;
        public float DropDuration = 1f;
        public float ShrinkDuration = 0.5f;
    }
}
