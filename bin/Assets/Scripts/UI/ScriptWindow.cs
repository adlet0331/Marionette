using System;
using System.Collections;
using DataBaseScripts;
using InGameObjects.Interaction;
using Managers;
using UnityEngine;
using UnityEngine.UI;

/* 대사 나오는 창
* InputManager에서 호출
* 
* 출력하는 것
* - IngameObject 이름
* - Scripts index = 1 ~ n까지 출력. 버튼 입력 받으면 다음으로 넘어감
* 
*/
namespace UI
{
    public class ScriptWindow : WindowObject
    {
        [SerializeField] private Text nameText;
        [SerializeField] private Text scriptText;
        [SerializeField] private ScriptData currObj = null;
        [SerializeField] private int currentPrintingIndex;

        [SerializeField] private bool blocked = false;
        private IEnumerator currentCoroutine;
        public override void Activate()
        {
            this.OpenWindow();
        }
        public int PressSpace()
        {
            // 끝내기
            if (currentPrintingIndex >= currObj.scriptList.Count)
            {
                currObj = null;
                this.CloseWindow();
                currentPrintingIndex = 0;
                return -1;
            }
            // Coroutine 이 실행중이 아닐때
            if (!blocked)
            {
                nameText.text = currObj.name;
                currentCoroutine = _printScript(scriptText, currObj.scriptList[currentPrintingIndex]);
                StartCoroutine(currentCoroutine);
            }
            // Coroutine 이 실행중일 때, 스킵 기능
            else
            {
                StopCoroutine(currentCoroutine);
                scriptText.text = currObj.scriptList[currentPrintingIndex];
                EndPrintScript();
            }
            return currentPrintingIndex;
        }
        // 초기화
        public void SetScriptData(ScriptData data)
        {
            currObj = data;
        }
        private IEnumerator _printScript(Text textObj, string script)
        {
            blocked = true;
            textObj.text = "";
            yield return new WaitForSeconds(0.02f);
            for (int i = 0; i <= script.Length; i++)
            {
                textObj.text = script.Substring(0, i);
                yield return new WaitForSeconds(0.02f);
            }
            EndPrintScript();
        }
        private void EndPrintScript()
        {
            blocked = false;
            currentCoroutine = null;
            currentPrintingIndex++;
        }
    }
}
