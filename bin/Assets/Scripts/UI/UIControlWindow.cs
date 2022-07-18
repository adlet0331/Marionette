using DataBaseScripts.Base;
using UnityEngine;

namespace UI
{
    public abstract class UIControlWindow<T> : WindowObject where T : DataType
    {
        [SerializeField] protected T data;
        public void SetData(T d)
        {
            this.data = d;
        }
    }
}