using DataBaseScripts;
using Managers;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    public class ChooseControl : IInteractionObject<ChooseData>
    {
        public override bool Interact()
        {
            WindowManager.Instance.chooseWindow.SetData(data);
            WindowManager.Instance.chooseWindow.Activate();
            return false;
        }
    }
}