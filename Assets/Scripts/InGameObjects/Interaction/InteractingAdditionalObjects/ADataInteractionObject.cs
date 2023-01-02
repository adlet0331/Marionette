using Cysharp.Threading.Tasks;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    public abstract class ADataInteractionObject<TDataBaseType> : AInteractionObject
    {
        public TDataBaseType data;
    }
}   