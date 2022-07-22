using Managers;
using UnityEngine;

namespace UI
{
    public class StartSceneButtons : MonoBehaviour
    {
        public void NewGameButton()
        {
            SLManager.Instance.InitSaveData();
            SceneSwitchManager.Instance.SwitchScene(SceneSwitchManager.SceneName.Girl_room);
        }

        public void LoadGameButton()
        {

        }

        public void SettingButton()
        {
            WindowManager.Instance.settingWindow.Activate();
        }
    }
}
