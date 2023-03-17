using System.Collections;
using System.Collections.Generic;
using DataBaseScripts;
using Managers;
using UnityEngine;

namespace UI
{
    public class TalkTab : ADollTalkWindowTab
    {
        [Header("Changed in Game")]
        [SerializeField] private List<int> currentRandomIndexList;
        
        [SerializeField] private DollTalkData dollTalkData;
        [SerializeField] private bool blocked;
        [SerializeField] private int currentScriptIndex;

        private IEnumerator _currentCoroutine;
        
        public override void OpenTab()
        {
            gameObject.SetActive(true);
            int randomIndex = new System.Random().Next(2);
            //Random chat of doll
            blocked = false;
            currentScriptIndex = 0;
            dollTalkData = GamePlayManager.Instance.dataBaseCollection.dollTalkDataBase.dataKeyDictionary[randomIndex];
            GetInput(InputType.Space);
        }

        public override void CloseTab()
        {
            gameObject.SetActive(false);
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
        }

        public override void GetInput(InputType input)
        {
            switch (input)
            {
                case InputType.Space:
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
                        StopCoroutine(_currentCoroutine);
                        _endScript(currentScriptIndex);
                    }
                    // Coroutine 이 실행중이 아닐때
                    else
                    {
                        _currentCoroutine = _printScript(currentScriptIndex);
                        StartCoroutine(_currentCoroutine); 
                    }
                    return;
                default:
                    return;
            }
        }
        private IEnumerator _printScript(int index)
        {
            blocked = true;
            GamePlayManager.Instance.WindowsInstances.dollTalkWindow.ActivateNext(false);
            var text = "";
            var isLeft = dollTalkData.isGirlTalkingList[index];
            var printText = dollTalkData.scriptList[index];
            GamePlayManager.Instance.WindowsInstances.dollTalkWindow.SetChatText(isLeft, text);
            yield return new WaitForSeconds(0.01f);
            for (int i = 0; i <= printText.Length; i++)
            {
                text = printText.Substring(0, i);
                GamePlayManager.Instance.WindowsInstances.dollTalkWindow.SetChatText(isLeft, text);
                yield return new WaitForSeconds(0.02f);
            }
            _endScript(index);
        }
        // ReSharper disable Unity.PerformanceAnalysis
        private void _endScript(int index)
        {
            if (index != currentScriptIndex)
                Debug.LogAssertion("index : " + index.ToString() + " CurrentIndex : " + currentScriptIndex.ToString());
            
            blocked = false;
            GamePlayManager.Instance.WindowsInstances.dollTalkWindow.SetChatText(dollTalkData.isGirlTalkingList[index], dollTalkData.scriptList[index]);
            currentScriptIndex++;
            // checkIsLastAndOpenDollTalkSelectionWindow
            if (currentScriptIndex >= dollTalkData.scriptList.Count)
            {
                GamePlayManager.Instance.WindowsInstances.dollTalkWindow.ChangeWindowTab(DollTalkWindowType.TabSelecting);
                currentScriptIndex = 0;
            }
            GamePlayManager.Instance.WindowsInstances.dollTalkWindow.ActivateNext(true);
        }
    }
}