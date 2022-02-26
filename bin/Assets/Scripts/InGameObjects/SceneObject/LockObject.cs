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
    [SerializeField] private string nameStr = "";
    [SerializeField] private LockInfo[] needItemIndexList;
    public override void Interact()
    {
        foreach (LockInfo lockInfo in needItemIndexList)
        {
            if (!InventoryManager.Instance.CheckItemIsIn(lockInfo.needItemIdx))
            {
                WindowManager.Instance.interactableWindow.SetIsLockedWindow(nameStr, lockInfo.ifNotString);
                return;
            }
        }
        return;
    }

    private void UnLock()
    {

        return;
    }

    private void Start()
    {
        this.objectType = InteractionObjectType.LockObject;
    }
}
