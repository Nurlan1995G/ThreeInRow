using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class Item : MonoBehaviour
    {
        private Point _currentPoint;
        private bool _canDetectCollisions;

        [field: SerializeField] public TypeItem TypeItem { get; private set; }

        public event Action<List<Item>> OnCollisionDetected;

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (!_canDetectCollisions)
                return; // Если проверка не активна, ничего не делаем

            Debug.Log(_canDetectCollisions + " - canDetect");

            if (collision.gameObject.TryGetComponent(out Item otherItem))
            {
                Debug.Log("Trigger - сработал");
                OnCollisionDetected?.Invoke(new List<Item> { otherItem });
            }
        }

        public void ActivateCollisionDetection()
        {
            Debug.Log("ActivateCollisionDetection");
            _canDetectCollisions = true;
        }

        public void DeactivateCollisionDetection() => 
            _canDetectCollisions = false;

        public void SetPosition(Vector3 position) =>
        transform.position = position;

        public void Activate() =>
            gameObject.SetActive(true);

        public void Deactivate() =>
            gameObject.SetActive(false);

        public void SetCurrentPoint(Point point) => 
            _currentPoint = point;

        public Point GetCurrentPoint() =>
            _currentPoint;

        public void RemoveFromCurrentPoint()
        {
            if (_currentPoint != null)
            {
                _currentPoint.FreeCell();
                _currentPoint = null;
            }
        }
    }

    public enum TypeItem
    {
        Mushroom,
        Meat,
        Potion,
        Eye
    }
}
