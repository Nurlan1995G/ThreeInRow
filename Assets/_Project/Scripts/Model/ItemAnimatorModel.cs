namespace Assets._project.CodeBase
{
    public class ItemAnimatorModel
    {
        private const string IsDead = "IsDead";
        private ItemModel _item;

        public ItemAnimatorModel(ItemModel item) =>
            _item = item;

        public void AnimateShrink(bool dead)
        {
            _item.Item.Animator.SetBool(IsDead, dead);
        }
    }
}
