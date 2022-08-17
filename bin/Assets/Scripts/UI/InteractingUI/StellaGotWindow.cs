using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public class StellaGotUIData
    {
        public string name;
        public string script;
        public Sprite sprite;
    }
    public class StellaGotWindow : UIControlWindow<StellaGotUIData>
    {
        [SerializeField] private Text nameText;
        [SerializeField] private Text infoText;
        [SerializeField] private Image stellaImage;
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
            stellaImage.sprite = data.sprite;
        }
    }
}