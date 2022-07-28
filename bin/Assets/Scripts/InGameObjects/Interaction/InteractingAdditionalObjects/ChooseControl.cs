using DataBaseScripts;
using Managers;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    public class ChooseControl : IInteractionObject<ChooseData>
    {
        public override bool Interact()
        {
            WindowManager.Instance.chooseWindow.OpenWithData(data);
            return false;
        }
    }
}