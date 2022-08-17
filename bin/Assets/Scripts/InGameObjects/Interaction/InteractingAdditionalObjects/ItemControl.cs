using System;
using System.IO;
using Cysharp.Threading.Tasks;
using DataBaseScripts;
using Managers;
using UI;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    [Serializable]
    public class ItemControl : IInteractionObjectWithUI<ItemControlData, ItemGotUIData>
    {
        [SerializeField] private bool interacted;
        private ItemDataBase itemDataBase;
        protected override void GetUIWindowAndInit()
        {
            interacted = false;
            itemDataBase = DataBaseManager.Instance.itemDataBase; 
            UIWindow = WindowManager.Instance.itemGotWindow;
        }
        public override async UniTask<bool> Interact()
        {
            if (!interacted)
            {
                int currentIndex = 0;
                for (int i = 0; i < data.itemIdxList.Count; i++)
                {
                    if (data.isAddList[i])
                    {
                        currentIndex = data.itemIdxList[i];
                        break;
                    }
                }
                UIData.name = itemDataBase.dataKeyDictionary[currentIndex].name;
                UIData.script = data.getDescription;
                UIData.sprite = Resources.Load<Sprite>(Path.Combine("Sprites", "Items",
                    itemDataBase.dataKeyDictionary[currentIndex].spriteName));
                
                UIWindow.InteractWithData(UIData);
                interacted = true;
                return false;
            }
            else
            {
                UIWindow.CloseWindow();
                interacted = false;
                return true;
            }
        }

    }
}