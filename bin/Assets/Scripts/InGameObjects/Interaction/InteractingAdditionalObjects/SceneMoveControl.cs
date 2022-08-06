using System;
using Cysharp.Threading.Tasks;
using DataBaseScripts.Base;
using Managers;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    [Serializable]
    public class SceneMoveData
    {
        public SceneSwitchManager.SceneName sceneName;
    }
    public sealed class SceneMoveControl : IInteractionObject<SceneMoveData>
    {
        public override async UniTask<bool> Interact()
        {
            SceneSwitchManager.Instance.SwitchScene(data.sceneName);
            return true;
        }
    }
}