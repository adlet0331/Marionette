    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class DollTalkWindow : WindowObject
    {
        [SerializeField] private int currentWindow; // 0 : ChatWithDoll, 1 : Inventory, 2 : AnimaAbility, 3 : Save & Load
        [SerializeField] private GameObject chatBoxL, chatBoxR;
        [SerializeField] private List<GameObject> WindowObjects;
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

        private void chatLeftRight(bool LeftRight) // Left = true, Right = false
        {
            if (LeftRight)
            {
                chatBoxL.SetActive(true);
                chatBoxR.SetActive(false);
            }
            else
            {
                chatBoxL.SetActive(false);
                chatBoxR.SetActive(true);
            }
        }

        private void dollTalkStart()
        {
            chatLeftRight(false);
            //Random chat of doll
        }
    }
}

