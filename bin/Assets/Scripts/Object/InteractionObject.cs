using UnityEngine;

/* 스크립트가 있는 인게임 오브젝트 (인터페이스)
 * InGameObjectManager에 idx가 있음
 */
public abstract class InteractionObject : MonoBehaviour
{
    [SerializeField] protected bool isScriptable;
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
