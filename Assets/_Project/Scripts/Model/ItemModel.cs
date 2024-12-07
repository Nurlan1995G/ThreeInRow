using System.Drawing;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class ItemModel
    {
        private readonly Item _item;
        private Point _currentPoint;

        public Item Item => _item;
        public Vector3 Position => _item.transform.position;

        public ItemModel(Item item) =>
            _item = item;

        public void SetPosition(Vector3 position) =>
            _item.transform.position = position;

        public void SetCurrentPoint(Point point) =>
            _currentPoint = point;

        public Point GetCurrentPoint() =>
            _currentPoint;

        public (int row, int column) GetCurrentPointData() =>
            _currentPoint?.GetInfoPositionPoint() ?? (-1, -1); 

        public void Activate() =>
            _item.gameObject.SetActive(true);

        public void Deactivate() =>
            _item.gameObject.SetActive(false);

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
