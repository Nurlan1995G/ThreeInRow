using UnityEngine;

namespace Assets._project.CodeBase
{
    public class Item : MonoBehaviour
    {
        private Point _currentPoint;

        public Vector3 ItemPosition => transform.position;
        [field: SerializeField] public TypeItem TypeItem { get; private set; }
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }

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
