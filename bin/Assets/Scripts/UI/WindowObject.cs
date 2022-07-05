using UnityEngine;
using UnityEngine.EventSystems;

/*
 * 게임 내 Window 오브젝트 
 */
public abstract class WindowObject : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler {
    //For Opening Window
    [SerializeField] private bool isDraggable;
    [SerializeField] private bool notMoveableWhileOpen;
    public abstract void Activate();
    public void OpenWindowWithoutSetting()
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
    }
    public void CloseWindowWithoutOption()
    {
        gameObject.SetActive(false);
    }
    public void OpenWindow()
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
        WindowManager.Instance.currentOpenWindowNum += 1;
        if (notMoveableWhileOpen)
            InputManager.Instance.SetOptions(false, true);
        else
            InputManager.Instance.SetOptions(true, true);
    }
    public void CloseWindow()
    {
        WindowManager.Instance.currentOpenWindowNum -= 1;
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
