using System;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class PlayerInput : MonoBehaviour
    {
        public event Action<Item> OnItemClicked;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                if (hit.collider != null)
                    Debug.Log($"Hit: {hit.collider.gameObject.name}");

                if (hit.collider != null && hit.collider.TryGetComponent(out Item clickedItem))
                {
                    Debug.Log("Клик по предмету сработал");
                    Debug.Log(clickedItem.transform.position + " - clickedItem.transform.position");

                   OnItemClicked?.Invoke(clickedItem);
                }
            }
        }
    }

}
