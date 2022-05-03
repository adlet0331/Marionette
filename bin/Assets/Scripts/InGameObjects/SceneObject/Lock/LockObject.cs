using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LockInfo
{
    public int needItemIdx;
    public string ifNotString;
    public LockInfo(int idx, string str)
    {
        needItemIdx = idx;
        ifNotString = str;
    }
}

public class LockObject : InteractionObject
{
    [SerializeField] private bool isUnLocked = false;
    [SerializeField] private LockAttatchObject lockedObject;
    public void SetIsUnLocked(bool isunlocked)
    {
        isUnLocked = isunlocked;
    }
    public override void Interact()
    {
        LockData lockData = DataBaseManager.Instance.LockDataBase.LockDataList[idx];
        List<LockInfo> needItemIndexList = lockData.lockInfoList;
        foreach (LockInfo lockInfo in needItemIndexList)
        {
            if (!InventoryManager.Instance.CheckItemIsIn(lockInfo.needItemIdx))
            {
                WindowManager.Instance.interactableWindow.SetIsLockedWindow(lockInfo.ifNotString);
                return;
            }
        }
        lockedObject.UnLock();
        return;
    }

    private void Start()
    {
        this.objectType = InteractionObjectType.LockObject;
        this.lockedObject = this.gameObject.GetComponent<LockAttatchObject>();
    }
}
