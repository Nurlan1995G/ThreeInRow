using System.Collections.Generic;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class ItemManagerModel
    {
        private Dictionary<TypeItem, List<Item>> _itemsByType;
        private List<Item> _items;
        private List<Item> _reserveItems;
        
        private Vector3 _startPosition;

        public ItemManagerModel(List<Item> items, Vector3 startPosition)
        {
            _items = items;
            _startPosition = startPosition;
            _reserveItems = new List<Item>(); 

            PopulateItemByType();
        }

        public void ReplaceItem(Item oldItem)
        {
            oldItem.RemoveFromCurrentPoint();
            AddAfterReset(oldItem);
        }

        public Item GetRandomItem()
        {
            if (_items.Count > 0)
            {
                int randomIndex = Random.Range(0, _items.Count);
                Item item = _items[randomIndex];
                _items.RemoveAt(randomIndex);
                return item;
            }
            else if (_reserveItems.Count > 0)
            {
                int randomIndex = Random.Range(0, _reserveItems.Count);
                Item item = _reserveItems[randomIndex];
                _reserveItems.RemoveAt(randomIndex);
                return item;
            }

            return null;  
        }

        public List<Item> FilterMatchingItems(Item clickedItem, List<Item> items)
        {
            Debug.Log("FilterMatchingItems");

            List<Item> matchingItems = new List<Item>();

            foreach (var item in items)
            {
                if (item.TypeItem == clickedItem.TypeItem)
                {
                    Debug.Log("типы совпали");
                    matchingItems.Add(item);
                }
            }

            return matchingItems;
        }

        public void AddAfterReset(Item item)
        {
            if (!_items.Contains(item) && !_reserveItems.Contains(item))
                _reserveItems.Add(item);  
        }

        private void PopulateItemByType()  //Заполнить тип предмета
        {
            _itemsByType = new Dictionary<TypeItem, List<Item>>();

            foreach (Item ball in _items)
            {
                if (!_itemsByType.ContainsKey(ball.TypeItem))
                    _itemsByType.Add(ball.TypeItem, new List<Item>());

                _itemsByType[ball.TypeItem].Add(ball);
            }
        }
    }
}
