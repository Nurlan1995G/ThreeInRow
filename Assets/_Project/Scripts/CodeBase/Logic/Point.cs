using UnityEngine;

namespace Assets._project.CodeBase
{
    public class Point : MonoBehaviour
    {
        [SerializeField] private bool _isBusy;

        public bool IsBusy => _isBusy;

        public Vector3 GetPlaceItem(Item item)
        {
            if (!_isBusy && item.GetCurrentPoint() == null)
            {
                _isBusy = true;
                return transform.position;
            }

            return Vector3.zero;
        }

        public void MarkAsBusy() => 
            _isBusy = true;

        public void FreeCell() => 
            _isBusy = false;
    }
}
