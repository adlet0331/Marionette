using System;
using DataBaseScripts.Base;
using InGameObjects.Interaction.InteractingAdditionalObjects;
using Managers;
using UnityEngine;

namespace InGameObjects.Object
{
    [Serializable]
    public class AnimaObjectData : DataType
    {
        public int upCount = 1;
    }
    public class AnimaObject : IInteractionObject<AnimaObjectData>
    {
        public override bool Interact()
        {
            AnimaAbilityManager.Instance.IncrementAnimaAbility(data.idx, data.upCount);
            return true;
        }
    }
}
