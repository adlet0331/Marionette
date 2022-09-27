using DataBaseScripts.Base;
using UI;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    public abstract class ADataInteractionObjectWithUI<DataBaseType, UIDataType>  : ADataInteractionObject<DataBaseType> where DataBaseType : DataType
    {
        [SerializeField] protected UIControlWindow<UIDataType>  UIWindow;
        [SerializeField] protected UIDataType UIData;
        private void Start()
        {
            GetUIWindowAndInit();
            if (object.ReferenceEquals(UIWindow, null))
            {
                Debug.LogAssertion($"{this.GetType().ToString()}: Type {UIWindow.GetType()} is not initialized.");
            }
        }
        protected abstract void GetUIWindowAndInit();
    }
}