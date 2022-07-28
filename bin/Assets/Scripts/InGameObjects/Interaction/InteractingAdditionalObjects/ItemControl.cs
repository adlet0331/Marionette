﻿using System;
using DataBaseScripts;
using Managers;
using UI;
using UnityEditor.PackageManager.UI;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    [Serializable]
    public class ItemControl : IInteractionObject<ItemControlData>
    {
        [SerializeField] private bool interacted = false;
        public override bool Interact()
        {
            if (!interacted)
            {
                WindowManager.Instance.itemGotWindow.OpenWithData(this.data);
                interacted = true;
                return false;
            }
            else
            {
                WindowManager.Instance.itemGotWindow.CloseWindow();
                interacted = false;
                return true;
            }
        }
    }
}