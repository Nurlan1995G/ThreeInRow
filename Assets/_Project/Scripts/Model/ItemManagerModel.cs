using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class ItemManagerModel
    {
        private Dictionary<TypeItem, List<ItemModel>> _itemsByType;
        private List<ItemModel> _items;
        private List<ItemModel> _reserveItems;
        private List<ItemModel> _activaItems;

        public ItemManagerModel(List<ItemModel> items)
        {
            _items = items;
            _activaItems = new List<ItemModel>();
            _reserveItems = new List<ItemModel>();

            PopulateItemByType();
        }

        public void ReplaceItem(ItemModel oldItem)
        {
            oldItem.RemoveFromCurrentPoint();
            AddAfterReset(oldItem);
        }

        public ItemModel GetRandomItem()
        {
            if (_items.Count > 0)
            {
                int randomIndex = Random.Range(0, _items.Count);
                ItemModel item = _items[randomIndex];
                _items.RemoveAt(randomIndex);
                _activaItems.Add(item);
                return item;
            }
            else if (_reserveItems.Count > 0)
            {
                int randomIndex = Random.Range(0, _reserveItems.Count);
                ItemModel item = _reserveItems[randomIndex];
                _reserveItems.RemoveAt(randomIndex);
                return item;
            }

            return null;
        }

        public List<ItemModel> GetItemsOnSameY(ItemModel clickedItem)
        {
            float mainPositionY = clickedItem.ItemPosition.y;
            return _activaItems.Where(item => Mathf.Approximately(item.ItemPosition.y, mainPositionY)).ToList();
        }


        public List<ItemModel> GetItemsOnSameX(ItemModel clickedItem)
        {
            float mainPositionX = clickedItem.ItemPosition.x;
            return _activaItems.Where(item => Mathf.Approximately(item.ItemPosition.x, mainPositionX)).ToList();
        }

        public List<ItemModel> OnItemsMatched()
        {
            foreach (var item in _activaItems)
                item.RemoveFromCurrentPoint();

            return _activaItems;
        }

        public void AddAfterReset(ItemModel item)
        {
            if (!_items.Contains(item) && !_reserveItems.Contains(item))
            {
                _activaItems.Remove(item);
                _reserveItems.Add(item);
            }
        }

        private void PopulateItemByType()  //Заполнить тип предмета
        {
            _itemsByType = new Dictionary<TypeItem, List<ItemModel>>();

            foreach (ItemModel item in _items)
            {
                if (!_itemsByType.ContainsKey(item.Item.TypeItem))
                    _itemsByType.Add(item.Item.TypeItem, new List<ItemModel>());

                _itemsByType[item.Item.TypeItem].Add(item);
            }
        }
    }
}
