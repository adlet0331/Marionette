using DataBaseScripts.Base;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    public abstract class IInteractionObject<TDataBaseType> : MonoBehaviour
    {
        public TDataBaseType data;
        /// <summary>
        /// Return True If Interaction is End
        /// </summary>
        /// <returns></returns>
        public abstract bool Interact();
    }
}   