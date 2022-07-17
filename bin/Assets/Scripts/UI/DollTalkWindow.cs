using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class DollTalkWindow : WindowObject
    {
        [SerializeField] private int currentWindow; // 0 : Idle, 1 : ChatWithDoll, 2 : Inventory, 3 : AnimaAbility, 4 : Save & Load
        [SerializeField] private GameObject pointSelImg;
        [SerializeField] private List<GameObject> chooseBoxImg;
        [SerializeField] private List<GameObject> chooseBoxText;
        [SerializeField] private int activeSelNum; // Total number of selection
        [SerializeField] private int pointSelNum; // Current number of selection
        [SerializeField] private GameObject chatBoxL, chatBoxR;
        public override void Activate()
        {
            if (gameObject.activeSelf)
            {
                this.CloseWindow();
            }
            else
            {
                this.OpenWindow();
            }
        }

        private void setChooseNumber(int num) // 2, 3, 4 only
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

        private void chooseMove(int num) // <= (activeSelNum) only
        {
            pointSelImg.transform.position = new Vector3(-220.0f, 40.0f * num - 30.0f, 10000.0f);
            pointSelNum = num;
        }

        private void chooseUpDown(bool UpDown) // Up = true, Down = false
        {
            int moveP;
            if (UpDown)
                moveP = pointSelNum % activeSelNum + 1;
            else
                moveP = (pointSelNum - 2) % activeSelNum + 1;

            chooseMove(moveP);
            
        }

        private void chatLeftRight(bool LeftRight) // Left = true, Right = false
        {
            chatBoxL.SetActive(false);
            chatBoxR.SetActive(false);
            
            if (LeftRight)
                chatBoxL.SetActive(true);
            else
                chatBoxR.SetActive(true);
        }

        public void keyUpInput()
        {
            chooseUpDown(true);
        }

        public void keyDownInput()
        {
            chooseUpDown(false);
        }

        public void keySpaceInput()
        {
            
        }

        private void dollTalkStart()
        {
            chatLeftRight(false);
            //Random chat of doll
        }
    }
}

