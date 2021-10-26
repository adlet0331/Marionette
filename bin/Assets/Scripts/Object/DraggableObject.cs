using UnityEngine;
using UnityEngine.EventSystems;

/* �巡�� ������ ������Ʈ
 * 
 */
public abstract class DraggableObject : WindowedObject, IDragHandler, IBeginDragHandler {
    private Vector3 _dragBeginCursorPos;
    private Vector3 _dragBeginWindowPos;

    public void OnBeginDrag(PointerEventData eventData) {
        _dragBeginCursorPos = eventData.position;
        _dragBeginWindowPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = _dragBeginWindowPos + (Vector3)eventData.position - _dragBeginCursorPos;
    }
}
