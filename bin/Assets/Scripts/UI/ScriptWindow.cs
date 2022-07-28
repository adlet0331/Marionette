using System.Collections;
using System.IO;
using DataBaseScripts;
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
    public class ScriptWindow : UIControlWindow<ScriptData>
    {
        [SerializeField] private Text nameText;
        [SerializeField] private Text scriptText;
        [SerializeField] private Image currentImage;
        [SerializeField] private int currentPrintingIndex;

        [SerializeField] private bool blocked = false;
        private IEnumerator currentCoroutine;

        public override void OpenWithData(ScriptData d)
        {
            this.data = d;
            PressSpace();
        }

        public int PressSpace()
        {
            // 끝내기
            if (currentPrintingIndex >= data.scriptList.Count)
            {
                data = null;
                this.CloseWindow();
                currentPrintingIndex = 0;
                return -1;
            }
            // Coroutine 이 실행중이 아닐때
            if (!blocked)
            {
                nameText.text = data.name;
                currentCoroutine = _printScript(scriptText, data.scriptList[currentPrintingIndex]);
                StartCoroutine(currentCoroutine);
            }
            // Coroutine 이 실행중일 때, 스킵 기능
            else
            {
                StopCoroutine(currentCoroutine);
                scriptText.text = data.scriptList[currentPrintingIndex];
                EndPrintScript();
            }
            if (data.leftSpriteList[currentPrintingIndex] == "none")
            {
                currentImage.sprite = null;
                currentImage.gameObject.SetActive(false);
            }
            else {
                currentImage.gameObject.SetActive(true);
                currentImage.sprite = Resources.Load<Sprite>(Path.Combine("Sprites", "Character_Script",
                    data.leftSpriteList[currentPrintingIndex]));
            }
            return currentPrintingIndex;
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
