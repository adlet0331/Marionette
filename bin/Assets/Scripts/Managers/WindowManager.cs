using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

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
        public Image FadeInOutBoard;
    }
}