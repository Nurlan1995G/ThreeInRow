using UnityEngine;

namespace Assets._project.CodeBase
{
    public class Item : MonoBehaviour
    {
        [field: SerializeField] public TypeItem TypeItem { get; private set; }
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
        public ItemModel ItemModel { get; private set; }

        public void Initialize(ItemModel itemModel) =>
            ItemModel = itemModel;
    }

    public enum TypeItem
    {
        Mushroom,
        Meat,
        Potion,
        Eye
    }
}
