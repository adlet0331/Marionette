using System;
using Cysharp.Threading.Tasks;
using DataBaseScripts.Base;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    [Serializable]
    public class AnimationData : DataType
    {
        
    }
    public class AnimationControl : ADataInteractionObject<AnimationData>
    {
        public override async UniTask<bool> Interact()
        {
            return true;
        }
    }
}