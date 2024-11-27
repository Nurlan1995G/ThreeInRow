using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets._Project.Scripts
{
    public class GridCellView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image cellImage; // Ссылка на Image компонента для отображения содержимого ячейки.

        public delegate void CellClickHandler();
        public event CellClickHandler OnClick;

        /// <summary>
        /// Устанавливает содержимое ячейки.
        /// </summary>
        /// <param name="content">Sprite, который будет отображаться в ячейке. Если null, ячейка считается пустой.</param>
        public void SetContent(Sprite content)
        {
            if (content != null)
            {
                cellImage.sprite = content;
                cellImage.enabled = true; // Отображаем ячейку.
            }
            else
            {
                cellImage.sprite = null;
                cellImage.enabled = false; // Скрываем ячейку, если она пустая.
            }
        }

        /// <summary>
        /// Обработка клика по ячейке.
        /// </summary>
        /// <param name="eventData">Информация о событии клика.</param>
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke();
        }
    }
}
