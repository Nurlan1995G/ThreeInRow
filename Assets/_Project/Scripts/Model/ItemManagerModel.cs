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

        public void AddAfterReset(Item item)
        {
            item.transform.position = _startPosition;
            item.Deactivate();
            Debug.Log(item.transform.position + " item position " + item.name);

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
