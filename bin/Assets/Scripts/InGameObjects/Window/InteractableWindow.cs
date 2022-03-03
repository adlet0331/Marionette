using System.Collections;
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
    [SerializeField] private InteractionObjectType interactionItemType;
    
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

    public void SetInteractionObject(int idx, InteractionObjectType type)
    {
        if (blocked)
            return;

        this.currentIdx = idx;
        this.interactionItemType = type;
        if(type == InteractionObjectType.ScriptableObject)
        {
            ScriptableObjData currentData = ScriptObjDataManager.Instance.ScriptObjDataList[idx];

            nameText.text = currentData.name;
            explanationText.text = currentData.scriptableScript;
            return;
        }
            
        else if (type == InteractionObjectType.ItemableObject)
        {
            Item currentData = ItemDataManager.Instance.ItemDataList[idx];

            nameText.text = currentData.name;
            explanationText.text = currentData.itemInfo;
            return;
        }

        else if (type == InteractionObjectType.LockObject)
        {
            LockData currentData = LockDataManager.Instance.LockDataList[idx];

            nameText.text = currentData.interactName;
            explanationText.text = currentData.interactString;

            return;
        }

        else
        {
            return;
        }
    }
}
