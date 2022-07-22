using DataBaseScripts.Base;
using UnityEngine;

namespace UI
{
    public abstract class UIControlWindow<T> : WindowObject
    {
        [SerializeField] protected T data;
        public void SetData(T d)
        {
            this.data = d;
        }
    }
}