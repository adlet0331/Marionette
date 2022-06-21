using UnityEngine;

public class StartSceneButtons : MonoBehaviour
{
    public void NewGameButton()
    {
        SceneSwitchManager.Instance.SwitchScene(SceneSwitchManager.SceneName.P_Girl_room);
    }

    public void LoadGameButton()
    {

    }

    public void SettingButton()
    {

    }
}
