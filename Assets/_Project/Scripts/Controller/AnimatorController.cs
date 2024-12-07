using Assets._project.CodeBase;
using Assets._project.Config;

namespace Assets._Project.Scripts.Controller
{
    public class AnimatorController
    {
        public void DeactivateItemWithAnimation(ItemModel item, float shrinkDuration)
        {
            ItemAnimatorModel animatorModel = new ItemAnimatorModel(item);

            animatorModel.AnimateShrink(shrinkDuration);
        }
    }
}
