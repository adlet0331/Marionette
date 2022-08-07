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
        [SerializeField] private bool isDraggable = false;
        [SerializeField] private bool moveableWhileOpen = false;
        [SerializeField] private bool inputableWhileOpen = true;
        [SerializeField] private bool profileShowingWhileOpen = false;

        public bool IsOpened
        {
            get => this.gameObject.activeSelf;
        }
        public abstract void Activate();
        protected void OpenWindow()
        {
            if (gameObject.activeSelf)
                return;
            gameObject.SetActive(true);
            transform.SetAsLastSibling();
            WindowManager.Instance.SetCurrentWindow(this);
            InputManager.Instance.SetOptions(moveableWhileOpen, inputableWhileOpen);
            if (!profileShowingWhileOpen)
            {
                WindowManager.Instance.profileWindow.gameObject.SetActive(false);
            }
        }
        // ReSharper disable Unity.PerformanceAnalysis
        public void CloseWindow()
        {
            gameObject.SetActive(false);
            WindowManager.Instance.RemoveWindow(this);
            if (WindowManager.Instance.CurrentOpenWindowTypeString == "")
            {
                SceneSwitchManager.Instance.returnToOriginalSceneSetting();
            }
            if (!profileShowingWhileOpen)
            {
                WindowManager.Instance.profileWindow.gameObject.SetActive(true);
            }
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
