using UnityEngine;

/* Window ��ü�� ��Ƶ� Singleton
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