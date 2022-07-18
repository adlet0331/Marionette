using UI;
using UnityEngine;

/* Window 매니저 Singleton
 * 
 */
namespace Managers
{
    public class WindowManager : Singleton<WindowManager>
    {
        [SerializeField] public int currentOpenWindowNum;
        public WindowObject sLWindow;
        public SettingWindow settingWindow;
        public ProfileWindow profileWindow;
        public InteractableWindow interactableWindow;
        public ScriptWindow scriptWindow;
        public ItemGotWindow itemGotWindow;
        public LockWindow lockWindow;
        public InventoryWindow inventoryWindow;
        public DollTalkWindow dollTalkWindow;
		public ChooseWindow chooseWindow;
    }
}