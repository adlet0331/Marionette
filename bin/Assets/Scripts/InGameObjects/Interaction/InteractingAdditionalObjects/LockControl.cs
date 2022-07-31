using DataBaseScripts;
using Managers;
using Unity.VisualScripting;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class LockControl : IInteractionObjectWithUI<LockData, string>
    {
        [SerializeField] private bool UIOpened;
        [SerializeField] private bool UnLocked = false;
        
        protected override void GetUIWindowAndInit()
        {
            UIOpened = false;
            UnLocked = false;
            UIWindow = WindowManager.Instance.lockWindow;
        }
        public override bool Interact()
        {
            if (UIOpened)
            {
                UIOpened = false;
                WindowManager.Instance.lockWindow.CloseWindow();
                if (UnLocked)
                {
                    gameObject.SetActive(false);
                }
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
                        InventoryManager.Instance.GetItemNumInInventory(data.needItemList[i]) > data.needItemNumList[i])
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
                        // 일단 하나 없애는걸로 구현
                        InventoryManager.Instance.DeleteItem(data.needItemList[i], data.needItemNumList[i]);
                    }
                }
                UIWindow.InteractWithData(data.lockedString);
            }
            else
            {
                UIWindow.InteractWithData(data.unLockString);
            }
            return false;
        }
    }
}