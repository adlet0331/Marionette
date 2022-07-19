using Managers;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * 게임 내 Window 오브젝트 
 */
namespace UI
{
    public abstract class WindowObject : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler {
        //For Opening Window
        [SerializeField] private bool isDraggable;
        [SerializeField] private bool moveableWhileOpen;
        [SerializeField] private bool inputableWhileOpen;
        public abstract void Activate();
        public void OpenWindow()
        {
            gameObject.SetActive(true);
            transform.SetAsLastSibling();
            WindowManager.Instance.SetCurrentWindow(this);
            InputManager.Instance.SetOptions(moveableWhileOpen, inputableWhileOpen);
        }
        public void CloseWindow()
        {
            gameObject.SetActive(false);
            WindowManager.Instance.RemoveWindow(this);
            InputManager.Instance.SetOptions(true, true);
        }
        
        // Interface
        public void OnBeginDrag(PointerEventData eventData) {
            if (!isDraggable) 
                return;
            dragBeginCursorPos = eventData.position;
            dragBeginWindowPos = transform.position;
        }
        public void OnDrag(PointerEventData eventData) {
            if (!isDraggable) 
                return;
            transform.position = dragBeginWindowPos + (Vector3)eventData.position - dragBeginCursorPos;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if (!isDraggable) 
                return;
            transform.SetAsLastSibling();
        }
        private Vector3 dragBeginCursorPos;
        private Vector3 dragBeginWindowPos;
    }
}
