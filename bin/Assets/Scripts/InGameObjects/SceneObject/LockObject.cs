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
    [SerializeField] private bool isUnLocked = false;
    [SerializeField] private string nameStr = "";
    [SerializeField] private LockInfo[] needItemIndexList;
    [SerializeField] private InteractionObject lockedObject;
    public void SetIsUnLocked(bool isunlocked)
    {
        isUnLocked = isunlocked;
    }
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
        unLock();
        return;
    }

    private void unLock()
    {
        lockedObject.Interact();
        return;
    }

    private void Start()
    {
        this.objectType = InteractionObjectType.LockObject;
    }
}
