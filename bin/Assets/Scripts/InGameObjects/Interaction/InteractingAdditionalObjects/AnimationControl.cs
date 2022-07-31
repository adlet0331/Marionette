using System;
using DataBaseScripts.Base;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    [Serializable]
    public class AnimationData : DataType
    {
        
    }
    public class AnimationControl : IInteractionObject<AnimationData>
    {
        public override bool Interact()
        {
            return true;
        }
    }
}