using UnityEngine;

namespace Assets._project.CodeBase
{
    public class Point : MonoBehaviour
    {
        [SerializeField] private bool _isBusy;

        public bool IsBusy => _isBusy;

        public void PlaceBall(Item item)
        {
            if (!_isBusy && item.GetCurrentPoint() == null) 
            {
                item.transform.position = transform.position;
                item.gameObject.SetActive(true);
                item.SetCurrentPoint(this);
                _isBusy = true;
            }
        }

        public void FreeCell() => 
            _isBusy = false;
    }
}
