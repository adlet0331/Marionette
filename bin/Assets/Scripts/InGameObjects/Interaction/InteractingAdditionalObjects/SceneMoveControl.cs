using System;
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
        public override bool Interact()
        {
            SceneSwitchManager.Instance.SwitchScene(data.sceneName);
            return true;
        }
    }
}