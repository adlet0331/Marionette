using System;
using System.Collections;
using System.Data.Common;
using System.IO;
using Cysharp.Threading.Tasks;
using DataBaseScripts;
using Managers;
using UI;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    public class ScriptControl : IInteractionObjectWithUI<ScriptData, ScriptUIData>
    {
        [SerializeField] private int currentIndex;
        [SerializeField] private bool blocked;
        private IEnumerator currentCoroutine;
        protected override void GetUIWindowAndInit()
        {
            currentIndex = 0;
            blocked = false;
            currentCoroutine = null;
            UIWindow = WindowManager.Instance.scriptWindow;
        }
        public override async UniTask<bool> Interact()
        {
            if (currentIndex >= data.scriptList.Count) // 끝나는 것 처리
            {
                UIWindow.DeActivate();
                currentIndex = 0;
                return true;
            }
            if (!blocked) // 코루틴 끝나 있을 때
            {
                currentCoroutine = _printScript(currentIndex);
                StartCoroutine(currentCoroutine);
                return false;
            }
            else // 코루틴 안 끝났을 때
            {
                StopCoroutine(currentCoroutine);
                UIData.script = data.scriptList[currentIndex];
                UIWindow.InteractWithData(UIData);
                EndPrintScript();
            }

            return false;
        }
        private IEnumerator _printScript(int index)
        {
            UIWindow.gameObject.GetComponent<ScriptWindow>().ActivateNext(false);
            blocked = true;
            UIData.name = data.titleList[index];
            if (data.leftSpriteList[currentIndex] == "none")
            {
                UIData.sprite = null;
            }
            else
            {
                UIData.sprite = Resources.Load<Sprite>(Path.Combine("Sprites", "Character_Script",
                    data.leftSpriteList[currentIndex]));
            }
            UIData.script = "";
            string script = data.scriptList[index];
            yield return new WaitForSeconds(0.02f);
            for (int i = 0; i <= script.Length; i++)
            {
                UIData.script = script.Substring(0, i);
                UIWindow.InteractWithData(UIData);
                yield return new WaitForSeconds(0.02f);
            }
            EndPrintScript();
        }
        private void EndPrintScript()
        {
            blocked = false;
            currentCoroutine = null;
            currentIndex++;
            UIWindow.gameObject.GetComponent<ScriptWindow>().ActivateNext(true);
        }
    }
}
