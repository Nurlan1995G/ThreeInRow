using UnityEngine;

namespace Assets._project.CodeBase
{
    public class Point : MonoBehaviour
    {
        [SerializeField] private bool _isBusy;
        private int _rowPoint;
        private int _columnPoint;

        public bool IsBusy => _isBusy;

        public Vector3 GetPlaceItem(ItemModel item)
        {
            if (!_isBusy && item.GetCurrentPoint() == null)
            {
                _isBusy = true;
                item.SetCurrentPoint(this);
                return transform.position;
            }

            return Vector3.zero;
        }

        public void SetInfoPositionPoint(int row, int column)
        {
            _rowPoint = row;
            _columnPoint = column;
        }

        public (int row, int column) GetInfoPositionPoint()
        {
            int row = _rowPoint;
            int column = _columnPoint;

            return (row, column);
        }

        public void MarkAsBusy()
        {
            if (!_isBusy)
                _isBusy = true;
        }

        public void MarkAsFree()
        {
            if (_isBusy)
                _isBusy = false;
        }
    }
}
