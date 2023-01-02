using UnityEngine;

namespace UI
{
    public abstract class ADollTalkWindowTab : MonoBehaviour
    {
        [Header("Need To be Initialized")]
        [SerializeField] protected TypePerSelectionStrings typePerSelectionStrings;
        public abstract void OpenTab();
        public abstract void CloseTab();
        public abstract void GetInput(InputType input);
    }
}