using Managers;
using UnityEngine;

/*
 * 게임 내 Window 오브젝트 
 */
namespace UI
{
    public abstract class WindowObject : MonoBehaviour {
        //For Opening Window
        [SerializeField] private bool moveableWhileOpen = false;
        [SerializeField] private bool inputableWhileOpen = true;
        [SerializeField] private bool profileShowingWhileOpen = false;
        public bool IsOpened
        {
            get => this.gameObject.activeSelf;
        }
        public abstract void Activate();
        protected void OpenWindow()
        {
            if (gameObject.activeSelf)
                return;
            gameObject.SetActive(true);
            transform.SetAsLastSibling();
            WindowManager.Instance.SetCurrentWindow(this);
            InputManager.Instance.SetOptions(moveableWhileOpen, inputableWhileOpen);
            if (!profileShowingWhileOpen)
            {
                WindowManager.Instance.profileWindow.gameObject.SetActive(false);
            }
        }
        // ReSharper disable Unity.PerformanceAnalysis
        public void CloseWindow()
        {
            gameObject.SetActive(false);
            WindowManager.Instance.RemoveWindow(this);
            if (WindowManager.Instance.CurrentOpenWindowTypeString == "")
            {
                SceneSwitchManager.Instance.returnToOriginalSceneSetting();
            }
            if (!profileShowingWhileOpen)
            {
                WindowManager.Instance.profileWindow.gameObject.SetActive(true);
            }
        }
    }
}
