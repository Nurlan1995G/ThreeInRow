﻿using System;
using UnityEngine;

namespace Assets._project.Config
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Config")]
    public class GameConfig : ScriptableObject
    {
        public ManagerData ManagerData;
        public BallData BallData;
        public PlayerData PlayerData;
        public LogicData LogicData;
    }

    [Serializable]
    public class PlayerData
    {
        public float MaxShootingForce = 15f;
        public float MinShootingForce = 5f;
        public float MaxLineLength = 5f;
        public float MaxPullDistance = 10f;
        public int TotalBall = 30;
    }

    [Serializable]
    public class BallData
    {
        public float MinSpeed;
        public float MaxSpeed;
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
        public int RewardToPlayer = 10;
    }
}
