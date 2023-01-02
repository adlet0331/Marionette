using System;
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
    [Serializable]
    public class ItemGotUIData
    {
        public string name;
        public string script;
        public Sprite sprite;
    }
    public class ItemGotWindow : UIControlWindow<ItemGotUIData>
    {
        [SerializeField] private Text nameText;
        [SerializeField] private Text infoText;
        [SerializeField] private Image itemImage;
        public override void Activate()
        {
            OpenWindow();
        }
        public override void DeActivate()
        {
            CloseWindow();
        }
        public override void Interact()
        {
            nameText.text = data.name;
            infoText.text = data.script;
            itemImage.sprite = data.sprite;
        }
    }
}
