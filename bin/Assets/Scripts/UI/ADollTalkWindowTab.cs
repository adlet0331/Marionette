using System;
using UnityEngine;

namespace UI
{
    public abstract class ADollTalkWindowTab : MonoBehaviour
    {
        [SerializeField] protected DollTalkWindow dollTalkWindow;
        [SerializeField] protected DollTalkWindowType currentType;
        [SerializeField] protected TypePerSelectionStrings typePerSelectionStrings;
        public abstract void OpenTab();
        public abstract void GetInput(InputType input);

        private void Awake()
        {
            dollTalkWindow = GetComponentInParent<DollTalkWindow>();
        }
    }
}