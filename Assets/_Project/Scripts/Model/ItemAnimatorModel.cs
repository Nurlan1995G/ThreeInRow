using System.Collections;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class ItemAnimatorModel
    {
        private ItemModel _item;

        public ItemAnimatorModel(ItemModel item) => 
            _item = item;

        public IEnumerator AnimateShrink(float duration, System.Action onComplete = null)
        {
            Vector3 initialScale = _item.Item.transform.localScale;
            Vector3 targetScale = Vector3.zero;

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                _item.Item.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
                yield return null;
            }

            _item.Item.transform.localScale = targetScale;
            onComplete?.Invoke(); 
        }
    }
}
