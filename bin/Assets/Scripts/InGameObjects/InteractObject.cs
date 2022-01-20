using System.Collections.Generic;
using UnityEngine;

/*
 * Interact 하는 주체 (주로 플레이어)
 * 
 */
public class InteractObject : MonoBehaviour
{
    //확장성을 위해 InteractionObject로
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
    private int updateFstIdx()
    {
        if (checkIfListEmpty())
        {
            return -1;
        }
        else
        {
            int idx = -1;
            float distance = -1;
            float cnt;
            foreach (InteractionObject interObj in scriptableObjList)
            {
                cnt = Vector2.Distance(interObj.transform.position, gameObject.transform.position);
                if (cnt < distance || idx == -1)
                {
                    distance = cnt;
                    idx = interObj.GetIdx();
                }
            }
            currentInteractObj = scriptableObjList[0];
            return idx;
        }
    }
    private void Update()
    {
        int idx = updateFstIdx();
        if (idx == -1)
        {
            WindowManager.Instance.interactableWindow.gameObject.SetActive(false);
        }
        else
        {
            WindowManager.Instance.interactableWindow.SetInteractionObject(idx);
            WindowManager.Instance.interactableWindow.gameObject.SetActive(true);
        }
    }
    public InteractionObject GetFstInteractObj()
    {
        if (checkIfListEmpty())
            return null;
        else
        {
            Update();
            InteractionObject obj = scriptableObjList[0];

            return obj;
        }
    }
    public void InteractWithObject()
    {
        
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
