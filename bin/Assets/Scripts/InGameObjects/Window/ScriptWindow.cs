using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static InteractionObject;
/* 대사 나오는 창
* InputManager에서 호출
* 
* 출력하는 것
* - IngameObject 이름
* - Scripts index = 1 ~ n까지 출력. 버튼 입력 받으면 다음으로 넘어감
* 
*/
public class ScriptWindow : WindowObject
{
    [SerializeField] private Text NameText;
    [SerializeField] private Text ScriptText;
    [SerializeField] private ScriptableObjData currObj = null;
    [SerializeField] private int currentScriptIdx = -1;
    [SerializeField] private int currentScriptLength;
    private IEnumerator currentCoroutine;

    [SerializeField] private bool blocked = false;
    private void Open(int idx)
    {
        this.OpenWindow();

        currObj = ScriptObjDataManager.Instance.ScriptObjDataList[idx];
        currentScriptIdx = 0;
        currentScriptLength = currObj.scripts.Count;

        NameText.text = currObj.name;
        currentCoroutine = _printScript(ScriptText, currObj.scripts[0]);
        StartCoroutine(currentCoroutine);
    }
    private void Next(int idx)
    {
        currentScriptIdx++;
        NameText.text = currObj.name;
        currentCoroutine = _printScript(ScriptText, currObj.scripts[currentScriptIdx]);
        StartCoroutine(currentCoroutine);
    }
    private void Close()
    {
        currentScriptIdx = -1;
        CloseWindow();
    }
    private void Stop()
    {
        StopCoroutine(currentCoroutine);
        blocked = false;
        currentCoroutine = null;
        ScriptText.text = currObj.scripts[currentScriptIdx];
    }
    // 초기화
    public override void Activate()
    {
        return;
    }
    public void Activate(int idx, InteractionObjectType type)
    {
        if (PlayerManager.Instance.playerInteractObject == null)
            return;

        if (type != InteractionObjectType.ScriptableObject)
            return;

        if (blocked)
        {
            Stop();
            return;
        }

        InteractionObject obj = PlayerManager.Instance.playerInteractObject.GetFstInteractObj();

        if (obj == null)
            return;

        if (currentScriptIdx == -1)
            this.Open(idx);
        else if (currObj != null && currentScriptIdx == currObj.scripts.Count - 1)
            this.Close();
        else
            this.Next(idx);
        return;
    }
    IEnumerator _printScript(Text textObj, string script)
    {
        blocked = true;
        textObj.text = "";
        yield return new WaitForSeconds(0.02f);
        for (int i = 0; i <= script.Length; i++)
        {
            textObj.text = script.Substring(0, i);
            yield return new WaitForSeconds(0.02f);
        }
        blocked = false;
        currentCoroutine = null;
    }
}
