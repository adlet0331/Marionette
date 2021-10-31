using System.Collections.Generic;
using UnityEngine;

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
            WindowManager.Instance.interactionWindow.gameObject.SetActive(false);
        }
        else
        {
            WindowManager.Instance.interactionWindow.SetInteractionObject(idx);
            WindowManager.Instance.interactionWindow.gameObject.SetActive(true);
        }
    }
    public InteractionObject GetFstScrObj()
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

    public void AddInteractionList(InteractionObject interactionObj)
    {
        scriptableObjList.Add(interactionObj);
    }

    public void RemoveInteractionObj(InteractionObject interactionObj)
    {
        scriptableObjList.Remove(interactionObj);
    }
}
