using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

/* Window 매니저 Singleton
 * 
 */
namespace Managers
{
    [Serializable]
    public class WindowsInstances
    {
        [Header("Need To Be Initiated MANUALLY In Unity")]
        public Image fadeInOutBoard;
        
        [Header("Setting Window Objects")]
        public SettingWindow settingWindow;
        [Header("Interaction Window Objects")]
        public InteractableWindow interactableWindow;
        public ScriptWindow scriptWindow;
        public ChooseWindow chooseWindow;
        public ItemGotWindow itemGotWindow;
        public LockWindow lockWindow;
        public StellaGotWindow stellaGotWindow;
        public CutSceneWindow cutSceneWindow;
        [Header("Doll Talk Window")]
        public DollTalkWindow dollTalkWindow;
        public DollTalkSelectionWindow dollTalkSelectionWindowInnerTab;
        [Header("Non Window Objects")]
        public ProfileWindow profileWindow;
        
        public void GetAllWindowsInCanvas()
        {
            var canvas = GameObject.Find("Canvas");
            
            settingWindow = canvas.GetComponentInChildren<SettingWindow>(true);
            interactableWindow = canvas.GetComponentInChildren<InteractableWindow>(true);
            scriptWindow = canvas.GetComponentInChildren<ScriptWindow>(true);
            chooseWindow = canvas.GetComponentInChildren<ChooseWindow>(true);
            itemGotWindow = canvas.GetComponentInChildren<ItemGotWindow>(true);
            lockWindow = canvas.GetComponentInChildren<LockWindow>(true);
            stellaGotWindow = canvas.GetComponentInChildren<StellaGotWindow>(true);
            cutSceneWindow = canvas.GetComponentInChildren<CutSceneWindow>(true);
            dollTalkWindow = canvas.GetComponentInChildren<DollTalkWindow>(true);
            dollTalkSelectionWindowInnerTab = canvas.GetComponentInChildren<DollTalkSelectionWindow>(true);
            profileWindow = canvas.GetComponentInChildren<ProfileWindow>(true);
        }
    }
    
    [Serializable]
    public class WindowManager : AGameManager
    {
        [SerializeField] private string currentOpenWindowType;
        [SerializeField] private List<WindowObject> currentOpenWindowList;
        public WindowObject CurrentOpenWindow => currentOpenWindowList[^1];
        public string CurrentOpenWindowType => currentOpenWindowType;
        
        [SerializeField] private WindowsInstances windowsInstances;
        public WindowsInstances WindowsInstances => windowsInstances;
        
        public void SetCurrentWindow(WindowObject windowObject)
        {
            currentOpenWindowList.Add(windowObject);
            currentOpenWindowType = windowObject.GetType().ToString();
        }

        public void RemoveWindow(WindowObject windowObject)
        {
            currentOpenWindowList.Remove(windowObject);
            if (currentOpenWindowList.Count == 0)
            {
                currentOpenWindowType = "";
                return;
            }
            currentOpenWindowType = currentOpenWindowList[^1].GetType().ToString();
        }

        public override void Start()
        {
            windowsInstances.GetAllWindowsInCanvas();
        }
    }
}