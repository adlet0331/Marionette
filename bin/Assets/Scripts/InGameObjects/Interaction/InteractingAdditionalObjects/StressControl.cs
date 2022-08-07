using Cysharp.Threading.Tasks;
using DataBaseScripts;
using Managers;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    public class StressControl : IInteractionObject<StressControlData>
    {
        public override async UniTask<bool> Interact()
        {
            StressManager.Instance.AddStress(data.stressAdd);
            return true;
        }
    }
}