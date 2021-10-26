using UnityEngine;

/* ��ũ��Ʈ�� �ִ� �ΰ��� ������Ʈ (�������̽�)
 * InGameObjectManager�� idx�� ����
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
