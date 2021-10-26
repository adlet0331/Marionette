using UnityEngine;

/* Window ��ü�� ��Ƶ� Singleton
 * 
 */
public class WindowManager : Singleton<WindowManager>
{
    public SettingWindow settingWindow;
    public ScriptWindow scriptWindow;
    public ScriptableWindow interactionWindow;
}