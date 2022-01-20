using UnityEngine;

/* Window °´Ã¼µé ¸ð¾ÆµÐ Singleton
 * 
 */
public class WindowManager : Singleton<WindowManager>
{
    public WindowObject sLWindow;
    public SettingWindow settingWindow;
    public ScriptWindow scriptWindow;
    public ScriptableWindow interactionWindow;
    public InventoryWindow inventoryWindow;
}