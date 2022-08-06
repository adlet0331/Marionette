using System;
using Cysharp.Threading.Tasks;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    [Serializable]
    public class CameraWalkData
    {
        
    }
    public class CameraWalkControl : IInteractionObject<CameraWalkData>
    {
        public override async UniTask<bool> Interact()
        {
            
            return true;
        }
    }
}