using System.Collections.Generic;
using UnityEngine;
/* 플레이어 관리
 * 
 * 범위내에 있는 Scriptable Object를 List로 가지고 있음 
 * -> 가장 가까운 애가 Scriptable Window로 뜸
 * 
 */
public class PlayerManager : Singleton<PlayerManager>
{
    public GameObject moveablePlayerObject;
    public BoxCollider2D moveablePlayerCollider;

    [SerializeField] private InteractionObject currentInteractObj;
    [SerializeField] private List<InteractionObject> scriptableObjList;

    private void Start()
    {
        scriptableObjList = new List<InteractionObject>();
    }

    private void Update()
    {
        int idx = GetObjectIDX();
        if(idx == -1)
        {
            WindowManager.Instance.interactionWindow.gameObject.SetActive(false);
        }
        else
        {
            WindowManager.Instance.interactionWindow.gameObject.SetActive(true);
            WindowManager.Instance.interactionWindow.SetInteractionObject(idx);
        }
    }

    public int GetObjectIDX()
    {
        if(scriptableObjList.Count == 0)
        {
            return -1;
        }
        else
        {
            int idx = -1;
            float distance = -1;
            float cnt;
            foreach(InteractionObject interObj in scriptableObjList)
            {
                cnt = Vector2.Distance(interObj.transform.position, moveablePlayerObject.transform.position);
                if(cnt < distance || idx == -1)
                {
                    distance = cnt;
                    idx = interObj.GetIdx();
                }
            }
            return idx;
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
