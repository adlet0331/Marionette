using System;
using Cysharp.Threading.Tasks;
using DataBaseScripts.Base;
using InGameObjects.Interaction.InteractingAdditionalObjects;
using Managers;

namespace InGameObjects.Object
{
    [Serializable]
    public class AnimaObjectData : DataType
    {
        public int upCount = 1;
    }
    public class AnimaControl : ADataInteractionObjectWithUI<AnimaObjectData, AnimaObjectData>
    {
        protected override void GetUIWindowAndInit()
        {
            
        }
        public override async UniTask<bool> Interact()
        {
            StellaManager.Instance.IncrementStella(data.idx, data.upCount);
            return true;
        }
    }
}
