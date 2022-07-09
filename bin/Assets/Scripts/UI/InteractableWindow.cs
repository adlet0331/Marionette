﻿using System.Collections;
using DataBaseScripts;
using UnityEngine;
using UnityEngine.UI;
using static InteractionObject;

/* 상호작용 가능한 가장 가까운 오브젝트 정보 띄워주는 창
 * PlayerManager에서 호출
 * 
 * 아래 2개 출력
 * - 오브젝트 이름
 * - InGameObjectManager index = 0 인 String
 */
public class InteractableWindow : WindowObject
{
    [SerializeField] private bool isLockPrinting;
    [SerializeField] private Text nameText;
    [SerializeField] private Text explanationText;
    [SerializeField] private int currentIdx;

    private IEnumerator currentCoroutine;
    [SerializeField] private bool blocked;

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
        yield return new WaitForSeconds(0.5f);
        Debug.Log("End Couroutine");
        blocked = false;
        currentCoroutine = null;
    }

    public void SetIsLockedWindow(string str)
    {
        isLockPrinting = true;

        currentCoroutine = _printScript(explanationText, str);
        StartCoroutine(currentCoroutine);

        isLockPrinting = false;
        return;
    }

    public override void Activate()
    {
        return;
    }

    public void SetIsLockedWindow(string name, string str)
    {
        nameText.text = name;
        explanationText.text = str;

        return;
    }

    public new void CloseWindow()
    {
        blocked = false;
        base.CloseWindow();
    }
}
