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
    [SerializeField] private Text nameText;
    [SerializeField] private Text explanationText;
    [SerializeField] private int currentIdx;
    [SerializeField] private InteractionObjectType interactionItemType;

    IEnumerator SetLockedWindow(string str)
    {
        yield return null;
    }

    public override void Activate()
    {
        return;
    }

    public void SetIsLockedWindow(string str)
    {


        return;
    }

    public void SetInteractionObject(int idx, InteractionObjectType type)
    {
        this.currentIdx = idx;
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


            return;
        }

        else
        {
            return;
        }
    }
}
