using UnityEngine;

/* Interact 되는 객체
 * 
 * Interact 상속 받아서 사용 (상호작용 함수)
 * 
 * idx - 데이터 인덱스
 * InGameObjectManager 에서 데이터 관리
 */
public abstract class InteractionObject : MonoBehaviour
{
    // 스크립트가 있는가 (대화창이 뜨는가)
    [SerializeField] protected bool isScriptable;
    public void SetisScriptable(bool isSc)
    {
        isScriptable = isSc;
    }

    // 데이터 인덱스 (없으면 -1)
    [SerializeField] private int idx;
    public int GetIdx()
    {
        if (isScriptable)
        {
            return idx;
        }
        else
        {
            return -1;
        }
    }

    public abstract void Interact();
}
