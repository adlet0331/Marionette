using UnityEngine;
using UnityEngine.UI;

/* 상호작용 가능한 가장 가까운 오브젝트 정보 띄워주는 창
 * PlayerManager에서 호출
 * 
 * 아래 2개 출력
 * - 오브젝트 이름
 * - InGameObjectManager index = 0 인 String
 */
public class ScriptableWindow : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text explanationText;
    [SerializeField] private int currentIdx;

    public bool SetInteractionObject(int idx)
    {
        currentIdx = idx;
        ScriptableObjData currentObject = ScriptObjDataManager.Instance.ScriptObjDataList[idx];
        bool isDummy = false;
        if(currentObject == null)
        {
            currentObject = ScriptObjDataManager.Instance.ScriptObjDataList[0];
            isDummy = true;
        }
        nameText.text = currentObject.name;
        explanationText.text = currentObject.scripts[0];

        if (isDummy)
        {
            return false;
        }
        return true;
    }
}
