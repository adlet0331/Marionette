using DataBaseScripts.Base;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    public abstract class IInteractionObject<DataBaseType> : MonoBehaviour where DataBaseType : DataType
    {
        public DataBaseType data;
        /// <summary>
        /// Return True If Interaction is End
        /// </summary>
        /// <returns></returns>
        public abstract bool Interact();
    }
}   