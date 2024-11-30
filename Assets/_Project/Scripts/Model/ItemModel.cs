namespace Assets._project.CodeBase
{
    public class ItemModel
    {
        private int _score;
        private float _size;
        private Item _item;

        public int Score => _score;
        public float Size => _size;
        public Item Item => _item;

        public ItemModel(int score, float size, Item item)
        {
            _score = score;
            _size = size;
            _item = item;
        }


    }
}
