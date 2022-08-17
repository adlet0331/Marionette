using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace UI
{
    [Serializable]
    public enum DollTalkSelectionType
    {
        SelectTab = 4,
        TalkSelection = -1,
        ItemSelection = 3,
        StellaSelection = 2,
        SaveLoad = 2
    }

    [Serializable]
    public class TypePerSelectionStrings
    {
        public DollTalkSelectionType type;
        public List<string> strings;
    }
    public class DollTalkSelectionWindow: MonoBehaviour
    {
        [Header("Need to be Initialized")]
        [SerializeField] private List<GameObject> chooseBoxImgList;
        [SerializeField] private List<DollTalkSelectionBox> chooseBoxList;
        
        [Header("Changed In Game")]
        [SerializeField] private DollTalkSelectionType windowType;
        [SerializeField] private int totalSelNum; // Total number of selection
        [SerializeField] private int currentSelNum; // Current number of selection

        public void OpenWithType(TypePerSelectionStrings selectionStrings)
        {
            var windowNum = selectionStrings.strings.Count;

            if (selectionStrings.strings.Count != windowNum)
                throw new Exception(selectionStrings.type.ToString() + " : String Count Error.");
            
            windowType = selectionStrings.type;
            gameObject.SetActive(true);
            // 2개 3개 4개 이미지 교체
            for (int i = 0; i < 3; i++)
            {
                chooseBoxImgList[i].SetActive(i == windowNum - 2);
            }
            // ChooseBoxList 설정
            // 아래부터 0, 1, 2, 3
            for (int i = 0; i < chooseBoxList.Count; i++)
            {
                chooseBoxList[i].gameObject.SetActive(i < windowNum);
                if(i < windowNum)
                    chooseBoxList[i].SetText(selectionStrings.strings[i]);
            }
            totalSelNum = windowNum;
            currentSelNum = windowNum - 1;
            chooseBoxList[currentSelNum].ActivateSelection(false);
            
            _updateSelect();
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
        
        // Up = true, Down = false
        public void GetInput(InputType inputType) 
        {
            switch (inputType)
            {
                case InputType.Down:
                    currentSelNum = (currentSelNum + totalSelNum - 1) % totalSelNum;
                    break;
                case InputType.Up:
                    currentSelNum = currentSelNum = (currentSelNum + 1) % totalSelNum;
                    break;
                case InputType.Space:
                    switch (windowType)
                    {
                        case DollTalkSelectionType.SelectTab:
                            WindowManager.Instance.dollTalkWindow.ChangeWindowTab((DollTalkWindowType)currentSelNum);
                            return;
                        case DollTalkSelectionType.ItemSelection:
                            switch (currentSelNum)
                            {
                                case 0:
                                    WindowManager.Instance.dollTalkWindow.ChangeWindowTab(DollTalkWindowType.Inventory);
                                    return;
                            }
                            break;
                        default:
                            break;
                    }
                    return;
                default:
                    break;
            }
            _updateSelect();
        }
        private void _updateSelect()
        {
            for (int i = 0; i < totalSelNum; i++)
            {
                chooseBoxList[i].ActivateSelection(i == currentSelNum);
            }
        }
    }
}