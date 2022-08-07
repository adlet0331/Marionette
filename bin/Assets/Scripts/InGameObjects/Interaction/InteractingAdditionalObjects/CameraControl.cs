using System;
using Cysharp.Threading.Tasks;
using DataBaseScripts;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    public class CameraControl : IInteractionObject<CameraControlData>
    {
        public override async UniTask<bool> Interact()
        {
            

            return true;
        }
    }
}