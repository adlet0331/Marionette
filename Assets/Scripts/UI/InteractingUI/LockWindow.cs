using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LockWindow : UIControlWindow<string>
    {
        [SerializeField] private Text text;
        public override void Activate()
        {
            OpenWindow();
        }
        public override void DeActivate()
        {
            CloseWindow();
        }
        public override void Interact()
        {
            text.text = data;
        }
    }
}
