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
        [SerializeField] private int currentIndex = 0;
        private ItemDataBase itemDataBase;
        protected override void GetUIWindowAndInit()
        {
            currentIndex = 0;
            itemDataBase = DataBaseManager.Instance.itemDataBase; 
            UIWindow = WindowManager.Instance.itemGotWindow;
        }
        public override async UniTask<bool> Interact()
        {
            if (currentIndex < data.itemIdxList.Count)
            {
                var idx = data.itemIdxList[currentIndex];
                var itemNum = data.itemNumList[currentIndex];
                InventoryManager.Instance.AddItem(idx, itemNum);

                UIData.name = itemDataBase.dataKeyDictionary[idx].name;
                UIData.script = data.getDescription;
                UIData.sprite = Resources.Load<Sprite>(Path.Combine("Sprites", "Items",
                    itemDataBase.dataKeyDictionary[idx].spriteName));

                currentIndex++;
                
                UIWindow.InteractWithData(UIData);
                return false;
            }
            else
            {
                UIWindow.CloseWindow();
                currentIndex = 0;
                return true;
            }
        }

    }
}