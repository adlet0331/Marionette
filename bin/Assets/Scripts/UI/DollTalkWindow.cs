using System.Collections;
using System.Collections.Generic;
using DataBaseScripts;
using Managers;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    public enum DollTalkWindowType
    {
        TabSelecting = 0,
        DollTalk = 1,
        Inventory = 2,
        AnimaAbility = 3,
        SaveLoad = 4
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
        [SerializeField] private DollTalkWindowType _currentWindowType;

        [SerializeField] private DollTalkSelectionWindow dollTalkSelectionWindow;
        [SerializeField] private List<ADollTalkWindowTab> WindowTabs;
        [SerializeField] private GameObject chatBoxL, chatBoxR;
        [SerializeField] private Text chatText;
        [SerializeField] private DollTalkData dollTalkData;
        
        [SerializeField] private bool blocked;
        [SerializeField] private int currentScriptIndex;
        private IEnumerator currentCoroutine;

        private void ChangeSelectionTab(DollTalkSelectionType type)
        {
            switch (type)
            {
                case DollTalkSelectionType.ItemSelection:

                    return;
            }
            
        }
        private void ChangeWindowTab(DollTalkWindowType type)
        {
            WindowTabs[(int)_currentWindowType].gameObject.SetActive(false);
            _currentWindowType = type;
            WindowTabs[(int)_currentWindowType].gameObject.SetActive(true);
            
        }
        // ReSharper disable Unity.PerformanceAnalysis
        public override void Activate()
        {
            if (!gameObject.activeSelf)
            {
                OpenWindow();
                currentScriptIndex = 0;
                ChangeWindowTab(DollTalkWindowType.DollTalk);
                dollTalkStartRandomly(3);
            }
            else
            {
                CloseWindow();
            }
        }
        public void Interact()
        {
            if (_currentWindowType == DollTalkWindowType.DollTalk)
            {
                // 끝내기
                if (currentScriptIndex >= dollTalkData.scriptList.Count)
                {
                    dollTalkData = null;
                    currentScriptIndex = 0;
                    
                    return;
                }
                // Coroutine 이 실행중일 때, 스킵 기능
                if (blocked)
                {
                    StopCoroutine(currentCoroutine);
                    chatText.text = dollTalkData.scriptList[currentScriptIndex];
                    currentScriptIndex++;
                    blocked = false;
                    checkIsLastAndOpenDollTalkSelectionWindow();
                }
                // Coroutine 이 실행중이 아닐때
                else
                {
                    activateLeftorRight(dollTalkData.isGirlTalkingList[currentScriptIndex]);
                    currentCoroutine = _printScript(chatText, dollTalkData.scriptList[currentScriptIndex]);
                    StartCoroutine(currentCoroutine);
                }
            }
            
        }

        public void PressArrow()
        {
            
        }
        
        # region DollTalk
        private void activateLeftorRight(bool isLeft)
        {
            chatBoxL.SetActive(isLeft);
            chatBoxR.SetActive(!isLeft);
        }
        private void checkIsLastAndOpenDollTalkSelectionWindow()
        {
            if (currentScriptIndex >= dollTalkData.scriptList.Count)
            {
                //WindowManager.Instance.dollTalkSelectionWindowInnerTab.OpenWithType(DollTalkSelectionType.SelectTab);
            }
        }
        private void dollTalkStartRandomly(int maxIndex)
        {
            int randomIndex = new System.Random().Next(maxIndex);
            //Random chat of doll
            dollTalkData = DataBaseManager.Instance.dollTalkDataBase.dataList[randomIndex];

            Interact();
        }
        private IEnumerator _printScript(Text textObj, string script)
        {
            blocked = true;
            textObj.text = "";
            yield return new WaitForSeconds(0.01f);
            for (int i = 0; i <= script.Length; i++)
            {
                textObj.text = script.Substring(0, i);
                yield return new WaitForSeconds(0.02f);
            }
            currentScriptIndex++;
            blocked = false;
            checkIsLastAndOpenDollTalkSelectionWindow();
        }
        #endregion
    }
}

