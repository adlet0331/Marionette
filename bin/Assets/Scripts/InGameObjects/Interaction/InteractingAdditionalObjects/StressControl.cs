using System;
using Managers;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    [Serializable]
    public class StressControl : AbstractInteractionObject
    {
        [SerializeField] private int stressAddNum;
        public override void Interact()
        {
            StressManager.Instance.AddStress(stressAddNum);
        }
    }
}