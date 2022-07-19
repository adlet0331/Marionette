    using System.Collections;
using System.Collections.Generic;
    using DataBaseScripts;
    using Managers;
    using Unity.Mathematics;
    using UnityEngine;
    using UnityEngine.UI;
    using Random = System.Random;

    namespace UI
{
    public class DollTalkWindow : WindowObject
    {
        [SerializeField] private int currentWindow; // 0 : ChatWithDoll, 1 : Inventory, 2 : AnimaAbility, 3 : Save & Load
        [SerializeField] private GameObject chatBoxL, chatBoxR;
        [SerializeField] private Text chatText;
        [SerializeField] private List<GameObject> WindowObjects;
        [SerializeField] private DollTalkData dollTalkData;
        [SerializeField] private int currentScriptIndex;

        [SerializeField] private bool blocked;
        private IEnumerator currentCoroutine;
        public override void Activate()
        {
            if (!gameObject.activeSelf)
            {
                this.OpenWindow();
                currentScriptIndex = 0;
                dollTalkStartRandomly(3);
            }
            else
            {
                this.CloseWindow();
            }
        }

        public bool PressSpace()
        {
            // 끝내기
            if (currentScriptIndex >= dollTalkData.scriptList.Count)
            {
                dollTalkData = null;
                currentScriptIndex = 0;
                return true;
            }
            // Coroutine 이 실행중일 때, 스킵 기능
            if (blocked)
            {
                StopCoroutine(currentCoroutine);
                activateLeftorRight(dollTalkData.isGirlTalking[currentScriptIndex]);
                chatText.text = dollTalkData.scriptList[currentScriptIndex];
            }
            // Coroutine 이 실행중이 아닐때
            else
            {
                activateLeftorRight(dollTalkData.isGirlTalking[currentScriptIndex]);
                currentCoroutine = _printScript(chatText, dollTalkData.scriptList[currentScriptIndex]);
                StartCoroutine(currentCoroutine);
                _endPrintScript();
            }

            return false;
        }

        private void activateLeftorRight(bool isLeft)
        {
            chatBoxL.SetActive(isLeft);
            chatBoxR.SetActive(!isLeft);
        }
        private void dollTalkStartRandomly(int maxIndex)
        {
            int randomIndex = new Random().Next(maxIndex);
            //Random chat of doll
            dollTalkData = DataBaseManager.Instance.dollTalkDataBase.dataList[randomIndex];
            
            activateLeftorRight(dollTalkData.isGirlTalking[currentScriptIndex]);

            StartCoroutine(_printScript(chatText, dollTalkData.scriptList[currentScriptIndex]));
        }
        private IEnumerator _printScript(Text textObj, string script)
        {
            blocked = true;
            textObj.text = "";
            yield return new WaitForSeconds(0.01f);
            for (int i = 0; i <= script.Length; i++)
            {
                textObj.text = script.Substring(0, i);
                yield return new WaitForSeconds(0.01f);
            }
            _endPrintScript();
        }
        
        private void _endPrintScript()
        {
            blocked = false;
            currentCoroutine = null;
            currentScriptIndex++;
        }
    }
}

