using UnityEngine;

/* Window ��ü�� ��Ƶ� Singleton
 * 
 */
public class WindowManager : Singleton<WindowManager>
{
    public WindowObject sLWindow;
    public SettingWindow settingWindow;
    public ProfileWindow profileWindow;
    public InteractableWindow interactableWindow;
    public ScriptWindow scriptWindow;
    public ItemGotWindow itemGotWindow;
    public LockWindow lockWindow;
    public InventoryWindow inventoryWindow;
    public ItemSelectionWindow itemSelectionWindow;
    public ItemInfoWindow itemInfoWindow;
}