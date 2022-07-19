using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class DollTalkSelectionWindow : WindowObject
    {
        [SerializeField] private List<GameObject> chooseBoxImgList;
        [SerializeField] private List<GameObject> talkBoxGameObjectList;
        [SerializeField] private List<DollTalkSelectionBox> chooseBoxTextList;
        
        [SerializeField] private int activeSelNum; // Total number of selection
        [SerializeField] private int pointSelNum; // Current number of selection
        
        public override void Activate()
        {
            OpenWindow();
            initChooseNumber(4);
        }
        
        private void initChooseNumber(int num) // 2, 3, 4 only
        {
            for (int i = 0; i < 4; i++)
            {
                if (i == num - 2)
                {
                    chooseBoxImgList[i].SetActive(true);
                }
                else
                {
                    chooseBoxImgList[i].SetActive(false);
                }

                if (i < 4 - num)
                {
                    chooseBoxTextList[i].gameObject.SetActive(false);
                }
                else
                {
                    chooseBoxTextList[i].gameObject.SetActive(true);
                }
            }

            chooseBoxImgList[num - 1].SetActive(true);
            activeSelNum = num;
        }

        public void MoveUpDown(bool UpDown) // Up = true, Down = false
        {
            int moveP;
            if (UpDown)
                moveP = pointSelNum % activeSelNum + 1;
            else
                moveP = (pointSelNum - 2) % activeSelNum + 1;
        }
    }
}