using System;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    [Serializable]
    public class CameraWalkData
    {
        
    }
    public class CameraWalkControl : IInteractionObject<CameraWalkData>
    {
        public override bool Interact()
        {

            return true;
        }
    }
}