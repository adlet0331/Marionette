﻿using Cysharp.Threading.Tasks;
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
            GamePlayManager.Instance.SetCurrentWindow(this);
            GamePlayManager.Instance.SetInputOptions(moveableWhileOpen, inputableWhileOpen);
            if (!profileShowingWhileOpen)
            {
                GamePlayManager.Instance.WindowsInstances.profileWindow.gameObject.SetActive(false);
            }
        }
        // ReSharper disable Unity.PerformanceAnalysis
        public void CloseWindow()
        {
            gameObject.SetActive(false);
            GamePlayManager.Instance.RemoveWindow(this);
            if (GamePlayManager.Instance.CurrentOpenWindowType == "")
            {
                GamePlayManager.Instance.ApplySceneSetting().Forget();
            }
            if (!profileShowingWhileOpen)
            {
                GamePlayManager.Instance.WindowsInstances.profileWindow.gameObject.SetActive(true);
            }
        }
    }
}
