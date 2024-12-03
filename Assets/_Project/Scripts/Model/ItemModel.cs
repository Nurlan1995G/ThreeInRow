using UnityEngine;

namespace Assets._project.CodeBase
{
    public class ItemModel
    {
        private readonly Item _item;
        private Point _currentPoint;

        public Vector3 Position => _item.transform.position;

        public ItemModel(Item item)
        {
            _item = item;
        }

        public void SetPosition(Vector3 position)
        {
            _item.transform.position = position;
        }

        public void Activate()
        {
            _item.gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            _item.gameObject.SetActive(false);
        }

        public void SetCurrentPoint(Point point)
        {
            _currentPoint = point;
        }

        public void RemoveFromCurrentPoint()
        {
            _currentPoint?.FreeCell();
            _currentPoint = null;
        }
    }
}
