using DataBaseScripts.Base;
using UnityEngine;

namespace UI
{
    public abstract class UIControlWindow<T> : WindowObject
    {
        [SerializeField] protected T data;
        public override void Activate()
        {
            OpenWindow();
        }

        public abstract void OpenWithData(T d);
    }
}