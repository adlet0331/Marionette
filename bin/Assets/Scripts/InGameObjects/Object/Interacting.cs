using System.Collections.Generic;
using UnityEngine;

/*
 * Interact 하는 주체 (주로 플레이어)
 * 
 */
public class Interacting : MonoBehaviour
{
    [SerializeField] private bool isBlocked = false;
    [SerializeField] private int currentIdx;
    [SerializeField] private InteractionObject currentInteractObj;
    [SerializeField] private List<InteractionObject> scriptableObjList;

    private void Start()
    {
        scriptableObjList = new List<InteractionObject>();
    }
    private void updateFstIdx()
    {
        if (scriptableObjList.Count == 0)
        {
            this.currentIdx = -1;
            return;
        }
        else
        {
            float angle = 180.0f, cnt;
            InteractionObject currentFirstObj = scriptableObjList[0];
            var pointerWorldVec2 = InputManager.Instance.GetWorldPointerVec2();

            foreach (InteractionObject interObj in scriptableObjList)
            {
                var objectWorldPosition = interObj.transform.localToWorldMatrix * interObj.transform.localPosition - transform.localToWorldMatrix * transform.localPosition;
                var objectWorldVec2 = new Vector2(objectWorldPosition.x, objectWorldPosition.y);
                cnt = Vector2.Angle(objectWorldVec2, pointerWorldVec2);
                if (cnt < angle)
                {
                    angle = cnt;
                    currentFirstObj = interObj;
                }
            }
            currentInteractObj = currentFirstObj;
            currentIdx = currentFirstObj.idx;
        }
    }
    private void Update()
    {
        if (this.isBlocked)
            return;
        
        updateFstIdx();
        if (currentIdx == -1)
        {
            if (WindowManager.Instance.interactableWindow.gameObject.activeSelf)
                WindowManager.Instance.interactableWindow.CloseWindow();
        }
        else
        {
            if (!WindowManager.Instance.interactableWindow.gameObject.activeSelf)
                WindowManager.Instance.interactableWindow.OpenWindow();
            WindowManager.Instance.interactableWindow.OpenWindow();
        }
    }
    public void BlockInteract()
    {
        this.isBlocked = true;
    }
    public void UnblockInteract()
    {
        this.isBlocked = false;
    }
    public InteractionObject GetFstInteractObj()
    {
        if (scriptableObjList.Count == 0)
            return null;

        else
        {
            updateFstIdx();
            return currentInteractObj;
        }
    }
    public void AddInteractionList(InteractionObject interactionObj)
    {
        scriptableObjList.Add(interactionObj);
    }

    public void RemoveInteractionObj(InteractionObject interactionObj)
    {
        scriptableObjList.Remove(interactionObj);
    }
}
