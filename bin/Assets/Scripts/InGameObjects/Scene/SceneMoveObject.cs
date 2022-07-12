using InGameObjects.Interaction;
using Managers;
using UnityEngine;
using static Managers.SceneSwitchManager;

namespace InGameObjects.Scene
{
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
}