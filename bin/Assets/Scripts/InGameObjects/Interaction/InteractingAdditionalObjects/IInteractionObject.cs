using DataBaseScripts.Base;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    public abstract class IInteractionObject<T>  : MonoBehaviour where T : DataType
    {
        public T data;
        public abstract void Interact();
    }
}