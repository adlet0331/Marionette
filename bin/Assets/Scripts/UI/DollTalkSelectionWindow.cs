using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class DollTalkSelectionWindow : WindowObject
    {
        [SerializeField] private List<GameObject> chooseBoxImg;
        [SerializeField] private List<GameObject> chooseBoxText;
        
        [SerializeField] private int activeSelNum; // Total number of selection
        [SerializeField] private int pointSelNum; // Current number of selection
        
        public override void Activate()
        {
            OpenWindow();
        }
        
        private void initChooseNumber(int num) // 2, 3, 4 only
        {
            for (int i = 1; i <= num; i++)
            {
                chooseBoxText[i - 1].SetActive(false);
                if (i != 1) chooseBoxImg[i].SetActive(false);
            }

            for (int i = 1; i <= num; i++)
            {
                chooseBoxText[i - 1].SetActive(true);
            }

            chooseBoxImg[num - 1].SetActive(true);
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