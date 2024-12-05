using DG.Tweening;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class ItemAnimatorModel
    {
        private ItemModel _item;

        public ItemAnimatorModel(ItemModel item) =>
            _item = item;

        public void AnimateShrink(float duration, System.Action onComplete = null)
        {
            _item.Item.transform.DOScale(Vector3.zero, duration)
                .SetEase(Ease.Linear) 
                .OnComplete(() => onComplete?.Invoke());
        }
    }
}
