using UnityEngine;

/* 사용자 설정 창 (아니마 도감, 스토리 진행도, 게임 설정). ESC 누르면 나옴
 */
namespace UI
{
    public class SettingWindow : WindowObject
    {

        [SerializeField]
        private GameObject gameObject;
        [SerializeField]
        private int currentWindow;
        [SerializeField]
        private GameObject Object_AnimaLibrary;
        [SerializeField]
        private GameObject Object_StoryLine;
        [SerializeField]
        private GameObject Object_Settings;

        public override void Activate()
        {
            if (gameObject.activeSelf)
                this.CloseWindow();
            else
                this.OpenWindow();
        }

        public void tabInput() {
            Debug.Log("Test");
            if (currentWindow <= 2)
                currentWindow += 1;
            else 
                currentWindow = 1;
        

            changeWindow(currentWindow);
        }

        public void changeWindow(int cur)
        {
            Object_AnimaLibrary.SetActive(false);
            Object_StoryLine.SetActive(false);
            Object_Settings.SetActive(false);

            if (cur == 1) {
                Object_AnimaLibrary.SetActive(true);
                currentWindow = 1;
            } else if (cur == 2) {
                Object_StoryLine.SetActive(true);
                currentWindow = 2;
            } else if (cur == 3) {
                Object_Settings.SetActive(true);
                currentWindow = 3;
                Debug.Log("3");
            }

        }
    
    }
}
