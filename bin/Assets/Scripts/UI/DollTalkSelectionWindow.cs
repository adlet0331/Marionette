using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace UI
{
    public class DollTalkSelectionWindow : WindowObject
    {
        [SerializeField] private List<GameObject> chooseBoxImgList;
        [SerializeField] private List<DollTalkSelectionBox> chooseBoxList;
        
        [SerializeField] private int totalSelNum; // Total number of selection
        [SerializeField] private int currentSelNum; // Current number of selection
        
        public override void Activate()
        {
            OpenWindow();
            initChooseWindow(4);
        }
        
        public void MoveUpDown(bool isUp) // Up = true, Down = false
        {
            if (isUp)
                currentSelNum = (currentSelNum + totalSelNum - 1) % totalSelNum;
            else
                currentSelNum = (currentSelNum + 1) % totalSelNum;
            updateSelect();
        }

        public void PressSpace()
        {
            WindowManager.Instance.dollTalkWindow.ChangeTab(currentSelNum);
        }

        private void updateSelect()
        {
            for (int i = 0; i < totalSelNum; i++)
            {
                chooseBoxList[i].ActivateSelection(i == currentSelNum);
            }
        }
        private void initChooseWindow(int num) // 2, 3, 4 only
        {
            for (int i = 0; i < 3; i++)
            {
                if (i == num - 2)
                {
                    chooseBoxImgList[i].SetActive(true);
                }
                else
                {
                    chooseBoxImgList[i].SetActive(false);
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (i < 4 - num)
                {
                    chooseBoxList[i].gameObject.SetActive(false);
                }
                else
                {
                    chooseBoxList[i].gameObject.SetActive(true);
                    chooseBoxList[i].ActivateSelection(false);
                }
            }
            totalSelNum = num;
            currentSelNum = 0;
            updateSelect();
        }
    }
}