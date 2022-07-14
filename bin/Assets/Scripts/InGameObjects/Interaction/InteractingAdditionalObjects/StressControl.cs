using DataBaseScripts;
using Managers;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    public class StressControl : IInteractionObject<StressControlData>
    {
        [SerializeField] private int stressAddNum;
        public override void Interact()
        {
            StressManager.Instance.AddStress(stressAddNum);
        }
    }
}