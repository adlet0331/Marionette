using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public GameObject moveablePlayerObject;
    public BoxCollider2D moveablePlayerCollider;

    [SerializeField] private List<GameObject> interactionList;

    private void Start()
    {
        interactionList = new List<GameObject>();
    }

    public void AddInteractionList(GameObject gameObject)
    {
        interactionList.Add(gameObject);
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

    public void RemoveInteractionObj(GameObject gameObject)
    {
        interactionList.Remove(gameObject);
    }
}
