using DataBaseScripts.Base;
using UnityEngine;

namespace UI
{
    public abstract class UIControlWindow<T> : WindowObject
    {
        [SerializeField] protected T data;
        public abstract void DeActivate();
        public abstract void Interact();
        public void InteractWithData(T d)
        {
            Activate();
            this.data = d;
            Interact();
        }
    }
}