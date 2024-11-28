using Assets._project.CodeBase.Interface;
using Assets._project.Config;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class Item : MonoBehaviour, IItemControll
    {
        private Point _currentPoint;

        [field: SerializeField] public TypeItem TypeItem { get; private set; }

        public void Construct(GridManager gridManager, LogicData logicData, ItemManager ballManager, Player player)
        {
            //_triggerBall.Construct(this, gridManager, ballManager, player, logicData);
        }

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
