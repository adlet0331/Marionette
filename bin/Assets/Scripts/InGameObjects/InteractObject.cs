using System.Collections.Generic;
using UnityEngine;

/*
 * Interact 하는 주체 (주로 플레이어)
 * 
 */
public class InteractObject : MonoBehaviour
{
    [SerializeField] private bool isBlocked = false;
    //확장성을 위해 InteractionObject로
    [SerializeField] private int currentIdx;
    [SerializeField] private InteractionObjectType currentType;
    [SerializeField] private InteractionObject currentInteractObj;
    [SerializeField] private List<InteractionObject> scriptableObjList;

    private void Start()
    {
        scriptableObjList = new List<InteractionObject>();
    }
    private bool checkIfListEmpty()
    {
        if (scriptableObjList.Count == 0) return true;
        return false;
    }
    private void updateFstIdx()
    {
        if (checkIfListEmpty())
        {
            this.currentIdx = -1;
            return;
        }
        else
        {
            int idx = -1;
            float distance = -1, cnt;
            InteractionObject currentFirstObj = scriptableObjList[0];
            foreach (InteractionObject interObj in scriptableObjList)
            {
                cnt = Vector2.Distance(interObj.transform.position, gameObject.transform.position);
                if (cnt < distance || idx == -1)
                {
                    distance = cnt;
                    currentFirstObj = interObj;
                }
            }
            currentInteractObj = currentFirstObj;
            currentIdx = currentFirstObj.idx;
            currentType = currentFirstObj.objectType;
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
            WindowManager.Instance.interactableWindow.SetInteractionObject(currentIdx, currentType);
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
        if (checkIfListEmpty())
            return null;

        else
        {
            Update();

            return currentInteractObj;
        }
    }
    public void InteractWithObject()
    {
        currentInteractObj.Interact();
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
