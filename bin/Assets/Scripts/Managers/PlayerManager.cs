using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public GameObject moveablePlayerObject;
    public BoxCollider2D moveablePlayerCollider;

    [SerializeField] private List<InteractionObject> interactionList;

    private void Start()
    {
        interactionList = new List<InteractionObject>();
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
        if(interactionList.Count == 0)
        {
            return -1;
        }
        else
        {
            int idx = -1;
            float distance = -1;
            float cnt;
            foreach(InteractionObject interObj in interactionList)
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
        interactionList.Add(interactionObj);
    }

    public bool CheckInteractionObj(GameObject gameObject)
    {
        if(interactionList.Find(x => x == gameObject) == null)
        {
            return false;
        }
        else
        {
            return false;
        }
    }

    public void RemoveInteractionObj(InteractionObject interactionObj)
    {
        interactionList.Remove(interactionObj);
    }
}
