using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

/* Window 매니저 Singleton
 * 
 */
namespace Managers
{
    public class WindowManager : Singleton<WindowManager>
    {
        [SerializeField] private string currentOpenWindowTypeString;
        [SerializeField] private List<WindowObject> currentOpenWindowList;
        public WindowObject CurrentOpenWindow => currentOpenWindowList[^1];
        public string CurrentOpenWindowTypeString => currentOpenWindowTypeString;
        public void SetCurrentWindow(WindowObject windowObject)
        {
            currentOpenWindowList.Add(windowObject);
            currentOpenWindowTypeString = windowObject.GetType().ToString();
        }

        public void RemoveWindow(WindowObject windowObject)
        {
            currentOpenWindowList.Remove(windowObject);
            if (currentOpenWindowList.Count == 0)
            {
                currentOpenWindowTypeString = "";
                return;
            }
            currentOpenWindowTypeString = currentOpenWindowList[^1].GetType().ToString();
        }
        
        public WindowObject sLWindow;
        public SettingWindow settingWindow;
        public ProfileWindow profileWindow;
        public InteractableWindow interactableWindow;
        public ScriptWindow scriptWindow;
        public ItemGotWindow itemGotWindow;
        public LockWindow lockWindow;
        public InventoryWindow inventoryWindow;
        public DollTalkWindow dollTalkWindow;
        public DollTalkSelectionWindow dollTalkSelectionWindow;
		public ChooseWindow chooseWindow;
    }
}