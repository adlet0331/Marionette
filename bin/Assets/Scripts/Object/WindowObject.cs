using UnityEngine;
using UnityEngine.EventSystems;

/* 드래그 가능한 오브젝트
 * 
 */
public abstract class WindowObject : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler {
    //For Opening Window
    [SerializeField] private bool isDraggable;
    [SerializeField] private bool isMoveable;
    public abstract void Activate();
    public bool ActivateObject()
    {
        if (gameObject.activeSelf)
        {
            CloseWindow();
            return true;
        }
        else
        {   
            OpenWindow();
            return false;
        }
    }
    public void OpenWindow()
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
        if (!isMoveable)
            InputManager.Instance.SetOptions(false, true);
    }
    public void CloseWindow()
    {
        gameObject.SetActive(false);
        InputManager.Instance.SetOptions(true, true);
    }
    //Interface
    private Vector3 _dragBeginCursorPos;
    private Vector3 _dragBeginWindowPos;
    public void OnBeginDrag(PointerEventData eventData) {
        if (!isDraggable) 
            return;
        _dragBeginCursorPos = eventData.position;
        _dragBeginWindowPos = transform.position;
    }
    public void OnDrag(PointerEventData eventData) {
        if (!isDraggable) 
            return;
        transform.position = _dragBeginWindowPos + (Vector3)eventData.position - _dragBeginCursorPos;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isDraggable) 
            return;
        transform.SetAsLastSibling();
    }
}
