
using UnityEngine;
using static SceneSwitchManager;

public class SceneMoveAttatchObject : LockAttatchObject
{
    [SerializeField] private SceneName switchSceneName;
    public override void UnLock()
    {
        SceneSwitchManager.Instance.SwitchScene(switchSceneName);
    }
}
