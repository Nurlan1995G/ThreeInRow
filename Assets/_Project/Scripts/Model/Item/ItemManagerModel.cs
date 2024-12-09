using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._project.CodeBase
{
    //Этот класс отвечает за управление коллекцией предметов в игре. Он организует предметы в различные категории и предоставляет функциональность для управления их позициями, добавления и удаления, а также извлечения случайных предметов.
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

        //Удаляет элемент из текущей позиции и добавляет его в список резервов
        public void ReplaceItem(ItemModel oldItem)
        {
            oldItem.RemoveFromCurrentPoint();
            AddAfterReset(oldItem);
        }

        //Возвращает случайный элемент из основного списка элементов или резервного списка. Удаляет элемент из исходного списка и добавляет его в список активных элементов
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

        //Возвращает список элементов, имеющих ту же координату Y, что и выбранный элемент, по сути, находя элементы, выровненные по горизонтали
        public List<ItemModel> GetItemsOnSameY(ItemModel clickedItem)
        {
            float mainPositionY = clickedItem.ItemPosition.y;
            return _activaItems.Where(item => Mathf.Approximately(item.ItemPosition.y, mainPositionY)).ToList();
        }

        //Возвращает список элементов, имеющих ту же координату X, что и выбранный элемент, по сути, находя элементы, выровненные по вертикали
        public List<ItemModel> GetItemsOnSameX(ItemModel clickedItem)
        {
            float mainPositionX = clickedItem.ItemPosition.x;
            return _activaItems.Where(item => Mathf.Approximately(item.ItemPosition.x, mainPositionX)).ToList();
        }

        //Удаляет все активные элементы из их текущих позиций и возвращает список соответствующих элементов. Обычно используется, когда элементы соответствуют и удаляются из сетки
        public List<ItemModel> OnItemsMatched()
        {
            foreach (var item in _activaItems)
                item.RemoveFromCurrentPoint();

            return _activaItems;
        }

        //Добавляет элемент в список резервов, если его еще нет в основном списке элементов или списке резервов
        public void AddAfterReset(ItemModel item)
        {
            if (!_items.Contains(item) && !_reserveItems.Contains(item))
            {
                _activaItems.Remove(item);
                _reserveItems.Add(item);
            }
        }

        //Категоризирует элементы по типу, сохраняя их в словаре, где каждому типу соответствует список элементов
        private void PopulateItemByType()  
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
