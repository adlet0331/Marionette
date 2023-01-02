using Cysharp.Threading.Tasks;
using DataBaseScripts;
using Managers;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    public class StressControl : ADataInteractionObject<StressControlData>
    {
        public override async UniTask<bool> Interact()
        {
            StressManager.Instance.AddStress(data.stressAdd);
            return true;
        }
    }
}