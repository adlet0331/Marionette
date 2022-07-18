using System;
using DataBaseScripts;
using Managers;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    public class ScriptControl : IInteractionObject<ScriptData>
    {
        [SerializeField] private int currentIndex;

        private void Awake()
        {
            currentIndex = 0;
        }
        public override bool Interact()
        {
            if (currentIndex == 0)
            {
                WindowManager.Instance.scriptWindow.Activate();
                WindowManager.Instance.scriptWindow.SetData(data);
            }
            int nextIndex  = WindowManager.Instance.scriptWindow.PressSpace();
            if (nextIndex == -1)
            {
                currentIndex = 0;
                return true;
            }
            currentIndex = nextIndex;
            return false;
        }
    }
}
