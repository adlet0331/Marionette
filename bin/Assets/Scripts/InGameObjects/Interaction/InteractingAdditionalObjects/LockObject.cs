using DataBaseScripts;
using Managers;
using Unity.VisualScripting;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class LockObject : IInteractionObject<LockData>
    {
        [SerializeField] private bool UIOpened = false;
        [SerializeField] private bool UnLocked = false;
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
                if (data.needTypeList[i] == "Item")
                {
                    // 일단 하나 없애는걸로 구현
                    if (InventoryManager.Instance.GetItemNumInInventory(data.needItemList[i]) < data.needItemNum[i])
                    {
                        UnLocked = false;
                        break;
                    }
                }
            }

            if (UnLocked)
            {
                UnLocked = true;
                for (int i = 0; i < data.needItemList.Count; i++)
                {
                    if (data.needTypeList[i] == "Item")
                    {
                        // 일단 하나 없애는걸로 구현
                        InventoryManager.Instance.DeleteItem(data.needItemList[i], data.needItemNum[i]);
                    }
                }
                WindowManager.Instance.lockWindow.OpenWithData(data.lockedString);
            }
            else
            {
                WindowManager.Instance.lockWindow.OpenWithData(data.unLockString);
            }
            return false;
        }
    }
}