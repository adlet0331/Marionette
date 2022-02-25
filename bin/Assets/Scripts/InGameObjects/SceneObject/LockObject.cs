using System;
using UnityEngine;

[Serializable]
public class LockInfo
{
    public int needItemIdx;
    public string ifNotString;
}

public class LockObject : InteractionObject
{
    [SerializeField] private LockInfo[] needItemIndexList;
    public override void Interact()
    {
        foreach (LockInfo lockInfo in needItemIndexList)
        {
            if (!InventoryManager.Instance.CheckItemIsIn(lockInfo.needItemIdx))
            {

            }
        }
    }

    private void Start()
    {
        this.objectType = InteractionObjectType.LockObject;
    }
}
