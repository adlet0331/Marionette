using System;
using System.IO;
using Cysharp.Threading.Tasks;
using DataBaseScripts;
using Managers;
using UI;
using Unity.VisualScripting;
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
                InventoryManager.Instance.AddItem(data.itemIdxList[currentIndex], data.itemNumList[currentIndex]);

                UIData.name = itemDataBase.dataKeyDictionary[currentIndex].name;
                UIData.script = data.getDescription;
                UIData.sprite = Resources.Load<Sprite>(Path.Combine("Sprites", "Items",
                    itemDataBase.dataKeyDictionary[currentIndex].spriteName));

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