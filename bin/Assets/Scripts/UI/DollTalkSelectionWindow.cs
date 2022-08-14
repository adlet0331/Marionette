using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [Serializable]
    public enum DollTalkSelectionType
    {
        SelectTab = 4,
        ItemSelection = 2,
        AnimaAbilitySelection = 2,
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
        [SerializeField] private List<GameObject> chooseBoxImgList;
        [SerializeField] private List<DollTalkSelectionBox> chooseBoxList;

        [SerializeField] private DollTalkSelectionType windowType;
        [SerializeField] private int totalSelNum; // Total number of selection
        [SerializeField] private int currentSelNum; // Current number of selection

        public void OpenWithType(DollTalkSelectionType type, TypePerSelectionStrings selectionStrings)
        {
            if (selectionStrings.strings.Count != (int)type)
                throw new Exception(selectionStrings.type.ToString() + " : String Count Error.");
            
            windowType = type;
            gameObject.SetActive(true);
            // 2개 3개 4개 이미지 교체
            for (int i = 0; i < 3; i++)
            {
                chooseBoxImgList[i].SetActive(i == (int)type - 2);
            }
            // ChooseBoxList 설정
            // 아래부터 0, 1, 2, 3
            for (int i = 0; i < chooseBoxList.Count; i++)
            {
                chooseBoxList[i].gameObject.SetActive(i < (int)type);
                if(i < (int)type)
                    chooseBoxList[i].SetText(selectionStrings.strings[i]);
            }
            totalSelNum = (int)type;
            currentSelNum = 0;
            chooseBoxList[currentSelNum].ActivateSelection(false);
            
            updateSelect();
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
        
        // Up = true, Down = false
        public void InputUpDown(InputType inputType) 
        {
            switch (inputType)
            {
                case InputType.Down:
                    currentSelNum = (currentSelNum + totalSelNum - 1) % totalSelNum;
                    break;
                case InputType.Up:
                    currentSelNum = currentSelNum = (currentSelNum + 1) % totalSelNum;
                    break;
            }
            updateSelect();
        }
        private void updateSelect()
        {
            for (int i = 0; i < totalSelNum; i++)
            {
                chooseBoxList[i].ActivateSelection(i == currentSelNum);
            }
        }
    }
}