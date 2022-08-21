using Managers;
using UnityEngine;

namespace UI
{
    public class SaveInfo
    {
        public string sceneString;
        public float timePassed;
        public SaveInfo(string name, float t)
        {
            sceneString = name;
            timePassed = t;
        }

        public SaveInfo()
        {
            
        }
    }
    public class SLTab : ADollTalkWindowTab
    {
        [SerializeField] private SLSlot[] slotList;
        public override void OpenTab()
        {
            gameObject.SetActive(true);
            for (int i = 1; i <= slotList.Length; i++)
            {
                var info = SLManager.Instance.GetSaveDataInfo(i);
                slotList[i-1].SetSaveInfos(info);
            }
            WindowManager.Instance.dollTalkSelectionWindowInnerTab.OpenWithType(typePerSelectionStrings);
        }

        public override void CloseTab()
        {
            gameObject.SetActive(false);
        }

        public override void GetInput(InputType input)
        {
            var dollTalkSelectionWindow = WindowManager.Instance.dollTalkSelectionWindowInnerTab;
            switch (input)
            {
                case InputType.Up:
                    dollTalkSelectionWindow.GetInputIdx(InputType.Up);
                    return;
                case InputType.Down:
                    dollTalkSelectionWindow.GetInputIdx(InputType.Down);
                    return;
                case InputType.Space:
                    var selectionIdx = dollTalkSelectionWindow.GetInputIdx(InputType.Space);
                    switch (selectionIdx)
                    {
                        case 0: // 2번 저장
                            SLManager.Instance.Save(2);
                            break;
                        case 1: // 2번 로드
                            SLManager.Instance.Load(2);
                            break;
                        case 2: // 1번 저장
                            SLManager.Instance.Save(1);
                            break;
                        case 3: // 1번 로드
                            SLManager.Instance.Load(2); 
                            break;
                    }
                    return;
                    return;
            }
            
        }
    }
}