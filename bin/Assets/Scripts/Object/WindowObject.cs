using UnityEngine;
using UnityEngine.EventSystems;

/* 드래그 가능한 오브젝트
 * 
 */
public abstract class WindowObject : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler {
    //For Opening Window
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
    private void OpenWindow()
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
    }
    private void CloseWindow()
    {
        gameObject.SetActive(false);
    }
    //Interface
    private Vector3 _dragBeginCursorPos;
    private Vector3 _dragBeginWindowPos;
    public void OnBeginDrag(PointerEventData eventData) {
        _dragBeginCursorPos = eventData.position;
        _dragBeginWindowPos = transform.position;
    }
    public void OnDrag(PointerEventData eventData) {
        transform.position = _dragBeginWindowPos + (Vector3)eventData.position - _dragBeginCursorPos;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
    }
}
