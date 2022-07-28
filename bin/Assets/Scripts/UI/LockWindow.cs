using System;
using DataBaseScripts;
using InGameObjects.Interaction.InteractingAdditionalObjects;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LockWindow : UIControlWindow<string>
    {
        [SerializeField] private Text text;
        public override void OpenWithData(string printString)
        {
            text.text = printString;
            Activate();
        }
    }
}
