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
            List<ItemModel> itemsOnSameY = new List<ItemModel>();

            var mainPositionY = clickedItem.Position.y;

            foreach (var item in _activaItems)
            {
                if (Mathf.Approximately(item.Position.y, mainPositionY))
                    itemsOnSameY.Add(item);
            }

            return itemsOnSameY;
        }

        public List<ItemModel> GetItemsOnSameX(ItemModel clickedItem)
        {
            List<ItemModel> itemsOnSameX = new List<ItemModel>();

            var mainPositionX = clickedItem.Position.x;

            foreach (var item in _activaItems)
            {
                if (Mathf.Approximately(item.Position.x, mainPositionX))
                    itemsOnSameX.Add(item);
            }

            return itemsOnSameX;
        }

        public List<ItemModel> OnItemsMatched()
        {
            foreach (ItemModel item in _activaItems)
                item.RemoveFromCurrentPoint();

            return _activaItems;
        }

        public List<ItemModel> GetAllItems()
        {
            return _activaItems.Where(item => item.Item.gameObject.activeSelf).ToList();
        }

        private void AddAfterReset(ItemModel item)
        {
            if (!_items.Contains(item) && !_reserveItems.Contains(item))
            {
                _reserveItems.Add(item);
                _activaItems.Remove(item);
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
