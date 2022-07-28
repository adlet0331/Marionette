using DataBaseScripts.Base;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    public abstract class IInteractionObject<T>  : MonoBehaviour where T : DataType
    {
        public T data;
        /// <summary>
        /// Return True If Interaction is End
        /// </summary>
        /// <returns></returns>
        public abstract bool Interact();
    }
}