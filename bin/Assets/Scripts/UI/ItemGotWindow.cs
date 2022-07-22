using System.IO;
using DataBaseScripts;
using DataBaseScripts.Base;
using InGameObjects.Interaction.InteractingAdditionalObjects;
using Managers;
using UnityEngine;
using UnityEngine.UI;

/*
 * 아이템을 얻었을 때 나오는 창
 * 
 * 5초 동안 냅두면 닫힘
 * 
 */
namespace UI
{
    public class ItemGotWindow : UIControlWindow<ItemControlData>
    {
        [SerializeField] private Text nameText;
        [SerializeField] private Text infoText;
        [SerializeField] private Image itemImage;
        [SerializeField] private bool isOpened = false;

        public override void Activate()
        {
            if (!isOpened)
            {
                OpenWindow();
                for (int i = 0; i < data.itemIdxList.Count; i++)
                {
                    if (data.isAddList[i])
                    {
                        var itemData = DataBaseManager.Instance.itemDataBase.dataList[data.itemIdxList[i]];
                        nameText.text = itemData.name;
                        infoText.text = itemData.itemInfo;
                        itemImage.sprite = Resources.Load<Sprite>(Path.Combine("Sprites", "Items", itemData.spriteName));
                        break;
                    }
                }
                isOpened = true;
            }
            else
            {
                CloseWindow();
                isOpened = false;
            }
        }
    }
}
