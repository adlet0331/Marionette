using Cysharp.Threading.Tasks;
using DataBaseScripts;
using Managers;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class LockControl : ADataInteractionObjectWithUI<LockData, string>
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
            // UnLocked 되어 있으면 바로 리턴
            if (UnLocked && !UIOpened)
                return true;
            
            // 열려 있다면 Close 해주기
            if (UIOpened)
            {
                UIOpened = false;
                WindowManager.Instance.lockWindow.CloseWindow();
                return true;
            }
            UnLocked = true;
            
            // 요구하는 아이템이 모두 있는지 체크
            for (int i = 0; i < data.needItemList.Count; i++)
            {
                // Item
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
            // 요구하는 아이템 실제로 삭제
            string uiString;
            if (UnLocked)
            {
                for (int i = 0; i < data.needItemList.Count; i++)
                {
                    if (data.needTypeList[i] == 0)
                    {
                        // 있는지 체크만 하는 걸로 구현. 나중에 없애는것도 구현
                        InventoryManager.Instance.DeleteItem(data.needItemList[i], data.needItemNumList[i]);
                    }
                }
                uiString = data.unLockString;
            }
            else
            {
                uiString = data.lockedString;
            }
            
            // UI 오픈
            UIOpened = true;
            UIWindow.InteractWithData(uiString);
            return false;
        }
    }
}