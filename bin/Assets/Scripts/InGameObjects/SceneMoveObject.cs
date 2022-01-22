using UnityEngine;
using UnityEngine.SceneManagement;
using static SceneSwitchManager;

public class SceneMoveObject : ColliderObject
{
    [SerializeField] private SceneName sceneName;
    public override void InteractIn()
    {
        SceneSwitchManager.Instance.SwitchScene(sceneName);
    }

    public override void InteractOut()
    {
        return;
    }
}
