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
    private int currentScriptIdx;
    
    private void checkAvaliableIdx(int idx)
    {
        Debug.Assert(idx < 0 || idx > ScriptObjDataManager.Instance.ScriptObjDataList.Count, "Valid 하지 않은 ScriptObjectData IDX 입니다.");
        return;
    }
    private void Open(int idx)
    {
        ScriptableObjData obj = ScriptObjDataManager.Instance.ScriptObjDataList[idx];
        
        this.ActivateObject();
        Debug.Assert(!gameObject.activeSelf, "ScriptWindow가 닫혀있지 않습니다. 버그임!!");

        NameText.text = obj.name;
        ScriptText.text = obj.scripts[0];
    }
    private void Next(int idx)
    {

    }
    private void Close()
    {
        currentScriptIdx = 0;
        gameObject.SetActive(false);
    }
    public override void Activate()
    {
        InteractionObject obj = PlayerManager.Instance.playerInteractObject.GetFstScrObj();

    }
}
