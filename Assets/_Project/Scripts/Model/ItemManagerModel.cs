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

        public List<Item> GetItemsInRow(Item item)
        {
            List<Item> itemsInRow = new List<Item>();

            var currentPoint = item.GetCurrentPoint();
            
            if (currentPoint == null) 
                return itemsInRow;

            Vector3 position = currentPoint.transform.position;

            foreach (var i in _items)
            {
                var point = i.GetCurrentPoint();
               
                if (point != null && Mathf.Approximately(point.transform.position.y, position.y))
                    itemsInRow.Add(i);
            }

            return itemsInRow;
        }

        public List<Item> GetItemsInColumn(Item item)
        {
            List<Item> itemsInColumn = new List<Item>();

            var currentPoint = item.GetCurrentPoint();

            if (currentPoint == null) 
                return itemsInColumn;

            Vector3 position = currentPoint.transform.position;

            foreach (var i in _items)
            {
                var point = i.GetCurrentPoint();

                if (point != null && Mathf.Approximately(point.transform.position.x, position.x))
                    itemsInColumn.Add(i);
            }

            return itemsInColumn;
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
