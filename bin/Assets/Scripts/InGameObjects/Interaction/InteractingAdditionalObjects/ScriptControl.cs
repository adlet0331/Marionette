using DataBaseScripts;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    public class ScriptControl : IInteractionObject<ScriptData>
    {
        public ScriptData scriptData;
        public override void Interact()
        {
            throw new System.NotImplementedException();
        }
    }
}
