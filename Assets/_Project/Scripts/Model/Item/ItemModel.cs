using UnityEngine;

namespace Assets._project.CodeBase
{
    public class ItemModel
    {
        private readonly Item _item;
        private Point _currentPoint;

        public Item Item => _item;
        public Vector3 ItemPosition => _item.transform.position;

        public ItemModel(Item item) =>
            _item = item;

        public void SetPosition(Vector3 position) =>
            _item.transform.position = position;

        public void SetCurrentPoint(Point point) =>
            _currentPoint = point;

        public Point GetCurrentPoint() =>
            _currentPoint;

        public void Activate() =>
            _item.gameObject.SetActive(true);

        public void Deactivate() =>
            _item.gameObject.SetActive(false);

        public void SetDynamic() => 
            Item.Rigidbody2D.isKinematic = false;

        public void SetKinematic()
        {
            Item.Rigidbody2D.isKinematic = true;
            Item.Rigidbody2D.velocity = Vector2.zero; 
        }

        public void RemoveFromCurrentPoint()
        {
            if (_currentPoint != null)
            {
                _currentPoint.MarkAsFree();
                _currentPoint = null;
            }
        }
    }
}
