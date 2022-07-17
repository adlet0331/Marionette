using DataBaseScripts;
using Managers;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    public class StressControl : IInteractionObject<StressControlData>
    {
        public override bool Interact()
        {
            StressManager.Instance.AddStress(data.stressAdd);
            return true;
        }
    }
}