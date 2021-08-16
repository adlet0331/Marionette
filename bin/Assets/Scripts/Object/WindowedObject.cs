using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowedObject : MonoBehaviour, IPointerDownHandler {
    public event Action Closed;

    protected void OpenWindow() {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
    }

    public void Close() {
        OnClose();
        Closed?.Invoke();
        gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData) {
        transform.SetAsLastSibling();
    }

    public bool IsOpen => gameObject.activeSelf;

    protected virtual void OnClose() { }
}
