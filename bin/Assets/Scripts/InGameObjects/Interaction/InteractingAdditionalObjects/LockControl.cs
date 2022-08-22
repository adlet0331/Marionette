﻿using Cysharp.Threading.Tasks;
using DataBaseScripts;
using Managers;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class LockControl : IInteractionObjectWithUI<LockData, string>
    {
        [SerializeField] private bool UIOpened;
        [SerializeField] public bool UnLocked = false;
        
        protected override void GetUIWindowAndInit()
        {
            UIOpened = false;
            UnLocked = false;
            UIWindow = WindowManager.Instance.lockWindow;
        }
        public override async UniTask<bool> Interact()
        {
            if (UIOpened)
            {
                UIOpened = false;
                WindowManager.Instance.lockWindow.CloseWindow();
                if (UnLocked)
                    gameObject.SetActive(false);
                return true;
            }
            UIOpened = true;
            UnLocked = true;
            for (int i = 0; i < data.needItemList.Count; i++)
            {
                if (data.needTypeList[i] == 0)
                {
                    // 일단 하나 없애는걸로 구현
                    if (data.needItemList[i] == -1 || 
                        InventoryManager.Instance.GetItemNumInInventory(data.needItemList[i]) < data.needItemNumList[i])
                    {
                        UnLocked = false;
                        break;
                    }
                }
            }

            if (UnLocked)
            {
                for (int i = 0; i < data.needItemList.Count; i++)
                {
                    if (data.needTypeList[i] == 0)
                    {
                        // 있는지 체크만 하는 걸로 구현. 나중에 없애는것도 구현
                        //InventoryManager.Instance.DeleteItem(data.needItemList[i], data.needItemNumList[i]);
                    }
                }
                UIWindow.InteractWithData(data.unLockString);
            }
            else
            {
                UIWindow.InteractWithData(data.lockedString);
            }
            return false;
        }
    }
}