using System.Collections;
using System.Collections.Generic;
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
public class ScriptWindow : WindowObject
{
    [SerializeField] private Text NameText;
    [SerializeField] private Text ScriptText;
    [SerializeField] private ScriptableObjData currObj = null;
    [SerializeField] private int currentScriptIdx = -1;
    [SerializeField] private int currentScriptLength;

    [SerializeField] private bool blocked;
    private void Open(int idx)
    {
        Debug.Assert(!gameObject.activeSelf, "ScriptWindow가 닫혀있지 않습니다. 버그임!!");
        this.ActivateObject();

        currObj = ScriptObjDataManager.Instance.ScriptObjDataList[idx];
        currentScriptIdx = 0;
        currentScriptLength = currObj.scripts.Count;

        NameText.text = currObj.name;
        StartCoroutine(_printScript(ScriptText, currObj.scripts[0]));
    }
    private void Next(int idx)
    {
        currentScriptIdx++;
        NameText.text = currObj.name;
        StartCoroutine(_printScript(ScriptText, currObj.scripts[currentScriptIdx]));
    }
    private void Close()
    {
        currentScriptIdx = -1;
        gameObject.SetActive(false);
    }
    // 초기화
    public override void Activate()
    {
        InteractionObject obj = PlayerManager.Instance.playerInteractObject.GetFstScrObj();
        int idx = obj.GetIdx();
        Debug.Assert(idx >= 0 && idx <= ScriptObjDataManager.Instance.ScriptObjDataList.Count, 
            "Valid 하지 않은 ScriptObjectData IDX 입니다.");   
        if (currentScriptIdx == -1)
            this.Open(idx);
        else if (currObj != null && currentScriptIdx == currObj.scripts.Count - 1)
            this.Close();
        else
            this.Next(idx);
    }
    IEnumerator _printScript(Text textObj, string script)
    {
        blocked = true;
        textObj.text = "";
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i <= script.Length; i++)
        {
            textObj.text = script.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
        blocked = false;
    }
}
