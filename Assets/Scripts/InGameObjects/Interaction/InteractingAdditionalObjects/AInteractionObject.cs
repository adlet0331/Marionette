using Cysharp.Threading.Tasks;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    public abstract class AInteractionObject : MonoBehaviour
    {
        /// <summary>
        /// Return True If Interaction is End
        /// </summary>
        /// <returns></returns>
        public abstract UniTask<bool> Interact();
    }
}