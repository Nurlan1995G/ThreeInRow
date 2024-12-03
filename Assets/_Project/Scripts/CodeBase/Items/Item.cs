using Assets._project.Config;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        private Point _currentPoint;

        [field: SerializeField] public TypeItem TypeItem { get; private set; }
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
        public ItemModel ItemModel { get; private set; }

        public void Initialize(ItemModel itemModel)
        {
            ItemModel = itemModel;
            ResetScale();
        }

        public void ResetScale() => 
            transform.localScale = _gameConfig.LogicData.SizeItem;
    }

    public enum TypeItem
    {
        Mushroom,
        Meat,
        Potion,
        Eye
    }
}
