﻿using Assets._project.CodeBase.Sounds;
using Assets._project.Config;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class TriggerItem : MonoBehaviour
    {
        private Item _currentBall;
        private GridManager _gridManager;
        private ItemManager _ballManager;
        private Point _point;
        private Player _player;
        private LogicData _logicData;

        public void Construct(Item ball, GridManager gridManager, ItemManager ballManager, Player player, 
            LogicData logicData)
        {
            _currentBall = ball ?? throw new System.ArgumentNullException(nameof(ball));
            _gridManager = gridManager ?? throw new System.ArgumentNullException(nameof(gridManager));
            _ballManager = ballManager ?? throw new System.ArgumentNullException(nameof(ballManager));
            _player = player ?? throw new System.ArgumentNullException(nameof(player));
            _logicData = logicData ?? throw new System.ArgumentNullException(nameof(logicData));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Item otherBall))
            {
                if (_currentBall.TypeItem == otherBall.TypeItem)
                    CheckForMatch(otherBall);
                else
                {
                    if (_currentBall.GetCurrentPoint() == null)
                        SetPointToBall(_currentBall);
                }
            }
        }

        private void CheckForMatch(Item otherBall)
        {
            List<Item> matchingBalls = new List<Item>();
            FindMatchingBalls(_currentBall, ref matchingBalls);

            if (matchingBalls.Count == 2)
                SetPointToBall(_currentBall);

            if (matchingBalls.Count > 2)
            {
                foreach (Item ball in matchingBalls)
                {
                    _player.AddScore(_logicData.RewardToPlayer);
                    ball.RemoveFromCurrentPoint();
                    _ballManager.AddAfterReset(ball);
                }

                SoundHandler.Instance.PlayBurst();
            }
        }

        private void SetPointToBall(Item ball)
        {
            if(ball != null)
            {
                _gridManager.PlaceBallAtNearestFreePoint(ball);
            }
        }

        private void FindMatchingBalls(Item currentBall, ref List<Item> matchingBalls)
        {
            if (!matchingBalls.Contains(currentBall))
            {
                matchingBalls.Add(currentBall);
                Collider2D[] nearbyBalls = Physics2D.OverlapCircleAll(currentBall.transform.position, 1.0f);

                foreach (var collider in nearbyBalls)
                {
                    Item nextBall = collider.GetComponent<Item>();

                    if (nextBall != null && nextBall.TypeItem == currentBall.TypeItem)
                        FindMatchingBalls(nextBall, ref matchingBalls);
                }
            }
        }
    }
}
