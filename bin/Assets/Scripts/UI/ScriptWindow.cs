﻿using System.Collections;
using DataBaseScripts;
using InGameObjects.Object;
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
    [SerializeField] private InScriptData currObj = null;
    [SerializeField] private int currentScriptIdx = -1;
    [SerializeField] private int currentScriptLength;
    private IEnumerator currentCoroutine;

    [SerializeField] private bool blocked = false;
    private void Open(int idx)
    {
        this.OpenWindow();

        currObj = DataBaseManager.Instance.InScriptDataBase.dataList[idx];
        currentScriptIdx = 0;
        currentScriptLength = currObj.scriptList.Count;

        NameText.text = currObj.name;
        currentCoroutine = _printScript(ScriptText, currObj.scriptList[0]);
        StartCoroutine(currentCoroutine);
    }
    private void Next(int idx)
    {
        currentScriptIdx++;
        NameText.text = currObj.name;
        currentCoroutine = _printScript(ScriptText, currObj.scriptList[currentScriptIdx]);
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
        ScriptText.text = currObj.scriptList[currentScriptIdx];
    }
    // 초기화
    public override void Activate()
    {
        return;
    }
    public void Activate(int idx)
    {
        if (PlayerManager.Instance.interactingPlayer == null)
            return;

        if (blocked)
        {
            Stop();
            return;
        }

        InteractionObject obj = PlayerManager.Instance.interactingPlayer.GetFstInteractObj();

        if (obj == null)
            return;

        //InputManager.Instance.SetOptions(false, true);  스크립트 진행 중 움직임 막기

        if (currentScriptIdx == -1)
            this.Open(idx);
        else if (currObj != null && currentScriptIdx == currObj.scriptList.Count - 1)
        {   
            InputManager.Instance.SetOptions(true, true);
            this.Close();
        }
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
