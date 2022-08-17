using System.Collections;
using System.Collections.Generic;
using DataBaseScripts;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    public enum DollTalkWindowType
    {
        DollTalk = 3,
        Inventory = 2,
        StellaInventory = 1,
        SaveLoad = 0,
        TabSelecting = -1,
    }
    public enum InputType
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3,
        Space = 4,
        C = 5
    }
    public class DollTalkWindow : WindowObject
    {
        [Header("Current Status")]
        [SerializeField] private DollTalkWindowType _currentWindowType;

        [SerializeField] private DollTalkSelectionWindow dollTalkSelectionWindow;
        [SerializeField] private List<ADollTalkWindowTab> windowTabs;
        [SerializeField] private GameObject chatBoxL, chatBoxR;
        [SerializeField] private Text chatText;

        [SerializeField] private TypePerSelectionStrings tabSelecting;

        public void SetChatText(bool isLeft, string text)
        {
            chatBoxL.SetActive(isLeft);
            chatBoxR.SetActive(!isLeft);
            chatText.text = text;
        }
 
        // ReSharper disable Unity.PerformanceAnalysis
        public override void Activate()
        {
            if (!gameObject.activeSelf)
            {
                OpenWindow();
                foreach (var windowTab in windowTabs)
                {
                    windowTab.CloseTab();
                }
                dollTalkSelectionWindow.Close();
                ChangeWindowTab(DollTalkWindowType.DollTalk);
            }
            else
            {
                CloseWindow();
            }
        }
        public void Interact(InputType input)
        {
            if (input == InputType.C)
            {
                CloseWindow();
            }

            if (_currentWindowType == DollTalkWindowType.TabSelecting)
            {
                dollTalkSelectionWindow.GetInput(input);
                return;
            }

            windowTabs[(int)_currentWindowType].GetInput(input);
        }
        

        public void ChangeWindowTab(DollTalkWindowType type)
        {
            dollTalkSelectionWindow.Close();
            _currentWindowType = type;
            
            if (type == DollTalkWindowType.TabSelecting)
            {
                dollTalkSelectionWindow.OpenWithType(tabSelecting);
            }
            else
            {
                windowTabs[(int)_currentWindowType].OpenTab();
            }
        }
    }
}

